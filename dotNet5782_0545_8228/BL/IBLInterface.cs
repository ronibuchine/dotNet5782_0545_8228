using System;
using System.Collections.Generic;
using IBL.BO;

namespace IBL
{
    public interface IBLInterface
    {
        // Adder methods
        public void AddBaseStation(int stationID, string name, Location location, int availableChargers);
        public void AddDrone(int ID, string model, WeightCategories maxWeight, int stationID);
        public void NewCustomer(int customerID, string name, int phone, Location location);
        public void NewPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority);

        // Update methods
        public void UpdateDrone(int ID, string newModel);
        public void UpdateStation(int stationID, string stationName); // either one of the last two parameters must be entered or both of them
        public void UpdateStation(int stationID, int numChargers);
        public void UpdateStation(int stationID, string stationName, int numChargers);
        public void UpdateCustomer(int ID, string name);
        public void UpdateCustomer(int ID, int phone);
        public void UpdateCustomer(int ID, string name, int phone);


        // Actions
        public void SendDroneToCharge(int droneID);
        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);


        // Display
        public DroneStation DisplayBaseStation();
        public Drone DisplayDrone();
        public Customer DisplayCustomer();
        public Package DisplayPackage();
        public List<DroneStation> DisplayStationList();
        public List<DroneToList> DisplayDroneList();
        public List<CustomerToList> DisplayCustomerList();
        public List<PackageToList> DisplayPackageList();
        public List<PackageToList> DisplayUnassignedPackages();
        public List<BaseStationToList> DisplayAvailableStations();

    }
}
