using System.Collections.Generic;

namespace DALAPI
{
    public interface IDAL
    {
        public void AddDrone();
        public void AddStation();
        public void AddCustomer();
        public void AddPackage();
        public void AddDrone(DO.Drone drone);
        public void AddStation(DO.Station droneStation);
        public void AddCustomer(DO.Customer customer);
        public void AddPackage(DO.Package package);

        public IEnumerable<DO.Drone> GetAllDrones();
        public IEnumerable<DO.Station> GetAllStations();
        public IEnumerable<DO.Customer> GetAllCustomers();
        public IEnumerable<DO.Package> GetAllPackages();
        public IEnumerable<DO.DroneCharge> GetAllCharges();
        public IEnumerable<DO.Package> GetAllNotAssignedPackages();
        public IEnumerable<DO.Station> GetAllUnoccupiedStations();

        public DO.Drone GetDrone(int ID);
        public DO.Station GetStation(int ID);
        public DO.Customer GetCustomer(int ID);
        public DO.Package GetPackage(int ID);

        public DO.Drone GetActualDrone(int ID);
        public DO.Station GetActualStation(int ID);
        public DO.Customer GetActualCustomer(int ID);
        public DO.Package GetActualPackage(int ID);

        public void AssignPackageToDrone(int packageID, int droneID);
        public void CollectPackageToDrone(int packageID);
        public void ProvidePackageToCustomer(int packageID);
        public void SendDroneToCharge(int stationID, int droneID);
        public void ReleaseDroneFromCharge(int stationID, int droneID);

        public double[] PowerConsumptionRequest();
        public int GetNextID();

        public void Clear();
    }
}
