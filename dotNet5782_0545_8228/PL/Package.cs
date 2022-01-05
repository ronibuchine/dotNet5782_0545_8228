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
    class Package : INotifyPropertyChanged
    {

        public Package(PackageToList package)
        {
            ID = package.ID;
            senderName = package.senderName;
            receiverName = package.receiverName;
            weightCategory = package.weightCategory;
            priority = package.priority;
            status = package.status;
        }

        public Package(PackageAtCustomer package, string sendReceiveValue, int customerID)
        {
            ID = package.ID;
            weightCategory = package.weight;
            priority = package.priority;
            status = package.status;
            if (sendReceiveValue == "sent")
                senderName = BLFactory.GetBL().GetCustomer(customerID).name;
            else
                receiverName = BLFactory.GetBL().GetCustomer(customerID).name;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        private string _senderName;
        private string _receiverName;
        private WeightCategories _weightCategory;
        private Priorities _priority;
        private PackageStatuses _status;

        public int ID 
        { 
            get
            {
                return _ID;
            }
            set { _ID = value; NotifyPropertyChanged(); }
            
        }
        public string senderName 
        {
            get
            {
                return _senderName;
            }
            set { _senderName = value; NotifyPropertyChanged(); }
        }
        public string receiverName 
        {
            get
            {
                return _receiverName;
            }
            set { _receiverName = value; NotifyPropertyChanged(); }
        }
        public WeightCategories weightCategory 
        {
            get
            {
                return _weightCategory;
            }
            set { _weightCategory = value; NotifyPropertyChanged(); }
            
        }
        public Priorities priority 
        {
            get
            {
                return _priority;
            }
            set { _priority = value; NotifyPropertyChanged(); }
        }
        public PackageStatuses status 
        {
            get
            {
                return _status;
            }
            set { _status = value; NotifyPropertyChanged(); }
        }       

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
