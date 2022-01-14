using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using IBL;

namespace PL
{
    internal class CollectionManager
    {
        
        internal CollectionManager()
        {            
            drones = new(BLFactory.GetBL().GetDroneList().Select(d => new Drone(d)));
            stations = new(BLFactory.GetBL().GetStationList().Select(s => new Station(s)));
            packages = new(BLFactory.GetBL().GetPackageList().Select(p => new Package(p)));
            customers = new(BLFactory.GetBL().GetCustomerList().Select(c => new Customer(c))); 
        }       

        internal static ObservableCollection<Drone> drones { get; set; }
        internal static ObservableCollection<Station> stations { get; set; }
        internal static ObservableCollection<Package> packages { get; set; }
        internal static ObservableCollection<Customer> customers { get; set; }




    }
}
