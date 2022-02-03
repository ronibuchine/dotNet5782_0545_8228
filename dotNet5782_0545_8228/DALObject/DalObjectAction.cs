﻿using System;
using DO;
using DALAPI;
using System.Linq;

namespace DAL
{
    public partial class DALObject : IDAL
    {
        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            return DataSource.employees.Find(e => ID == e.ID && password == e.password && e.IsActive) != null;
        }

        public bool VerifyCustomerCredentials(int ID, string password)
        {
            return DataSource.customers.Find(e => ID == e.ID && password == e.password && e.IsActive) != null;
        }



        public void ProvidePackageToCustomer(int packageID)
        {
            GetActualPackage(packageID).delivered = DateTime.Now;
        }

        // This only removes the dronecharge from the list. It does not calculate time charged!
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            var removeMe = DataSource.droneCharges.Find(dc => dc.droneId == droneID);
            DataSource.droneCharges.Remove(removeMe);
            GetActualStation(stationID).chargeSlots++;
        }

        public void SendDroneToCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Add(new DroneCharge(droneID, stationID, DateTime.Now));
            GetActualStation(stationID).chargeSlots--;
        }

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

