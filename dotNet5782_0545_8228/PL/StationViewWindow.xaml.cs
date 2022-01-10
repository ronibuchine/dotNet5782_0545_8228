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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationViewWindow.xaml
    /// </summary>
    public partial class StationViewWindow : Window
    {
        IBLInterface bl;
        Station station;

        internal StationViewWindow(IBLInterface bl, Station station)
        {
            InitializeComponent();
            this.bl = bl;
            this.station = station;
            DataContext = station;
            ChargingDroneList.DataContext = bl.GetStation(station.ID).chargingDrones;
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
            new DroneWindow(bl, drone).Show();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Location location = new(Double.Parse(LatitudeEntry.Text), Double.Parse(LongitudeEntry.Text));
                bl.AddStation(NameEntry.Text, location, Int32.Parse(SlotsEntry.Text));
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
    }
}
