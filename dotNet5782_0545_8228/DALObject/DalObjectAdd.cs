using System.Collections.Generic;
using DO;
using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        // Add objects section
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddEmployee(int ID, string password) =>
            AddDalItem(DataSource.employees, new Employee(ID, password));

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, package);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, customer);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone) =>
            AddDalItem(DataSource.drones, drone);

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, station);
    }
}
