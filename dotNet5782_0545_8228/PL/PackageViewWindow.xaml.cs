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
    /// Interaction logic for PackageViewWindow.xaml
    /// </summary>
    public partial class PackageViewWindow : Window
    {

        IBLInterface bl;
        Package package;
        DroneToList drone;
        Customer customer = null;
        ObservableCollection<Package> customersOutgoingPackages = null;

        internal PackageViewWindow(IBLInterface bl, Package package)
        {
            InitializeComponent();
            this.bl = bl;
            this.package = package;
            this.drone = bl.GetDroneList().Where(d => d.packageNumber == package.ID).FirstOrDefault();
            DataContext = package;
            DronePanel.DataContext = drone;
            PackageInfoImage.Visibility = Visibility.Visible;
            ViewDrone.Visibility = Visibility.Visible;
            PackagePanel.Visibility = Visibility.Visible;
            DronePanel.Visibility = Visibility.Visible;
        }

        internal PackageViewWindow(IBLInterface bl, Package package, string sendReceiveValue, int customerID)
        {
            InitializeComponent();
            this.bl = bl;
            this.package = package;
            drone = bl.GetDroneList().Where(d => d.packageNumber == package.ID).FirstOrDefault();
            DataContext = package; 
            DronePanel.DataContext = drone;
            PackageInfoImage.Visibility = Visibility.Visible;
            ViewDrone.Visibility = Visibility.Visible;
            PackagePanel.Visibility = Visibility.Visible;
            DronePanel.Visibility = Visibility.Visible;

        }

        internal PackageViewWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            AddBorder.Visibility = Visibility.Visible;
            AddPackageImage.Visibility = Visibility.Visible;
            SenderSelection.ItemsSource = CollectionManager.customers.Select(c => c.ID);
            ReceiverSelection.ItemsSource = CollectionManager.customers.Select(c => c.ID);
            WeightSelection.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            PrioritySelection.ItemsSource = Enum.GetValues(typeof(Priorities));
        }

        internal PackageViewWindow(IBLInterface bl, Customer customer, ObservableCollection<Package> outgoingPackages)
        {
            InitializeComponent();
            this.bl = bl;
            this.customer = customer;
            this.customersOutgoingPackages = outgoingPackages;
            CollectionManager.customers = new(bl.GetCustomerList().Select(c => new Customer(c)));
            AddBorder.Visibility = Visibility.Visible;
            AddPackageImage.Visibility = Visibility.Visible;
            ReceiverSelection.ItemsSource = CollectionManager.customers.Select(c => c.ID);
            WeightSelection.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            PrioritySelection.ItemsSource = Enum.GetValues(typeof(Priorities));
            SenderText.Visibility = Visibility.Hidden;
            SenderSelection.Visibility = Visibility.Hidden;
        }

        private void DeletePackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeletePackage(package.ID);
                CollectionManager.packages.Remove(CollectionManager.packages.First(p => p.ID == package.ID));
                if (MessageBox.Show("Package removed successfully", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    Close();
                
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }    

        private void ViewDrone_Click(object sender, RoutedEventArgs e)
        {
            if (drone != null && customer == null)
                new DroneWindow(bl, new(drone)).Show();
            else
                MessageBox.Show("There is no drone assigned to the selected package.");
        }
       

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                int senderID;
                if (customer != null)
                    senderID = customer.ID;
                else
                    senderID = (int)SenderSelection.SelectedItem;
                if (senderID == (int)ReceiverSelection.SelectedItem)
                    throw new OperationNotPossibleException("The sender and receiver cannot be the same person.\nPlease try again.");
                int packageID = bl.AddPackage(senderID,
                    (int)ReceiverSelection.SelectedItem,
                    (WeightCategories)WeightSelection.SelectedItem,
                    (Priorities)PrioritySelection.SelectedItem).ID;
                Package package = new(bl.GetPackageList().Where(p => p.ID == packageID).FirstOrDefault());               
                if (customersOutgoingPackages != null && customer != null)
                {                    
                    customersOutgoingPackages.Add(package);                    
                    customer.numberPackagesUndelivered++;
                }
                CollectionManager.packages.Add(package);


                if (MessageBox.Show("Package added successfully!", "", MessageBoxButton.OK) == MessageBoxResult.OK) 
                    Close();
                

            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
    }
}
