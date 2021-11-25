using System.Collections.Generic;

namespace IDAL
{
    public interface IdalInterface
    {
        public void AddDrone();
        public void AddStation();
        public void AddCustomer();
        public void AddPackage();
        public void AddDrone(IDAL.DO.Drone drone);
        public void AddStation(IDAL.DO.Station droneStation);
        public void AddCustomer(IDAL.DO.Customer customer);
        public void AddPackage(IDAL.DO.Package package);

        public List<IDAL.DO.Drone> GetAllDrones();
        public List<IDAL.DO.Station> GetAllStations();
        public List<IDAL.DO.Customer> GetAllCustomers();
        public List<IDAL.DO.Package> GetAllPackages();
        public List<IDAL.DO.DroneCharge> GetAllCharges();
        public List<IDAL.DO.Package> GetAllNotAssignedPackages();
        public List<IDAL.DO.Station> GetAllUnoccupiedStations();

        public IDAL.DO.Drone GetDrone(int ID);
        public IDAL.DO.Station GetStation(int ID);
        public IDAL.DO.Customer GetCustomer(int ID);
        public IDAL.DO.Package GetPackage(int ID);

        public void AssignPackageToDrone(int packageID);
        public void CollectPackageFromDrone(int packageID);
        public void ProvidePackageToCustomer(int packageID);
        public void SendDroneToCharge(int stationID, int droneID);
        public void ReleaseDroneFromCharge(int stationID, int droneID);

        public double[] PowerConsumptionRequest();
        public int GetNextID();
    }
}
