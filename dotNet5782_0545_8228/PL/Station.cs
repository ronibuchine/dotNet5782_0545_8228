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
    class Station : INotifyPropertyChanged
    {

        public Station(StationToList station)
        {
            ID = station.ID;
            name = station.name;
            availableChargeSlots = station.availableChargeSlots;
            occupiedSlots = station.occupiedSlots;
            latitude = station.latitude;
            longitude = station.longitude;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        private string _name;
        private int _availableChargeSlots;
        private int _occupiedSlots;
        private double _latitude;
        private double _longitude;

        public int ID
        {
            get{ return _ID; }
            set { _ID = value; NotifyPropertyChanged(); }
        }
        public string name 
        {
            get{return _name;}
            set{ _name = value;  NotifyPropertyChanged();}
        }
        public int availableChargeSlots
        { 
            get
            {
                return _availableChargeSlots;
            }
            set { _availableChargeSlots = value; NotifyPropertyChanged(); }
        }
        public int occupiedSlots
        {
            get{return _occupiedSlots;}
            set { _occupiedSlots = value; NotifyPropertyChanged(); }
            
        }
        public double latitude 
        { 
            get{return _latitude;}
            set { _latitude = value; NotifyPropertyChanged(); }
            
        }
        public double longitude 
        { 
            get{return _longitude;}
            set { _longitude = value; NotifyPropertyChanged(); }
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
