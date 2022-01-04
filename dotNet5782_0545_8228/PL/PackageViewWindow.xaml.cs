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
        PackageAtCustomer packageAtCustomer;
        internal PackageViewWindow(IBLInterface bl, Package package)
        {
            InitializeComponent();
            this.bl = bl;
            this.package = package;
            DataContext = package;
        }

        internal PackageViewWindow(IBLInterface bl, PackageAtCustomer package)
        {
            this.bl = bl;
            packageAtCustomer = package;
            DataContext = packageAtCustomer;
        }

        private void DeletePackageButton_Click(object sender, RoutedEventArgs e)
        {
            bl.DeletePackage(packageAtCustomer.ID);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
