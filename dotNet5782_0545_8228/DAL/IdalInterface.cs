using System.Collections.Generic;

namespace IDAL
{
    public interface IdalInterface
    {
        public void AddDrone();
        public void AddStation();
        public void AddCustomer();
        public void AddPackage();
        public void AddDrone(DO.Drone drone);
        public void AddStation(DO.Station droneStation);
        public void AddCustomer(DO.Customer customer);
        public void AddPackage(DO.Package package);

        public List<DO.Drone> GetAllDrones();
        public List<DO.Station> GetAllStations();
        public List<DO.Customer> GetAllCustomers();
        public List<DO.Package> GetAllPackages();
        public List<DO.DroneCharge> GetAllCharges();
        public List<DO.Package> GetAllNotAssignedPackages();
        public List<DO.Station> GetAllUnoccupiedStations();

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
    }
}
