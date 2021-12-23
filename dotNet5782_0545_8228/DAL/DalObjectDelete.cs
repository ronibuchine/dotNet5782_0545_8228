using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {
        public void DeleteCustomer(int ID) => DataSource.customers.RemoveAll(c => c.ID == ID);

        public void DeleteDrone(int ID) => DataSource.drones.RemoveAll(d => d.ID == ID);

        public void DeletePackage(int ID) => DataSource.packages.RemoveAll(p => p.ID == ID);

        public void DeleteStation(int ID) => DataSource.stations.RemoveAll(s => s.ID == ID);
    }
}
