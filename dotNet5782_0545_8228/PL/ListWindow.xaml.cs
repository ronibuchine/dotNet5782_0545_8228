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
using System.ComponentModel;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {

        IBLInterface bl;
        internal bool refreshed = false;
        public ListWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            DroneListView.DataContext = CollectionManager.drones;
            StationListView.DataContext = CollectionManager.stations;
            PackageListView.DataContext = CollectionManager.packages;
            CustomerListView.DataContext = CollectionManager.customers;
            DroneStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            DroneWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            PackagePrioritySelector.ItemsSource = Enum.GetValues(typeof(Priorities));
            PackageWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
          

        }

        #region Window Actions
        private void DroneStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDrones();
        }

        private void DroneWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterDrones();
        }

                       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {            
            DroneStatusSelector.SelectedIndex = DroneWeightSelector.SelectedIndex = -1;            
            PackagePrioritySelector.SelectedIndex = PackageWeightSelector.SelectedIndex = -1;
            Refresh();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

       
       
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void PackagePrioritySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPackages();
        }

        private void PackageWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterPackages();
        }

        private void CapacityGrouper_Click(object sender, RoutedEventArgs e)
        {
            CollectionManager.stations = new(CollectionManager.stations.OrderByDescending(s => s.occupiedSlots + s.availableChargeSlots));
            StationListView.DataContext = CollectionManager.stations;
        }

        private void AvailableChargersGrouper_Click(object sender, RoutedEventArgs e)
        {
            CollectionManager.stations = new(CollectionManager.stations.OrderByDescending(s => s.availableChargeSlots));
            StationListView.DataContext = CollectionManager.stations;
        }

        private void IncomingPackageGrouping_Click(object sender, RoutedEventArgs e)
        {
            CollectionManager.customers = new(CollectionManager.customers.OrderByDescending(c => c.numberExpectedPackages + c.numberReceivedPackages));
            CustomerListView.DataContext = CollectionManager.customers;
        }

        private void OutgoingPackageGrouping_Click(object sender, RoutedEventArgs e)
        {
            CollectionManager.customers = new(CollectionManager.customers.OrderByDescending(c => c.numberPackagesDelivered + c.numberPackagesUndelivered));
            CustomerListView.DataContext = CollectionManager.customers;
        }

        #endregion

        #region New Windows
        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Station station = (Station)StationListView.SelectedItem;
            new StationViewWindow(bl, station).Show();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer customer = (Customer)CustomerListView.SelectedItem;
            new CustomerViewWindow(bl, customer).Show();
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
        }

        private void DroneActionWindow(object sender, MouseButtonEventArgs e)
        {
            Drone drone = (Drone)DroneListView.SelectedItem;
            new DroneWindow(bl, drone).Show();
        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            new StationViewWindow(bl).Show();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomerViewWindow(bl).Show();
        }

        private void PackageListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Package package = (Package)PackageListView.SelectedItem;
            new PackageViewWindow(bl, package).Show();
        }

        private void AddPackageButton_Click(object sender, RoutedEventArgs e)
        {
            new PackageViewWindow(bl).Show();
        }
        #endregion

       
        #region Helper Functions

        /// <summary>
        /// Filters the packages according to the combobox selections
        /// </summary>
        private void FilterPackages()
        {
            CollectionManager.packages = new(bl.GetPackageList().Select(p => new Package(p)));
            if (PackagePrioritySelector.SelectedItem == null && PackageWeightSelector.SelectedItem == null)
                return;
            else if (PackagePrioritySelector.SelectedItem == null)
                CollectionManager.packages = new(CollectionManager.packages.Where(p => p.weightCategory == GetPackageWeight()));
            else if (PackageWeightSelector.SelectedItem == null)
                CollectionManager.packages = new(CollectionManager.packages.Where(p => p.priority == GetPackagePriority()));
            else
                CollectionManager.packages = new(CollectionManager.packages.Where(p => p.priority == GetPackagePriority() && p.weightCategory == GetPackageWeight()));
            PackageListView.DataContext = CollectionManager.packages;
        }

        /// <summary>
        /// Refreshes all the observable collections
        /// </summary>
        private void Refresh()
        {
            CollectionManager.drones = new(bl.GetDroneList().Select(d => new Drone(d)));
            DroneListView.DataContext = CollectionManager.drones;
            CollectionManager.stations = new(bl.GetStationList().Select(s => new Station(s)));
            StationListView.DataContext = CollectionManager.stations;
            CollectionManager.packages = new(bl.GetPackageList().Select(p => new Package(p)));
            PackageListView.DataContext = CollectionManager.packages;
            CollectionManager.customers = new(bl.GetCustomerList().Select(c => new Customer(c)));
            CustomerListView.DataContext = CollectionManager.customers;
            refreshed = false;
        }

        private WeightCategories GetPackageWeight() => (WeightCategories)PackageWeightSelector.SelectedItem;

        private Priorities GetPackagePriority() => (Priorities)PackagePrioritySelector.SelectedItem;

        private DroneStatuses GetDroneStatus() => (DroneStatuses)DroneStatusSelector.SelectedItem;        

        private WeightCategories GetDroneWeight() => (WeightCategories)DroneWeightSelector.SelectedItem;
        
        private void FilterDrones()
        {
            CollectionManager.drones = new(bl.GetDroneList().Select(d => new Drone(d)));
            if (DroneStatusSelector.SelectedItem == null && DroneWeightSelector.SelectedItem == null)
                return;
            else if (DroneStatusSelector.SelectedItem == null)
                CollectionManager.drones = new(CollectionManager.drones.Where(d => d.weightCategory == GetDroneWeight()));
            else if (DroneWeightSelector.SelectedItem == null)
                CollectionManager.drones = new(CollectionManager.drones.Where(d => d.status == GetDroneStatus()));
            else
                CollectionManager.drones = new(CollectionManager.drones.Where(d => d.status == GetDroneStatus() && d.weightCategory == GetDroneWeight()));
            DroneListView.DataContext = CollectionManager.drones;
        }


        #endregion

       
    }
}
