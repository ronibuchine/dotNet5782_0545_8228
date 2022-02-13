using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IBL;
using BL;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerViewWindow.xaml
    /// </summary>
    public partial class CustomerViewWindow : Window
    {
        IBLInterface bl;
        Customer customer;
        ObservableCollection<Package> incomingPackages = new();
        ObservableCollection<Package> outgoingPackages = new();

        internal CustomerViewWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            AddBorder.Visibility = Visibility.Visible;
            AddCustomerImage.Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Drone Window ctor for actions
        /// </summary>
        internal CustomerViewWindow(IBLInterface bl, Customer customer)
        {
            InitializeComponent();
            this.bl = bl;
            this.customer = customer;
            DataContext = this.customer;

            outgoingPackages = new(bl.GetCustomer(customer.ID).packagesFromCustomer.Select(p => new Package(p, "sent", customer.ID)));
            incomingPackages = new(bl.GetCustomer(customer.ID).packagesToCustomer.Select(p => new Package(p, "received", customer.ID)));
            SentPackageList.DataContext = outgoingPackages;
            ReceivedPackageList.DataContext = incomingPackages;

            CustomerInfoBorder.Visibility = Visibility.Visible;
            SentPackageList.Visibility = Visibility.Visible;
            ReceivedPackageList.Visibility = Visibility.Visible;
            OutgoingHeader.Visibility = Visibility.Visible;
            IncomingHeader.Visibility = Visibility.Visible;
            CustomerImage.Visibility = Visibility.Visible;
            AddCustomerPackage.Visibility = Visibility.Visible;
        }        
       

     

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomer(customer.ID, NameBlock.Text, int.Parse(PhoneBlock.Text).ToString());
                Synchronize();
            }
            catch (FormatException fExcept)
            {
                MessageBox.Show("Please enter a valid phone number.");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void Synchronize()
        {
            BL.Customer tempCustomer = bl.GetCustomer(customer.ID);
            customer.name = tempCustomer.name;
            customer.phoneNumber = tempCustomer.phone;
            Customer temp = CollectionManager.customers.Where(c => c.ID == customer.ID).FirstOrDefault();
            CollectionManager.customers[CollectionManager.customers.IndexOf(temp)] = customer;
        }

        private void SentPackageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Package package = (Package)SentPackageList.SelectedItem;
            new PackageViewWindow(bl, package, "sent", customer.ID).Show();
        }

        private void ReceivedPackageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Package package = (Package)ReceivedPackageList.SelectedItem;
            new PackageViewWindow(bl, package, "received", customer.ID).Show();
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.Customer temp = bl.AddCustomer(NameEntry.Text,
                  PhoneEntry.Text,
                  new Location(double.Parse(LatitudeEntry.Text) % 90,
                  double.Parse(LongitudeEntry.Text) % 180),
                  int.Parse(IDEntry.Text));
                customer = new(bl.GetCustomerList().First(c => c.ID == temp.ID));
                CollectionManager.customers.Add(customer);
                if (MessageBox.Show("Customer added successfully!", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    Close();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
           
        }

        private void AddCustomerPackage_Click(object sender, RoutedEventArgs e)
        {
            new PackageViewWindow(bl, customer, outgoingPackages).Show();
        }
    }
}
