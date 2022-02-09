
using DO;
using DALAPI;

namespace DAL
{
    public partial class DALObject : IDAL
    {
        // Add objects section
        public void AddEmployee(int ID, string password) =>
            AddDalItem(DataSource.employees, new Employee(ID, password));

        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, package);

        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, customer);

        public void AddDrone(Drone drone) =>
            AddDalItem(DataSource.drones, drone);

        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, station);
    }
}
