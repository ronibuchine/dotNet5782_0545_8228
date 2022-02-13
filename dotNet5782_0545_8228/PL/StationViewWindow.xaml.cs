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
    /// Interaction logic for StationViewWindow.xaml
    /// </summary>
    public partial class StationViewWindow : Window
    {
        IBLInterface bl;
        Station station;
        ObservableCollection<BL.Drone> chargingDrones;

        internal StationViewWindow(IBLInterface bl, Station station)
        {
            InitializeComponent();
            this.bl = bl;
            this.station = station;
            DataContext = station;
            ChargingDroneList.DataContext = chargingDrones = new(bl.GetStation(station.ID).chargingDrones);
            ChargingDroneList.Visibility = Visibility.Visible;
            StationInfo.Visibility = Visibility.Visible;
            StationImage.Visibility = Visibility.Visible;
            Drones.Visibility = Visibility.Visible;
            SID.Visibility = Visibility.Visible;
            IName.Visibility = Visibility.Visible;
            Chargers.Visibility = Visibility.Visible;
            ILatitude.Visibility = Visibility.Visible;
            ILongitude.Visibility = Visibility.Visible;
            DeleteStationButton.Visibility = Visibility.Visible;
            TotalSlots.Text = (station.availableChargeSlots + station.occupiedSlots).ToString();
        }

        internal StationViewWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            AddBorder.Visibility = Visibility.Visible;
            StationBWImage.Visibility = Visibility.Visible;
        }

        private void DeleteStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteStation(station.ID);
                CollectionManager.stations.Remove(CollectionManager.stations.First(s => s.ID == station.ID));
                if (MessageBox.Show("Station has been decomissioned.", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
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

        private void ChargingDroneList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BL.Drone temp = (BL.Drone)ChargingDroneList.SelectedItem;
            Drone drone = new(bl.GetDroneList().Where(d => d.ID == temp.ID).FirstOrDefault());
            new DroneWindow(bl, drone, chargingDrones).Show();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Location location = new(Double.Parse(LatitudeEntry.Text) % 90, Double.Parse(LongitudeEntry.Text) % 180);
                int stationID = bl.AddStation(NameEntry.Text, location, Int32.Parse(SlotsEntry.Text)).ID;
                Station station = new(bl.GetStationList().Where(s => s.ID == stationID).FirstOrDefault());
                CollectionManager.stations.Add(station);
                if (MessageBox.Show("Station has been added successfully.", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
            }
            catch (InvalidBlObjectException except)
            {
                MessageBox.Show(except.Message);
            }
            catch (FormatException except)
            {
                MessageBox.Show("Please enter acceeptable numbers for location and charge slot entries.");
            }
        }

        private void UpdateStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int totalSlots = int.Parse(TotalSlots.Text);
                string stationName = NameBlock.Text;
                bl.UpdateStation(station.ID, stationName, totalSlots);
                // on success, update the UI
                CollectionManager.stations.First(s => s.ID == station.ID).availableChargeSlots = totalSlots - station.occupiedSlots;
                CollectionManager.stations.First(s => s.ID == station.ID).name = stationName;
                MessageBox.Show("Station updated succesfully");
            }
            catch (FormatException fExcept)
            {
                MessageBox.Show("Please enter a valid number for the charging slots.");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
    }
}
