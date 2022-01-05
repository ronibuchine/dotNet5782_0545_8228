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
    /// Interaction logic for PackageViewWindow.xaml
    /// </summary>
    public partial class PackageViewWindow : Window
    {

        IBLInterface bl;
        Package package;
        DroneToList drone;
        internal PackageViewWindow(IBLInterface bl, Package package)
        {
            InitializeComponent();
            this.bl = bl;
            this.package = package;
            this.drone = bl.GetDroneList().Where(d => d.packageNumber == package.ID).FirstOrDefault();
            DataContext = package;
            DronePanel.DataContext = drone;
        }

        internal PackageViewWindow(IBLInterface bl, PackageAtCustomer package, string sendReceiveValue, int customerID)
        {
            this.bl = bl;
            this.package = new(package, sendReceiveValue, customerID);
            drone = bl.GetDroneList().Where(d => d.packageNumber == package.ID).FirstOrDefault();
            DataContext = package;
            if (drone != null) 
                DronePanel.DataContext = drone;
        }

        private void DeletePackageButton_Click(object sender, RoutedEventArgs e)
        {
            bl.DeletePackage(package.ID);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
