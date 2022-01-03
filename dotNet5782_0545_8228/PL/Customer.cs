using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace PL
{
    class Customer : INotifyPropertyChanged
    {

        public Customer(CustomerToList customer)
        {
            ID = customer.ID;
            name = customer.name;
            phoneNumber = customer.phoneNumber;
            numberPackagesDelivered = customer.numberPackagesDelivered;
            numberPackagesUndelivered = customer.numberPackagesUndelivered;
            numberReceivedPackages = customer.numberReceivedPackages;
            numberExpectedPackages = customer.numberExpectedPackages;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        private string _name;
        private string _phoneNumber;
        private int _numberPackagesDelivered;
        private int _numberPackagesUndelivered;
        private int _numberReceivedPackages;
        private int _numberExpectedPackages;

        public int ID 
        {
            get
            { 
                return _ID;
            }
            set { _ID = value; NotifyPropertyChanged(); }
        }
        public string name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public string phoneNumber 
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged();
            }
        }
        public int numberPackagesDelivered 
        {
            get
            {
                return _numberPackagesDelivered;
            }
            set { _numberPackagesDelivered = value; NotifyPropertyChanged(); }
        }
        public int numberPackagesUndelivered 
        {
            get
            {
                return _numberPackagesUndelivered;
            }
            set { _numberPackagesUndelivered = value; NotifyPropertyChanged(); }
        }
        public int numberReceivedPackages 
        {
            get
            {
                return _numberReceivedPackages;
            }
            set { _numberReceivedPackages = value; NotifyPropertyChanged(); }
        }
        public int numberExpectedPackages         {            get
            {
                return _numberExpectedPackages;
            }            set { _numberExpectedPackages = value; NotifyPropertyChanged(); }        }               private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }    }
}
