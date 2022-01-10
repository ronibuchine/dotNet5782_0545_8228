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
        internal DroneWindow(IBLInterface bl, Drone drone)
        {
            InitializeComponent();
            this.bl = bl;
            this.drone = drone;
            DataContext = this.drone;

            TextEntries.Visibility = Visibility.Visible;            
            ButtonGrid.Visibility = Visibility.Visible;
            UpdateDroneButton.Visibility = Visibility.Visible;

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
                    //drones = new(bl.GetDroneList().Select(d => new Drone(d)));
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
            try
            {
                bl.ReleaseDroneFromCharge(drone.ID);
                Synchronize();
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

        private void DeleteDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteDrone(drone.ID);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
    }
}
