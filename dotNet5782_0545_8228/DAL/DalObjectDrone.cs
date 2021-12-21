using System;
using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        public void AddDrone() =>
            AddDalItem((List<Drone>)DataSource.drones, DataSource.MAX_DRONES, IdalDoType.DRONE);

        public void AddDrone(Drone drone) =>
            AddDalItem((List<Drone>)DataSource.drones,DataSource.MAX_DRONES, drone, IdalDoType.DRONE);

        public IEnumerable<Drone> GetAllDrones() => GetAllItems(DataSource.drones);

        public Drone GetDrone(int ID) => GetOneItem(DataSource.drones, ID);

        public Drone GetActualDrone(int ID) => GetActualOneItem(DataSource.drones, ID);

        /// <summary>
        /// Takes index of a parcel and assigns to next available drone which can support the parcel weight
        /// Updates the scheduled time.
        /// </summary>
        /// <param name="droneID">index of the package to assign</param>
        public void AssignPackageToDrone(int packageID, int droneID)
        {
            Package package = GetActualPackage(packageID);
            package.scheduled = DateTime.Now;
            package.droneId = droneID;
        }

        /// <summary>
        /// Updates the pickup time for the package to now
        /// </summary>
        /// <param name="packageID">index of the package</param>
        public void CollectPackageToDrone(int packageID)
        {
            GetActualPackage(packageID).pickedUp = DateTime.Now;
        }

    }
}
