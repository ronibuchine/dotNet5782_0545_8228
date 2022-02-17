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
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        IBL.IBLInterface bl;
        public CreateAccountWindow(IBL.IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerAccountCheck.IsChecked == true)
                {
                    BL.Customer customer = bl.GetCustomer(Int32.Parse(IDBox.Text));
                    bl.UpdateCustomer(Int32.Parse(IDBox.Text), NameBox.Text, PhoneBox.Text, PasswordBox.Text);
                    CollectionManager.customers.FirstOrDefault(c => c.ID == int.Parse(IDBox.Text)).name = NameBox.Text;
                    CollectionManager.customers.FirstOrDefault(c => c.ID == int.Parse(IDBox.Text)).phoneNumber = PhoneBox.Text;
                    if (MessageBox.Show("Account Created Successfully", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
                }
                else //employee is checked
                {
                    
                    bl.AddEmployee(Int32.Parse(IDBox.Text), PasswordBox.Text);
                    if (MessageBox.Show("Account Created Successfully", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
                }
                
            }
            catch (InvalidBlObjectException except)
            {
                try
                {
                    bl.AddCustomer(NameBox.Text,
                        (Int32.Parse(PhoneBox.Text)).ToString(),
                        new Location(new Random().NextDouble() * 360, new Random().NextDouble() * 180),
                        Int32.Parse(IDBox.Text),
                        PasswordBox.Text);
                    CollectionManager.customers.Add(new Customer(bl.GetCustomerList().FirstOrDefault(c => c.ID == Int32.Parse(IDBox.Text))));
                    if (MessageBox.Show("Account Created Successfully", "", MessageBoxButton.OK) == MessageBoxResult.OK) Close();
                }
                catch (Exception fExcept)
                {
                    MessageBox.Show("Please enter a valid ID/Phone Number");
                }
                
            }  
            catch (OperationNotPossibleException except)
            {
                MessageBox.Show(except.Message);
            }
            catch (FormatException except)
            {
                MessageBox.Show("Please enter a valid ID");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
