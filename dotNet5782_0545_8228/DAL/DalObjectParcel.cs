
using System;
using System.Collections.Generic;

namespace DalObject
{
    public partial class DalObject : IDAL.IdalInterface
    {
       

       

        // Update objects section

        /// <summary>
        /// Given a package we want the drone that is assigned to it
        /// </summary>
        /// <param name="package">parcel that we would like the drone</param>
        /// <returns>the drone that is assigned to the package</returns>
        internal IDAL.DO.Drone GetParcelDrone(IDAL.DO.Parcel package)
        {
            foreach (IDAL.DO.Drone drone in DataSource.drones)
            {
                if (drone.ID == package.DroneId)
                {
                    return drone;
                }
            }
            throw new IDAL.DO.DalObjectAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }
        
        
        
        public void AddParcel() =>
            AddDalItem(DataSource.parcels, DataSource.IdalDoType.Parcel);

        // Displaying all objects section

       
        public List<IDAL.DO.Parcel> DisplayAllParcels() => DisplayAllItems(DataSource.parcels);
        public List<IDAL.DO.Parcel> DisplayAllNotAssignedParcels() =>
            DisplayAllItems(DataSource.parcels, (IDAL.DO.Parcel p) => p.DroneId == 0);
        
       
        public List<IDAL.DO.Parcel> DisplayParcel(int choice) => DisplayOneItem(DataSource.parcels, choice);
       

       
    }
}
