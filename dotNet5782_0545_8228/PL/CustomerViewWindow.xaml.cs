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
        ObservableCollection<Customer> customers;

        internal CustomerViewWindow(IBLInterface bl, ObservableCollection<Customer> customers)
        {
            InitializeComponent();
            this.bl = bl;
            this.customers = customers;
        }

        /// <summary>
        /// Drone Window ctor for actions
        /// </summary>
        internal CustomerViewWindow(IBLInterface bl, Customer customer, ObservableCollection<Customer> customers)
        {
            InitializeComponent();
            this.bl = bl;
            this.customer = customer;
            this.customers = customers;
            DataContext = this.customer;
            SentPackageList.ItemsSource = bl.GetCustomer(customer.ID).packagesFromCustomer;
            ReceivedPackageList.ItemsSource = bl.GetCustomer(customer.ID).packagesToCustomer;

        }

        

        private void AddDrone_Click(object sender, RoutedEventArgs e)
        {
            string model = ModelEntry.Text;
            string weight = WeightSelection.Text;
            WeightCategories trueWeight;
            if (weight == "light")
                trueWeight = WeightCategories.light;
            else if (weight == "medium")
                trueWeight = WeightCategories.medium;
            else
                trueWeight = WeightCategories.heavy;
            int stationID = Int32.Parse(StationSelection.Text);
            try
            {
                bl.AddDrone(model, trueWeight, stationID);
                if (MessageBox.Show("Drone Added Successfully!", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    ListWindow window = Application.Current.Windows.OfType<ListWindow>().FirstOrDefault();
                    customers = new(bl.GetCustomerList().Select(c => new Customer(c)));
                    Close();
                }
            }
            catch (InvalidBlObjectException except)
            {
                if (MessageBox.Show(except.Message, "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCustomer(customer.ID, NameBlock.Text, PhoneBlock.Text);
                Synchronize();
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
            Customer temp = customers.Where(c => c.ID == customer.ID).FirstOrDefault();
            customers[customers.IndexOf(temp)] = customer;
        }

        private void SentPackageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PackageAtCustomer package = (PackageAtCustomer)SentPackageList.SelectedItem;
            new PackageViewWindow(bl, package, "sent", customer.ID).Show();
        }

        private void ReceivedPackageList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PackageAtCustomer package = (PackageAtCustomer)ReceivedPackageList.SelectedItem;
            new PackageViewWindow(bl, package, "received", customer.ID).Show();
        }
    }
}
