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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IBL;
using BL;
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBLInterface bl;

        public MainWindow()
        {
            InitializeComponent();
            bl = BLFactory.GetBL();
            Drone.Visibility = Visibility.Visible;
            XDS.Visibility = Visibility.Visible;
        }

        private void ShowDronesButton_Click(object sender, RoutedEventArgs e)
        {
            
            int ID;
            try
            {
                ID = Int32.Parse(Username.Text);                
            }
            catch (FormatException except)
            {
                ID = 0;                
            }
            string password = Password.Text;
            if (IsBusiness.IsChecked == true && bl.VerifyEmployeeCredentials(ID, password))
            {
                new DroneListWindow(bl).Show();
            }
            else if (IsCustomer.IsChecked == true && bl.VerifyCustomerCredentials(ID, password)) 
            {
                new DroneListWindow(bl).Show();
            }
            else
            {
                MessageBox.Show("One or more of the credentials provided were incorrect. Please try again.");
            }
            
        }
        

        
    }
}
