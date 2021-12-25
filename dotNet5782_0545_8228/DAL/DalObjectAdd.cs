using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        // Add objects section
        public void AddPackage() => AddDalItem(DataSource.packages, DataSource.MAX_PACKAGES, IdalDoType.PACKAGE);

        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, DataSource.MAX_PACKAGES, package, IdalDoType.PACKAGE);

        public void AddCustomer() => AddDalItem(DataSource.customers, DataSource.MAX_CUSTOMERS, IdalDoType.CUSTOMER);

        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, DataSource.MAX_CUSTOMERS, customer, IdalDoType.CUSTOMER);

        public void AddDrone() =>
           AddDalItem(DataSource.drones, DataSource.MAX_DRONES, IdalDoType.DRONE);

        public void AddDrone(Drone drone) =>
            AddDalItem((List<Drone>)DataSource.drones, DataSource.MAX_DRONES, drone, IdalDoType.DRONE);

        public void AddStation() =>
           AddDalItem(DataSource.stations,DataSource.MAX_STATIONS, IdalDoType.STATION);

        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, DataSource.MAX_STATIONS, station, IdalDoType.STATION);
    }
}
