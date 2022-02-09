using System.Collections.Generic;

namespace DALAPI
{
    public interface IDAL
    {
        /// <summary>
        /// This issues an API call which adds a new employee to the DataSource
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        public void AddEmployee(int ID, string password);

        /// <summary>
        /// This issues an API call which adds a new drone to the DataSource.
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(DO.Drone drone);

        /// <summary>
        /// This issues an API call which adds a new station to the DataSource.
        /// </summary>
        /// <param name="droneStation"></param>
        public void AddStation(DO.Station droneStation);

        /// <summary>
        /// This issues an API call which adds a new customer to the DataSource.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(DO.Customer customer);

        /// <summary>
        /// This issues an API call which adds a new package to the DataSource.
        /// </summary>
        /// <param name="package"></param>
        public void AddPackage(DO.Package package);

        /// <summary>
        /// This API call will update a given drone and changes it's model to the string that was passed.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="newModel"></param>
        public void UpdateDrone(int ID, string newModel);

        /// <summary>
        /// This API call will update a given station and changes it's name to the string that was passed.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="stationName"></param>
        public void UpdateStation(int stationID, string stationName);

        /// <summary>
        /// This API call will update a given station and changes the number of charging slots it has.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="numChargers"></param>
        public void UpdateStation(int stationID, int numChargers);

        /// <summary>
        /// This API call will both update the number of chargers and the station name of a given station
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="stationName"></param>
        /// <param name="numChargers"></param>
        public void UpdateStation(int stationID, string stationName, int numChargers);

        /// <summary>
        /// This API call will update the name of a given customer in the DataSource
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        public void UpdateCustomerName(int ID, string name);

        /// <summary>
        /// This API call will update a customer phone number in the DataSource
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="phone"></param>
        public void UpdateCustomerPhone(int ID, string phone);

        /// <summary>
        /// This API call will update a customer password in the DataSource
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="phone"></param>
        public void UpdateCustomerPassword(int ID, string password);

        /// <summary>
        /// This API call will update both the name and phone number of a customer in the DataSource
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int ID, string name, string phone, string password = null);

        /// <summary>
        /// This API call will return all the active drones which are in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Drone> GetAllDrones();

        /// <summary>
        /// This API call will return all the active stations which are tin the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStations();

        /// <summary> 
        /// This API call will return all the active customers in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Customer> GetAllCustomers();

        /// <summary>
        /// This API call will return all the active packages in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Package> GetAllPackages();

        /// <summary>
        /// This API call will return all the DroneCharge pairs in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.DroneCharge> GetAllCharges();

        /// <summary>
        /// This API call will return all of the Employees in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Employee> GetAllEmployees();

        /// <summary>
        /// This API call will retrieve all the unassigned packages in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Package> GetAllUnassignedPackages();

        /// <summary>
        /// This API call will retrieve all the unoccupied stations in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllUnoccupiedStations();


        /// <summary>
        /// This API call retrieves a drone basedon it's ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>A copy of the drone object</returns>
        public DO.Drone GetDrone(int ID);

        /// <summary>
        /// This API call retrieves a given station based on it's ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a copy of the station object</returns>
        public DO.Station GetStation(int ID);

        /// <summary>
        /// This API call retrieves a given customer based on it's ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a copy of the customer object</returns>
        public DO.Customer GetCustomer(int ID);

        /// <summary>
        /// This API call retrieves a given package based on it's ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>A copy of the package object</returns>
        public DO.Package GetPackage(int ID);

        /// <summary>
        /// This API call retrieves a given employee based on it's ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>A copy of the employee object</returns>
        public DO.Employee GetEmployee(int ID);

        /* /// <summary> */
        /* /// This API call retrieves a drone based on it's ID */
        /* /// </summary> */
        /* /// <param name="ID"></param> */
        /* /// <returns>A reference to the actual drone</returns> */
        /* public DO.Drone GetActualDrone(int ID); */

        /* /// <summary> */
        /* /// This API call retrieves a station from the system */
        /* /// </summary> */
        /* /// <param name="ID"></param> */
        /* /// <returns>A referecne to the actual station</returns> */
        /* public DO.Station GetActualStation(int ID); */

        /* /// <summary> */
        /* /// This API call retrieves a customer from the system */
        /* /// </summary> */
        /* /// <param name="ID"></param> */
        /* /// <returns>A reference of the actual customer</returns> */
        /* public DO.Customer GetActualCustomer(int ID); */

        /* /// <summary> */
        /* /// This API call retrieves a package from the system */
        /* /// </summary> */
        /* /// <param name="ID"></param> */
        /* /// <returns>A reference to the actual package</returns> */
        /* public DO.Package GetActualPackage(int ID); */

        /* /// <summary> */
        /* /// This API call retrieves the employee from the system */
        /* /// </summary> */
        /* /// <param name="ID"></param> */
        /* /// <returns>A reference to the actual employee</returns> */
        /* public DO.Employee GetActualEmployee(int ID); */

        /// <summary>
        /// This API call will assign a given package to the specified drone in the system if it can
        /// This may throw an Exception on failure
        /// </summary>
        /// <param name="packageID"></param>
        /// <param name="droneID"></param>
        public void AssignPackageToDrone(int packageID, int droneID);

        /// <summary>
        /// This API call collects a package for a drone.
        /// This may throw an Exception upon failure
        /// </summary>
        /// <param name="packageID"></param>
        public void CollectPackageToDrone(int packageID);

        /// <summary>
        /// This API call Will deliver the package to the customer it is meant for.
        /// This may throw an exception upon failure
        /// </summary>
        /// <param name="packageID"></param>
        public void ProvidePackageToCustomer(int packageID);

        /// <summary>
        /// This API call sends a drone to charge at the closest available station.
        /// This may throw an exception upon failure
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="droneID"></param>
        public void SendDroneToCharge(int stationID, int droneID);

        /// <summary>
        /// This API call will release a drone from charging at a station.
        /// This may throw an exception upon failure
        /// </summary>
        /// <param name="stationID"></param>
        /// <param name="droneID"></param>
        public void ReleaseDroneFromCharge(int stationID, int droneID);

        /// <summary>
        /// This API call verifies the credentials of a given employee
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>a boolean value based on whether or not it was verified</returns>
        public bool VerifyEmployeeCredentials(int ID, string password);


        /// <summary>
        /// This API call verifies the credentials of a given customer
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="password"></param>
        /// <returns>a boolean value based on whether or not it was verified</returns>
        public bool VerifyCustomerCredentials(int ID, string password);

        /// <summary>
        /// This retrieves the power consumption of the different weight classes
        /// </summary>
        /// <returns></returns>
        public double[] PowerConsumptionRequest();

        /// <summary>
        /// Marks a customer as inactive in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteCustomer(int ID);

        /// <summary>
        /// Marks a Drone as inactive in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteDrone(int ID);

        /// <summary>
        /// Marks a package as inactive in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeletePackage(int ID);

        /// <summary>
        /// Marks a station as inactive in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteStation(int ID);

        /// <summary>
        /// Marks an employee as inactive in the system
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteEmployee(int ID);

        /// <summary>
        /// Get next ID of current running ID's
        /// </summary>
        public int GetNextID();


        /// <summary>
        /// Delete entire contents of dal
        /// </summary>
        public void Clear();
    }
}
