using System;
using DO;
using DALAPI;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DalObject : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            return DataSource.employees.Find(e => ID == e.ID && password == e.password && e.IsActive) != null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool VerifyCustomerCredentials(int ID, string password)
        {
            return DataSource.customers.Find(e => ID == e.ID && password == e.password && e.IsActive) != null;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ProvidePackageToCustomer(int packageID)
        {
            GetActualPackage(packageID).delivered = DateTime.Now;
        }

        // This only removes the droncharge from the list. It does not calculate time charged!
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            var temp = DataSource.droneCharges.ToList();
            temp.Remove(temp.Find(dc => dc.droneId == droneID));
            DataSource.droneCharges = temp;
            GetActualStation(stationID).chargeSlots++;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CollectPackageToDrone(int packageID)
        {
            GetActualPackage(packageID).pickedUp = DateTime.Now;
        }
    }
}

