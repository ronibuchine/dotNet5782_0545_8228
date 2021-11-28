using System;
using System.Collections.Generic;
using IBL.BO;

namespace IBL
{
    public interface IBLInterface
    {
        // Adder methods
        public Station AddBaseStation(string name, Location location, int availableChargers);
        public Drone AddDrone(string model, WeightCategories maxWeight, int stationID);
        public Customer AddCustomer(string name, string phone, Location location);
        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority);

        // Update methods
        public void UpdateDrone(int ID, string newModel);
        public void UpdateStation(int stationID, string stationName); // either one of the last two parameters must be entered or both of them
        public void UpdateStation(int stationID, int numChargers);
        public void UpdateStation(int stationID, string stationName, int numChargers);
        public void UpdateCustomerName(int ID, string name);
        public void UpdateCustomerPhone(int ID, String phone);
        public void UpdateCustomer(int ID, string name, String phone);


        // Actions
        public void SendDroneToCharge(int droneID);
        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);


        // Display
        public Station GetBaseStation(int ID);
        public Drone GetDrone(int ID);
        public Customer GetCustomer(int ID);
        public Package GetPackage(int ID);
        public List<BaseStationToList> GetStationList();
        public List<DroneToList> GetDroneList();
        public List<CustomerToList> GetCustomerList();
        public List<PackageToList> GetPackageList();
        public List<PackageToList> GetUnassignedPackages();
        public List<Station> GetAvailableStations();

    }
}
