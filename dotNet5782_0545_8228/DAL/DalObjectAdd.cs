using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        // Update objects section

                
        public void AddPackage() => AddDalItem(DataSource.packages, IdalDoType.PACKAGE);

        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, package, IdalDoType.PACKAGE);
       

        public void AddCustomer() => AddDalItem(DataSource.customers, IdalDoType.CUSTOMER);

        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, customer, IdalDoType.CUSTOMER);

        public void AddDrone() =>
           AddDalItem((List<Drone>)DataSource.drones, IdalDoType.DRONE);

        public void AddDrone(Drone drone) =>
            AddDalItem((List<Drone>)DataSource.drones, drone, IdalDoType.DRONE);

        public void AddStation() =>
           AddDalItem(DataSource.stations, IdalDoType.STATION);

        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, station, IdalDoType.STATION);
    }
}
