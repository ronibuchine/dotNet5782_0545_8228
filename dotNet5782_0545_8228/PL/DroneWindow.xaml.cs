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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for the Drone Action Window UI.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBLInterface bl;
        DroneToList drone;
        public DroneWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            WeightSelection.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            List<int> stationIds = new();
            foreach (var station in bl.GetAvailableStations())
            {
                stationIds.Add(station.ID);
            }
            StationSelection.ItemsSource = stationIds;
            ModelEntry.Visibility = Visibility.Visible;
            StationSelection.Visibility = Visibility.Visible;
            WeightSelection.Visibility = Visibility.Visible;
            AddDrone.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Drone Window ctor for actions
        /// </summary>
        public DroneWindow(IBLInterface bl, DroneToList drone)
        {
            InitializeComponent();
            this.bl = bl;
            this.drone = drone;
            string packageString = drone.status == DroneStatuses.delivery ? drone.packageNumber.ToString() : "no assigned package";
            DroneData.Visibility = Visibility.Visible;
            DroneData.Text = $"What action would you like to perform on the drone?\n ID = {drone.ID}";
            ButtonGrid.Visibility = Visibility.Visible;
            UpdateDroneButton.Visibility = Visibility.Visible;
            CollectPackageButton.Visibility = Visibility.Visible;
            DeliverPackageButton.Visibility = Visibility.Visible;
            SendToChargeButton.Visibility = Visibility.Visible;
            AssignPackageToDroneButton.Visibility = Visibility.Visible;
            ReleaseDroneFromChargeButton.Visibility = Visibility.Visible;
            DroneImage.Visibility = Visibility.Visible;

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
                    DroneListWindow window = Application.Current.Windows.OfType<DroneListWindow>().FirstOrDefault();
                    window.DroneStatusSelector.SelectedItem = window.DroneWeightSelector.SelectedItem = "All Drones";
                    window.DroneListView.ItemsSource = bl.GetDroneList();
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

        private void RefreshAndClose()
        {
            DroneListWindow window = Application.Current.Windows.OfType<DroneListWindow>().FirstOrDefault();
            window.DroneStatusSelector.SelectedItem = window.DroneWeightSelector.SelectedItem = "All Drones";
            window.DroneListView.ItemsSource = bl.GetDroneList();
            Close();
        }

      

        private void ModelUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateDroneModelText.Text == "Enter Model")
            {
                MessageBox.Show("Please enter a drone model.");
                UpdateDroneModelText.Focus();
                UpdateDroneModelText.FontWeight = FontWeights.Bold;
                UpdateDroneModelText.Background = Brushes.Red;
            }
            else
            {
                bl.UpdateDrone(drone.ID, UpdateDroneModelText.Text);
                ModelUpdateButton.Visibility = Visibility.Hidden;
                UpdateDroneModelText.Visibility = Visibility.Hidden;
                if (MessageBox.Show("Drone Model Updated Successfully!", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    RefreshAndClose();

            }

        }
      

        private void ModelEntry_MouseEnter(object sender, MouseEventArgs e)
        {
            ModelEntry.Text = "";
        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            ModelUpdateButton.Visibility = Visibility.Visible;
            UpdateDroneModelText.Visibility = Visibility.Visible;
        }

        private void SendToChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.ID);
                if (MessageBox.Show("Drone is now charging -- status is set to maintenance.", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    RefreshAndClose();
            }
            catch (InvalidBlObjectException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (InvalidOperationException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void AssignPackageToDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AssignPackageToDrone(drone.ID);
                if (MessageBox.Show("Drone has now been assigned a package -- status set to delivery", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    RefreshAndClose();
            }
            catch (InvalidBlObjectException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (InvalidOperationException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void ReleaseDroneFromChargeButton_Click(object sender, RoutedEventArgs e)
        {
            if (drone.status == DroneStatuses.maintenance)
            {
                ChargingReleaseButton.Visibility = Visibility.Visible;
                ChargingAmount.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Drone is not currently charging.");
            }
        }

        private void ChargingAmount_MouseEnter(object sender, MouseEventArgs e)
        {
            ChargingAmount.Text = "";
        }

        private void ChargingReleaseButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                bl.ReleaseDroneFromCharge(drone.ID, Int32.Parse(ChargingAmount.Text));
                if (MessageBox.Show($"Drone has charged for {ChargingAmount.Text} hours.", "", MessageBoxButton.OK) == MessageBoxResult.OK) 
                    RefreshAndClose();

            }
            catch (InvalidBlObjectException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (InvalidOperationException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void CollectPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.CollectPackage(drone.ID);
                if (MessageBox.Show("Drone has successfully collected a package -- status set to delivery.", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    RefreshAndClose();
            }
            catch (InvalidBlObjectException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (InvalidOperationException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void DeliverPackageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeliverPackage(drone.ID);
                if (MessageBox.Show("Drone is delivering a package -- status set to delivery.", "", MessageBoxButton.OK) == MessageBoxResult.OK)
                    RefreshAndClose();
            }
            catch (InvalidBlObjectException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (InvalidOperationException i)
            {
                MessageBox.Show(i.Message);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
    }
}
