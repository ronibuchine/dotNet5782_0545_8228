﻿using System;
using System.Collections.Generic;
using BL;

namespace IBL
{
    public interface IBLInterface
    {
        // Adder methods
        public Station AddStation(string name, Location location, int availableChargers);
        public Drone AddDrone(string model, WeightCategories maxWeight, int stationID);
        public Customer AddCustomer(string name, string phone, Location location);
        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority);

        // Update methods
        public void UpdateDrone(int ID, string newModel);
        public void UpdateStation(int stationID, string stationName);
        public void UpdateStation(int stationID, int numChargers);
        public void UpdateStation(int stationID, string stationName, int numChargers);
        public void UpdateCustomerName(int ID, string name);
        public void UpdateCustomerPhone(int ID, String phone);
        public void UpdateCustomer(int ID, string name, String phone);


        // Actions
        public void SendDroneToCharge(int droneID);
        public void ReleaseDroneFromCharge(int droneID, int hoursCharging);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);


        // Getters
        public Station GetStation(int ID);
        public Drone GetDrone(int ID);
        public Customer GetCustomer(int ID);
        public Package GetPackage(int ID);
        public IEnumerable<StationToList> GetStationList();
        public IEnumerable<DroneToList> GetDroneList();
        public IEnumerable<CustomerToList> GetCustomerList();
        public IEnumerable<PackageToList> GetPackageList();
        public IEnumerable<Package> GetUnassignedPackages();
        public IEnumerable<Station> GetAvailableStations();
        public IEnumerable<DroneToList> GetSpecificDrones(Func<DroneToList, bool> pred);

        // deleters
        public void DeleteCustomer(int ID);
        public void DeleteDrone(int ID);
        public void DeletePackage(int ID);
        public void DeleteStation(int ID);


    }
}
