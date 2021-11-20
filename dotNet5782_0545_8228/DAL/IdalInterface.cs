using System.Collections.Generic;

namespace IDAL
{
    public interface IdalInterface
    {
        public void AddDrone();
        public void AddDroneStation();
        public void AddCustomer();
        public void AddParcel();

        public List<IDAL.DO.Drone> DisplayAllDrones();
        public List<IDAL.DO.DroneStation> DisplayAllDroneStations();
        public List<IDAL.DO.Customer> DisplayAllCustomers();
        public List<IDAL.DO.Parcel> DisplayAllParcels();
        public List<IDAL.DO.Parcel> DisplayAllNotAssignedParcels();
        public List<IDAL.DO.DroneStation> DisplayAllUnoccupiedStations();


        public List<IDAL.DO.Drone> DisplayDrone(int choice);
        public List<IDAL.DO.DroneStation> DisplayDroneStation(int choice);
        public List<IDAL.DO.Customer> DisplayCustomer(int choice);
        public List<IDAL.DO.Parcel> DisplayParcel(int choice);

        public void AssignPackageToDrone(int choice);
        public void CollectPackageFromDrone(int choice);
        public void ProvidePackageToCustomer(int choice);
        public void SendDroneToCharge(int stationChoice, int droneChoice);
        public void ReleaseDroneFromCharge(int stationChoice, int droneChoice);

        // Should this have any paramters?
        public double[] PowerConsumptionRequest();
    }
}
