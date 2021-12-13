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
        }
    }
}
