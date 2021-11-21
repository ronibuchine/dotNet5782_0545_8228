using System.Collections.Generic;

namespace IDAL
{
    public interface IdalInterface
    {
        public void AddDrone();
        public void AddDroneStation();
        public void AddCustomer();
        public void AddParcel();
        public void AddDrone(IDAL.DO.Drone drone);
        public void AddDroneStation(IDAL.DO.DroneStation droneStation);
        public void AddCustomer(IDAL.DO.Customer customer);
        public void AddParcel(IDAL.DO.Parcel parcel);

        public List<IDAL.DO.Drone> GetAllDrones();
        public List<IDAL.DO.DroneStation> GetAllDroneStations();
        public List<IDAL.DO.Customer> GetAllCustomers();
        public List<IDAL.DO.Parcel> GetAllParcels();
        public List<IDAL.DO.DroneCharge> GetAllCharges();
        public List<IDAL.DO.Parcel> GetAllNotAssignedParcels();
        public List<IDAL.DO.DroneStation> GetAllUnoccupiedStations();


        public IDAL.DO.Drone GetDrone(int ID);
        public IDAL.DO.DroneStation GetDroneStation(int ID);
        public IDAL.DO.Customer GetCustomer(int ID);
        public IDAL.DO.Parcel GetParcel(int ID);


        public void AssignPackageToDrone(int packageID);
        public void CollectPackageFromDrone(int packageID);
        public void ProvidePackageToCustomer(int packageID);
        public void SendDroneToCharge(int stationID, int droneID);
        public void ReleaseDroneFromCharge(int stationID, int droneID);

        // Should this have any paramters?
        public double[] PowerConsumptionRequest();
    }
}
