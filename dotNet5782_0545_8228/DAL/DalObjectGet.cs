using System;
using System.Collections.Generic;
using DO;
using DALAPI;
using System.Linq;

namespace DAL
{
    public partial class DalObject : IDAL
    {

       

        public IEnumerable<Drone> GetAllDrones() => GetAllItems(DataSource.drones);

        public Drone GetDrone(int ID) => GetOneItem(DataSource.drones, ID);

        public Drone GetActualDrone(int ID) => GetActualOneItem(DataSource.drones, ID);

       

        public IEnumerable<Package> GetAllPackages() => GetAllItems(DataSource.packages);

        public IEnumerable<Package> GetAllNotAssignedPackages() =>
            GetAllItems(DataSource.packages, (Package p) => p.droneId == 0);

        public Package GetPackage(int ID) => GetOneItem(DataSource.packages, ID);

        public Package GetActualPackage(int ID) => GetActualOneItem(DataSource.packages, ID);

        public IEnumerable<Customer> GetAllCustomers() => GetAllItems(DataSource.customers);

        public Customer GetCustomer(int ID) => GetOneItem(DataSource.customers, ID);

        public Customer GetActualCustomer(int ID) => GetActualOneItem(DataSource.customers, ID);

        public IEnumerable<Station> GetAllStations() => GetAllItems(DataSource.stations);

        public IEnumerable<Station> GetAllUnoccupiedStations() =>
            GetAllItems(DataSource.stations, (Station ds) => ds.chargeSlots > 0);

        public Station GetStation(int ID) => GetOneItem(DataSource.stations, ID);

        public Station GetActualStation(int ID) => GetActualOneItem(DataSource.stations, ID);

        public IEnumerable<DroneCharge> GetAllCharges()
        {
            return (IEnumerable<DroneCharge>)DataSource.droneCharges.Select(dc => dc.Clone());
        }


    }
}
