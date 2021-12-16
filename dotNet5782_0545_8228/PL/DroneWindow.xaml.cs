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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBLInterface bl;
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
        }

        /// <summary>
        /// Drone Window ctor for actions
        /// </summary>
        public DroneWindow(IBLInterface bl, DroneToList drone)
        {
            InitializeComponent();
            ModelEntry.Visibility = Visibility.Hidden;
            StationSelection.Visibility = Visibility.Hidden;
            WeightSelection.Visibility = Visibility.Hidden;
            AddDrone.Visibility = Visibility.Hidden;
            
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
            bl.AddDrone(model, trueWeight, stationID);

            Close();
        }       
    }
}
