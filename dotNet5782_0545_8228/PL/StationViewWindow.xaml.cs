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
        }

        private void DeleteStationButton_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteStation(station.ID);
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
    }
}
