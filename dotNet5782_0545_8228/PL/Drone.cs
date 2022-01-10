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
    class Drone : INotifyPropertyChanged
    {

        public Drone(DroneToList drone)
        {
            ID = drone.ID;
            model = drone.model;
            weightCategory = drone.weightCategory;
            battery = drone.battery;
            status = drone.status;
            location = drone.location;
            packageNumber = drone.packageNumber;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        private string _model;
        private WeightCategories? _weightCategory;
        private double? _battery;
        private DroneStatuses? _status;
        private Location _location;
        private int? _packageNumber;
        public int ID
        {
            get
            {
                return _ID;
            }
            set { _ID = value; NotifyPropertyChanged("ID"); }
        }
        public string model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                NotifyPropertyChanged();
            }
        }
        public WeightCategories? weightCategory
        {
            get
            {
                return _weightCategory;
            }
            set { _weightCategory = value; NotifyPropertyChanged("weightCategory"); }
        }
        public double? battery
        {
            get
            {
                return _battery;
            }
            set { _battery = value; NotifyPropertyChanged("battery"); }
        }
        public DroneStatuses? status
        {
            get
            {
                return _status;
            }
            set { _status = value; NotifyPropertyChanged("status"); }
        }
        public Location location
        {
            get
            {
                return _location;
            }
            set { _location = value; NotifyPropertyChanged("location"); }
            
        }
        public int? packageNumber
        {
            get
            {
                return _packageNumber;
            }
            set { _packageNumber = value; NotifyPropertyChanged("packageNumber"); }
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
