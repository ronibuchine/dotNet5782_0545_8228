using System;
using System.Collections.Generic;
using BL;

namespace IBL
{
    public interface IBLInterface
    {
        /// <summary>
        /// API call which adds a station to the system.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="availableChargers"></param>
        /// <returns>A reference to the station that was added.</returns>
        public Station AddStation(string name, Location location, int availableChargers);

        /// <summary>
        /// API call which adds a drone to the system.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="maxWeight"></param>
        /// <param name="stationID"></param>
        /// <returns>A reference to the drone which was just added</returns>
        public Drone AddDrone(string model, WeightCategories maxWeight, int stationID);


        /// <summary>
        /// API call which adds a customer to the system.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="location"></param>
        /// <param name="ID"></param>
        /// <returns>A reference to the customer that was added.</returns>
        /// <param name="password"></param>
        public Customer AddCustomer(string name, string phone, Location location, int ID, string password = null);

        /// <summary>
        /// API call which adds a new package to the system.
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="receiverID"></param>
        /// <param name="weight"></param>
        /// <param name="priority"></param>
        /// <returns>A reference to the package that was added.</returns>
        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority);


        public void AddEmployee(int ID, string password);

        /// <summary>
        /// Updates a drone entity with a new model name
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="newModel"></param>
        public void UpdateDrone(int ID, string newModel);

        /// <summary>
        /// Updates a Station entity with a new station name
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="stationName"></param>
        public void UpdateStation(int stationID, string stationName);

        /// <summary>
        /// Updates a station entity with a new amount of chargers. If the number of chargers is decreased it may throw an exception if there are drones currently charging there.
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="numChargers"></param>
        public void UpdateStation(int stationID, int numChargers);

        /// <summary>
        /// Updates both the name and number of chargers at a station
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="stationName"></param>
        /// <param name="numChargers"></param>
        public void UpdateStation(int stationID, string stationName, int numChargers);

        /// <summary>
        /// Updates the customers name
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        public void UpdateCustomerName(int ID, string name);

        /// <summary>
        /// Updates the customers phone number
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="phone"></param>
        public void UpdateCustomerPhone(int ID, String phone);


        /// <summary>
        /// Updates the customers password
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        public void UpdateCustomerPassword(int ID, string password);

        /// <summary>
        /// Updates both the name and phone number of a given customer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="password">defaults to null if not passed</param>
        public void UpdateCustomer(int ID, string name, string phone, string password = null);


        /// <summary>
        /// This API call will send the given drone to the nearest charging station assuming that drone is able to make the journey.
        /// This will fail if the drone isn't free or it doesn't have enough battery.
        /// </summary>
        /// <param name="droneID">ID of the drone to send</param>
        public void SendDroneToCharge(int droneID);

        /// <summary>
        /// This API call will release a specified drone from charging. It is released after a certain amount of seconds which is specified by the user of the system.
        /// This call throws exceptions when the drone doesn't have the correct status, i.e. not in maintenance.
        /// </summary>
        /// <param name="droneID">the drone that is currently in charging to be released.</param>
        public void ReleaseDroneFromCharge(int droneID);

        /// <summary>
        /// This API call will assign the best possible package to the drone that is supplied to the function call.
        /// Packages are assigned in order of suitability in terms of weight and priority.
        /// It throws an exception in the event that there is no suitable package or the drone cannot currently be assigned a package.
        /// </summary>
        /// <param name="droneID">The drone ID to assign to.</param>
        public void AssignPackageToDrone(int droneID);

        /// <summary>
        /// This API call will allow the drone to collect the package it is assigned to.
        /// This call will fail in the event that the drone isn't currently assigned a package or it does not have enough battery to reach the sender.
        /// </summary>
        /// <param name="droneID">The drone to collect the package</param>
        public void CollectPackage(int droneID);

