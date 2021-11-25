﻿using System;
using System.Collections.Generic;
using IDAL.DO;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddDrone() =>
            AddDalItem(DataSource.drones, IdalDoType.DRONE);

        public void AddDrone(Drone drone) =>
            AddDalItem(DataSource.drones, drone, IdalDoType.DRONE);

        public List<Drone> GetAllDrones() => GetAllItems(DataSource.drones);

        public Drone GetDrone(int ID) => GetOneItem(DataSource.drones, ID);
        private Drone _GetDrone(int ID) => _GetOneItem(DataSource.drones, ID);

        /// <summary>
        /// Takes index of a parcel and assigns to next available drone which can support the parcel weight
        /// Updates the scheduled time.
        /// </summary>
        /// <param name="droneID">index of the package to assign</param>
        public void AssignPackageToDrone(int packageID, int droneID)
        {
            Package package = _GetPackage(packageID);
            package.scheduled = DateTime.Now;
            package.droneId = droneID;
        }

        /// <summary>
        /// Updates the pickup time for the package after checking to make sure the parcel is assigned to a drone
        /// </summary>
        /// <param name="packageID">index of the parcel</param>
        public void CollectPackageFromDrone(int packageID)
        {
            if (packageID < 0 || packageID > DataSource.packages.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            IDAL.DO.Drone currentDrone = GetParcelDrone(DataSource.packages[packageID]);
            
            DataSource.packages[packageID].pickedUp = DateTime.Now;
            
        }

    }
}
