using System;
using System.Collections.Generic;

namespace DALAPI
{
    public interface IDAL
    {
        // Add methods
        public void AddDrone();
        public void AddStation();
        public void AddCustomer();
        public void AddPackage();
        public void AddDrone(DO.Drone drone);
        public void AddStation(DO.Station droneStation);
        public void AddCustomer(DO.Customer customer);
        public void AddPackage(DO.Package package);

        // Update methods
        public void UpdateDrone(int ID, string newModel);
        public void UpdateStation(int stationID, string stationName); 
        public void UpdateStation(int stationID, int numChargers);
        public void UpdateStation(int stationID, string stationName, int numChargers);
        public void UpdateCustomerName(int ID, string name);
        public void UpdateCustomerPhone(int ID, string phone);
        public void UpdateCustomer(int ID, string name, string phone);

        // Get alls
        public IEnumerable<DO.Drone> GetAllDrones();
        public IEnumerable<DO.Station> GetAllStations();
        public IEnumerable<DO.Customer> GetAllCustomers();
        public IEnumerable<DO.Package> GetAllPackages();
        public IEnumerable<DO.DroneCharge> GetAllCharges();
        public IEnumerable<DO.Package> GetAllUnassignedPackages();
        public IEnumerable<DO.Station> GetAllUnoccupiedStations();

        //Getters
        public DO.Drone GetDrone(int ID);
        public DO.Station GetStation(int ID);
        public DO.Customer GetCustomer(int ID);
        public DO.Package GetPackage(int ID);

        // Get actuals
        public DO.Drone GetActualDrone(int ID);
        public DO.Station GetActualStation(int ID);
        public DO.Customer GetActualCustomer(int ID);
        public DO.Package GetActualPackage(int ID);

        // Get all conditionals
        public IEnumerable<DO.Drone> GetAllDrones(Func<DO.Drone, bool> pred);
        public IEnumerable<DO.Station> GetAllStations(Func<DO.Station, bool> pred);
        public IEnumerable<DO.Customer> GetAllCustomers(Func<DO.Customer, bool> pred);
        public IEnumerable<DO.Package> GetAllPackages(Func<DO.Package, bool> pred);

        // Actions
        public void AssignPackageToDrone(int packageID, int droneID);
        public void CollectPackageToDrone(int packageID);
        public void ProvidePackageToCustomer(int packageID);
        public void SendDroneToCharge(int stationID, int droneID);
        public void ReleaseDroneFromCharge(int stationID, int droneID);

        // deleters
        public void DeleteCustomer(int ID);
        public void DeleteDrone(int ID);
        public void DeletePackage(int ID);
        public void DeleteStation(int ID);

        // misc
        public double[] PowerConsumptionRequest();
        public int GetNextID();
        public void Clear();
    }
}
