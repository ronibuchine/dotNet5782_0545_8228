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
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {

        IBLInterface bl;
        internal ObservableCollection<Drone> drones;
        internal ObservableCollection<Station> stations;
        internal ObservableCollection<Package> packages;
        internal ObservableCollection<Customer> customers;
        public ListWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            drones = new(bl.GetDroneList().Select(d => new Drone(d)));
            DroneListView.DataContext = drones;
            stations = new(bl.GetStationList().Select(s => new Station(s)));
            StationListView.DataContext = stations;
            packages = new(bl.GetPackageList().Select(p => new Package(p)));
            PackageListView.DataContext = packages;
            customers = new(bl.GetCustomerList().Select(c => new Customer(c)));
            CustomerListView.DataContext = customers;
            DroneStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            DroneWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));           
        }

        private void DroneStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DroneWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (DroneStatusSelector.SelectedItem == null && DroneWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones;
            else if (DroneStatusSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones.Where(d => d.weightCategory == GetWeight());
            else if (DroneWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones.Where(d => d.status == GetStatus());
            else
                DroneListView.ItemsSource = drones.Where(d => d.status == GetStatus() && d.weightCategory == GetWeight());
        }

        private DroneStatuses GetStatus()
        {
            return (DroneStatuses)DroneStatusSelector.SelectedItem;            
        }

        private WeightCategories GetWeight()
        {
            return (WeightCategories)DroneWeightSelector.SelectedItem;            
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

       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = drones;
            DroneStatusSelector.SelectedIndex = DroneWeightSelector.SelectedIndex = -1;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CustomerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer customer = (Customer)CustomerListView.SelectedItem;
            new CustomerViewWindow(bl, customer, customers).Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            drones = new(bl.GetDroneList().Select(d => new Drone(d)));
            DroneListView.DataContext = drones;
            stations = new(bl.GetStationList().Select(s => new Station(s)));
            StationListView.DataContext = stations;
            packages = new(bl.GetPackageList().Select(p => new Package(p)));
            PackageListView.DataContext = packages;
            customers = new(bl.GetCustomerList().Select(c => new Customer(c)));
            CustomerListView.DataContext = customers;
        }

        private void PackageListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Package package = (Package)PackageListView.SelectedItem;
            new PackageViewWindow(bl, package).Show();
        }

        private void StationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Station station = (Station)StationListView.SelectedItem;
            new StationViewWindow(bl, station).Show();
        }

        private void AddStationButton_Click(object sender, RoutedEventArgs e)
        {
            new StationViewWindow(bl).Show();
        }
    }
}
