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
            _ID = drone.ID;
            _model = drone.model;
            _weightCategory = drone.weightCategory;
            _battery = drone.battery;
            _status = drone.status;
            _location = drone.location;
            _packageNumber = drone.packageNumber;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        private string _model;
        private BL.WeightCategories _weightCategory;
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
            set { _ID = value; NotifyPropertyChanged(); }
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
                BLFactory.GetBL().UpdateDrone(ID, value);
                NotifyPropertyChanged();
            }
        }
        public WeightCategories weightCategory
        {
            get
            {
                return _weightCategory;
            }
            set { _weightCategory = value; NotifyPropertyChanged(); }
        }
        public double? battery
        {
            get
            {
                return _battery;
            }
            set { _battery = value; NotifyPropertyChanged(); }
        }
        public DroneStatuses? status
        {
            get
            {
                return _status;
            }
            set { _status = value; NotifyPropertyChanged(); }
        }
        public Location location
        {
            get
            {
                return _location;
            }
            set { _location = value; NotifyPropertyChanged(); }
            
        }
        public int? packageNumber
        {
            get
            {
                return _packageNumber;
            }
            set { _packageNumber = value; NotifyPropertyChanged(); }
        }

        private BL.Drone GetDrone() => BLFactory.GetBL().GetDrone(ID);        

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
