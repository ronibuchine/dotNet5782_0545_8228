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
using BLOBjectNamespace;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {

        IBLInterface bl;
        public DroneListWindow(IBLInterface bl)
        {
           
            InitializeComponent();
            this.bl = bl;
            DroneListView.ItemsSource = bl.GetDroneList();
            DroneStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            DroneWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
        }

        private void DroneStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> selectorSource = new();
            foreach (var item in Enum.GetValues(typeof(DroneStatuses)))
            {
                selectorSource.Add(item.ToString());
            }
            selectorSource.Add("All Drones");
            ComboBox combo = (ComboBox)sender;
            combo.ItemsSource = selectorSource;
            if ((string)combo.SelectedItem == "delivery")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.status == DroneStatuses.delivery);
            }
            if ((string)combo.SelectedItem == "maintenance")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.status == DroneStatuses.maintenance);
            }
            if ((string)combo.SelectedItem == "free")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.status == DroneStatuses.free);
            }
            if ((string)combo.SelectedItem == "All Drones")
            {
                DroneListView.ItemsSource = bl.GetDroneList();
            }
        }

        private void DroneWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> selectorSource = new();
            foreach (var item in Enum.GetValues(typeof(WeightCategories)))
            {
                selectorSource.Add(item.ToString());
            }
            selectorSource.Add("All Drones");
            ComboBox combo = (ComboBox)sender;
            combo.ItemsSource = selectorSource;
            if ((string)combo.SelectedItem == "light")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.weightCategory == WeightCategories.light);
            }
            if ((string)combo.SelectedItem == "medium")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.weightCategory == WeightCategories.medium);
            }
            if ((string)combo.SelectedItem == "heavy")
            {
                DroneListView.ItemsSource = bl.GetDroneList().FindAll((d) => d.weightCategory == WeightCategories.heavy);
            }
            if ((string)combo.SelectedItem == "All Drones")
            {
                DroneListView.ItemsSource = bl.GetDroneList();
            }
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl).Show();
        }

        private void DroneActionWindow(object sender, MouseButtonEventArgs e)
        {
            DroneToList drone = (DroneToList)DroneListView.SelectedItem;
            new DroneWindow(bl, drone).Show();
        }        
    }
}
