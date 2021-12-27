using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        // Add objects section
        public void AddPackage() => AddDalItem(DataSource.packages, IdalDoType.PACKAGE);

        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, package);

        public void AddCustomer() => AddDalItem(DataSource.customers, IdalDoType.CUSTOMER);

        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, customer);

        public void AddDrone() =>
           AddDalItem(DataSource.drones, IdalDoType.DRONE);

        public void AddDrone(Drone drone) =>
            AddDalItem(DataSource.drones, drone);

        public void AddStation() =>
           AddDalItem(DataSource.stations, IdalDoType.STATION);

        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, station);
    }
}
