using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {
        public void DeleteCustomer(int ID) => GetActualCustomer(ID).IsActive = false;

        public void DeleteDrone(int ID) => GetActualDrone(ID).IsActive = false;

        public void DeletePackage(int ID) => GetActualPackage(ID).IsActive = false;

        public void DeleteStation(int ID) => GetActualStation(ID).IsActive = false;
        public void DeleteEmployee(int ID) => GetActualEmployee(ID).IsActive = false;
    }
}