        /// <summary>
        /// This API call will deliver a package to the customer the package is intended for.
        /// This call fails when the package hasn't been collect4ed yet, the drone does not have enough battery or the drone is not in the correct status.
        /// </summary>
        /// <param name="droneID">the drone to deliver the package</param>
        public void DeliverPackage(int droneID);

        /// <summary>
        /// This API call will verify if the employee inputted a valid ID and password
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>a boolean based on whether or not it is verified</returns>
        public bool VerifyEmployeeCredentials(int ID, string password);

        /// <summary>
        /// This API call will verify if the customer inputted a valid ID and password
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>a boolean based on whether or not it is verified</returns>
        public bool VerifyCustomerCredentials(int ID, string password);


        /// <summary>
        /// API call which retrieves a specified station entity.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a station</returns>
        public Station GetStation(int ID);

        /// <summary>
        /// API call which retrieves a specified drone entity.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a drone</returns>
        public Drone GetDrone(int ID);

        /// <summary>
        /// API call which gets the specified customer.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> a customer</returns>
        public Customer GetCustomer(int ID);

        /// <summary>
        /// API call which retrieves a specified package via ID number
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a package</returns>
        public Package GetPackage(int ID);

        /// <summary>
        /// API call which tells whether or not an employee exists in the system
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a boolean value based on whether or not the employee exists</returns>
        public bool GetEmployee(int ID);


        /// <summary>
        /// Retrieves all stations in ToList form.
        /// </summary>
        /// <returns>All stations</returns>
        public IEnumerable<StationToList> GetStationList();

        /// <summary>
        /// Retrieves all drones in ToList form
        /// </summary>
        /// <returns>All drones</returns>
        public IEnumerable<DroneToList> GetDroneList();

        /// <summary>
        /// Retrieves all Customers in ToList form.
        /// </summary>
        /// <returns>All customers</returns>
        public IEnumerable<CustomerToList> GetCustomerList();

        /// <summary>
        /// Retrieves all packages in ToList form
        /// </summary>
        /// <returns> All packages</returns>
        public IEnumerable<PackageToList> GetPackageList();

        /// <summary>
        /// Retrieves all packages which aren't currently assigned to a Drone
        /// </summary>
        /// <returns>Packages that have not been assigned to drones</returns>
        public IEnumerable<Package> GetUnassignedPackages();

        /// <summary>
        /// Retrieves a list of all stations which are available
        /// </summary>
        /// <returns>Stations with available charge slots</returns>
        public IEnumerable<Station> GetAvailableStations();

        /// <summary>
        /// Retrieves a list of drones which fit some predicate function.
        /// </summary>
        /// <param name="pred"></param>
        /// <returns>drones that match the given predicate</returns>
        public IEnumerable<DroneToList> GetSpecificDrones(Func<DroneToList, bool> pred);

        ///<summary>
        ///Delete a customer
        ///</summary>
        ///<param name="ID">
        ///The ID of the customer to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///If there is a package in the system that needs to be delivered to the customer
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the customer does not exist in the system
        ///</exception>
        public void DeleteCustomer(int ID);

        ///<summary>
        ///Delete a drone
        ///</summary>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///If the drone is in delivery status it cannot be deleted
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the drone does not exist in the system
        ///</exception>
        public void DeleteDrone(int ID);

        ///<summary>
        ///Delete a package
        ///</summary>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///The package can only be deleted if it has not be assigned yet, or if has already been delivered
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the package does not exist in the system
        ///</exception>
        public void DeletePackage(int ID);

        ///<summary>
        ///Delete a package
        ///</summary>
        ///<remarks>
        ///Be very careful when calling this.There may be drones that are counting on this station in the future and will be stranded if the station dissapears
        ///</remarks>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///The station can only be deleted if there are no drones charging on it
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the package does not exist in the system
        ///</exception>
        public void DeleteStation(int ID);

        /// <summary>
        /// This API call deactivates an employee account in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteEmployee(int ID);


    }
}
