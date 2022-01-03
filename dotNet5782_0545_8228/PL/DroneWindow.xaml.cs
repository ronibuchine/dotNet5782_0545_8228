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
    /// Interaction logic for the Drone Action Window UI.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBLInterface bl;
        Drone drone;
        ObservableCollection<Drone> drones;

        internal DroneWindow(IBLInterface bl, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            this.bl = bl;
            this.drones = drones;
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
        internal DroneWindow(IBLInterface bl, Drone drone, ObservableCollection<Drone> drones)
        {
            InitializeComponent();
            this.bl = bl;
            this.drone = drone;
            this.drones = drones;
            DataContext = this.drone;
            string packageString = drone.status == DroneStatuses.delivery ? drone.packageNumber.ToString() : "no assigned package";
            
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
                    ListWindow window = Application.Current.Windows.OfType<ListWindow>().FirstOrDefault();
                    drones = new(bl.GetDroneList().Select(d => new Drone(d)));
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

        private void ModelUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateDroneModelText.Text == "Enter Model" || UpdateDroneModelText.Text == "")
            {
                MessageBox.Show("Please enter a drone model.");
                UpdateDroneModelText.Focus();
                UpdateDroneModelText.FontWeight = FontWeights.Bold;
                UpdateDroneModelText.Background = Brushes.Red;
            }
            else
            {
                bl.UpdateDrone(drone.ID, UpdateDroneModelText.Text);
                Synchronize();
                MessageBox.Show("Drone Model Updated Successfully!");
            }

        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDroneButton.Visibility = Visibility.Visible;
            UpdateDroneModelText.Visibility = Visibility.Visible;
        }

        private void Synchronize()
        {
            BL.Drone tempDrone = bl.GetDrone(drone.ID);
            drone.status = tempDrone.status;
            drone.location = tempDrone.currentLocation;
            drone.battery = tempDrone.battery;
            drone.packageNumber = tempDrone.packageInTransfer == null ? null : tempDrone.packageInTransfer.ID;
            Drone temp = drones.Where(d => d.ID == drone.ID).FirstOrDefault();
            drones[drones.IndexOf(temp)] = drone;
        }

        private void SendToChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.ID);
                Synchronize();
                MessageBox.Show("Drone is now charging -- status is set to maintenance.");
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
                Synchronize();
                MessageBox.Show("Drone has now been assigned a package -- status set to delivery");                
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
                Synchronize();
                MessageBox.Show($"Drone has charged for {ChargingAmount.Text} hours.");

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
                Synchronize();
                MessageBox.Show("Drone has successfully collected a package -- status set to delivery.");
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
                Synchronize();
                MessageBox.Show("Drone delivered the package -- status set to free.");
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
