using System.Collections.Generic;
using DO;
using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALObject : IDAL
    {

        //Regular getters -- These returns copies
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int ID) => GetOneItem(DataSource.customers, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int ID) => GetOneItem(DataSource.drones, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int ID) => GetOneItem(DataSource.packages, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int ID) => GetOneItem(DataSource.stations, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetEmployee(int ID) => GetOneItem(DataSource.employees, ID);



        // Actual Getters -- these return instances
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetActualCustomer(int ID) => GetActualOneItem(DataSource.customers, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetActualDrone(int ID) => GetActualOneItem(DataSource.drones, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetActualPackage(int ID) => GetActualOneItem(DataSource.packages, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetActualStation(int ID) => GetActualOneItem(DataSource.stations, ID);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetActualEmployee(int ID) => GetActualOneItem(DataSource.employees, ID);



        // Get all -- These return copies
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomers() => GetAllItems(DataSource.customers);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetAllDrones() => GetAllItems(DataSource.drones);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetAllCharges() => (IEnumerable<DroneCharge>)DataSource.droneCharges.ConvertAll(dc => dc.Clone());

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetAllPackages() => GetAllItems(DataSource.packages);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAllStations() => GetAllItems(DataSource.stations);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Employee> GetAllEmployees() => GetAllItems(DataSource.employees);



        // Get all conditionals -- these return copies
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetAllUnassignedPackages() => GetAllItems(DataSource.packages, (Package p) => p.droneId == 0);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAllUnoccupiedStations() => GetAllItems(DataSource.stations, (Station ds) => ds.chargeSlots > 0);

        // Some of these take predicates, some of them are pre-built for common queries
        /* public IEnumerable<Drone> GetAllDrones(Func<Drone, bool> pred) => GetAllDrones().Where(pred); */

        /* public IEnumerable<Customer> GetAllCustomers(Func<Customer, bool> pred) => GetAllCustomers().Where(pred); */

        /* public IEnumerable<Package> GetAllPackages(Func<Package, bool> pred) => GetAllPackages().Where(pred); */

        /* public IEnumerable<Station> GetAllStations(Func<Station, bool> pred) => GetAllStations().Where(pred); */



    }
}
