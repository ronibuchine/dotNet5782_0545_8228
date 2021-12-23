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
using BL;

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
            List<string> statuses = new List<string>();
            foreach (var status in Enum.GetValues(typeof(DroneStatuses)))
            {
                statuses.Add(status.ToString());
            }
            statuses.Add("All Drones");
            List<string> weights = new List<string>();
            foreach (var weight in Enum.GetValues(typeof(WeightCategories)))
            {
                weights.Add(weight.ToString());
            }
            weights.Add("All Drones");

            DroneStatusSelector.ItemsSource = statuses;
            DroneStatusSelector.SelectedItem = "All Drones";
            DroneWeightSelector.ItemsSource = weights;
            DroneWeightSelector.SelectedItem = "All Drones";
        }

        private void DroneStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isAllWeight = (string)DroneWeightSelector.SelectedItem == "All Drones";
            ComboBox combo = (ComboBox)sender;

            if (isAllWeight && (string)DroneStatusSelector.SelectedItem == "All Drones")
            {
                DroneListView.ItemsSource = bl.GetDroneList();
            }
            else if (isAllWeight && (string)DroneStatusSelector.SelectedItem != "All Drones")
            {
                DroneListView.ItemsSource = bl.GetSpecificDrones((d) => d.status == GetStatus());
            }
            else
            {
                DroneListView.ItemsSource = bl.GetSpecificDrones((d) => d.status == GetStatus() && d.weightCategory == GetWeight());
            }
           
        }

        private void DroneWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isAllStatus = (string)DroneStatusSelector.SelectedItem == "All Drones"; 
            ComboBox combo = (ComboBox)sender;

            if (isAllStatus && (string)DroneWeightSelector.SelectedItem == "All Drones")
            {
                DroneListView.ItemsSource = bl.GetDroneList();
            }
            else if (isAllStatus && (string)DroneWeightSelector.SelectedItem != "All Drones")
            {
                DroneListView.ItemsSource = bl.GetSpecificDrones((d) => d.weightCategory == GetWeight());
            }
            else
            {
                DroneListView.ItemsSource = bl.GetSpecificDrones((d) => d.status == GetStatus() && d.weightCategory == GetWeight());
            }
            
        }

        private DroneStatuses GetStatus()
        {
            if ((string)DroneStatusSelector.SelectedItem == "free") return DroneStatuses.free;
            else if ((string)DroneStatusSelector.SelectedItem == "maintenance") return DroneStatuses.maintenance;
            else return DroneStatuses.delivery;
        }

        private WeightCategories GetWeight()
        {
            if ((string)DroneWeightSelector.SelectedItem == "light") return WeightCategories.light;
            else if ((string)DroneWeightSelector.SelectedItem == "medium") return WeightCategories.medium;
            else return WeightCategories.heavy;
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

       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}
