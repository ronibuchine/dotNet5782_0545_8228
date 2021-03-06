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
    /// Interaction logic for the Drone Action Window UI.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBLInterface bl;
        Drone drone;
        Package simulatedPackage = null;
        // reference to a stations charging drone list if we need to have live updates
        ObservableCollection<BL.Drone> chargingDrones;
        BackgroundWorker worker;

        internal DroneWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            WeightSelection.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            List<int> stationIds = new();
            foreach (var station in bl.GetAvailableStations())
            {
                stationIds.Add(station.ID);
            }
            AddGrid.Visibility = Visibility.Visible;
            StationSelection.ItemsSource = stationIds;
            ModelEntry.Visibility = Visibility.Visible;
            StationSelection.Visibility = Visibility.Visible;
            WeightSelection.Visibility = Visibility.Visible;
            AddDrone.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Drone Window ctor for actions
        /// </summary>
        internal DroneWindow(IBLInterface bl, Drone drone, ObservableCollection<BL.Drone> chargingDrones = null)
        {
            InitializeComponent();
            this.bl = bl;
            this.drone = drone;
            this.chargingDrones = chargingDrones;
            DataContext = this.drone;

            TextEntries.Visibility = Visibility.Visible;
            ButtonGrid.Visibility = Visibility.Visible;
            UpdateDroneButton.Visibility = Visibility.Visible;
            SimulationButton.Visibility = Visibility.Visible;

        }

        private void Synchronize()
        {
            BL.Drone tempDrone = bl.GetDrone(drone.ID);
            drone.status = tempDrone.status;
            drone.location = tempDrone.currentLocation;
            drone.battery = tempDrone.battery;
            drone.packageNumber = tempDrone.packageInTransfer == null ? null : tempDrone.packageInTransfer.ID;

            int stationID = bl.GetChargingStation(drone.ID);
            if (stationID != 0)
            {
                drone.location.longitude = CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).longitude;
                drone.location.latitude = CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).latitude;
            }
            // removes old drone from list and adds the updated one
            int index = CollectionManager.drones
                .IndexOf(CollectionManager.drones
                .FirstOrDefault(d => d.ID == drone.ID));
            CollectionManager.drones[index] = drone;

            if (chargingDrones != null)
                chargingDrones.Remove(chargingDrones.FirstOrDefault(d => d.ID == drone.ID));

        }       

        #region UI Elements
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
                int ID = bl.AddDrone(model, trueWeight, stationID).ID;
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).availableChargeSlots--;
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).occupiedSlots++;
                Drone drone = new(bl.GetDroneList().Where(d => d.ID == ID).FirstOrDefault());
                CollectionManager.drones.Add(drone);
                if (MessageBox.Show("Drone Added Successfully!", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();

            }
            catch (InvalidBlObjectException except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ModelUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }


        }

        private void UpdateDroneButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDroneButton.Visibility = Visibility.Visible;
            UpdateDroneModelText.Visibility = Visibility.Visible;
        }

        #endregion
              

        #region StackPanel Actions
        private void SendToChargeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.ID);
                int stationID = bl.GetChargingStation(drone.ID);
                Synchronize();
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).availableChargeSlots--;
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).occupiedSlots++;                
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
                CollectionManager.packages.FirstOrDefault(p => p.ID == drone.packageNumber).status = PackageStatuses.scheduled;                
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
            try
            {
                int stationID = bl.GetChargingStation(drone.ID);
                bl.ReleaseDroneFromCharge(drone.ID);
                Synchronize();
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).availableChargeSlots++;
                CollectionManager.stations.FirstOrDefault(s => s.ID == stationID).occupiedSlots--;
               
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
                CollectionManager.packages.FirstOrDefault(p => p.ID == drone.packageNumber).status = PackageStatuses.pickedUp;                
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
                
                CollectionManager.packages.FirstOrDefault(p => p.ID == drone.packageNumber).status = PackageStatuses.delivered;
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

        private void DeleteDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteDrone(drone.ID);
                CollectionManager.drones.Remove(CollectionManager.drones.First(d => d.ID == drone.ID));
                if (MessageBox.Show("Drone successfully deactivated.", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        #endregion


        #region Simulator
        public bool TryAssignPackage(int droneID)
        {
            try
            {
                if (bl.GetDrone(droneID).status == DroneStatuses.free)
                {
                    bl.AssignPackageToDrone(drone.ID);
                    int packageNumber = bl.GetDrone(drone.ID).packageInTransfer.ID;
                    simulatedPackage = CollectionManager.packages.FirstOrDefault(p => p.ID == packageNumber);
                    return false;
                }               
                else
                    return true;
            }
            catch (OperationNotPossibleException except)
            {
                return true;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action sim = new(Simulate);
            bl.ActivateSimulator(drone.ID,
                sim,
                () => worker.CancellationPending || TryAssignPackage(drone.ID));
        }

        public void Simulate()
        {
            worker.ReportProgress(1);
        }

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(drone.status == DroneStatuses.free ||
                (drone.status == DroneStatuses.delivery && bl.GetPackage(bl.GetDrone(drone.ID).packageInTransfer.ID).pickedUp == null)))
            {
                MessageBox.Show("The drone must be free or assigned a package in order to run the simulation.");
            }   
            else
            {
                ButtonGrid.Visibility = Visibility.Hidden;
                StopSimulationButton.Visibility = Visibility.Visible;
                SimulationButton.Visibility = Visibility.Hidden;
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

                try
                {
                    worker.RunWorkerAsync();                
                }
                catch (Exception except)
                {
                    worker.CancelAsync();
                    MessageBox.Show(except.Message);
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Simulation completed,\nno longer assigning packages");
            ButtonGrid.Visibility = Visibility.Visible;
            SimulationButton.Visibility = Visibility.Visible;
            StopSimulationButton.Visibility = Visibility.Hidden;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Synchronize();            
            if (drone.packageNumber != null) // update package status
            {
                simulatedPackage.status = bl.GetPackageList().FirstOrDefault(p => p.ID == drone.packageNumber).status;
            }
            else if (simulatedPackage != null)
            {
                simulatedPackage.status = PackageStatuses.delivered;
            }
        }

        private void StopSimulationButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            MessageBox.Show("Cancelation request recevied,\nsimulation will stop once the drone is fully charged.");
        }
        #endregion
    }
}
