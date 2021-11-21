
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
                if (drone.ID == package.droneId)
                {
                    return drone;
                }
            }
            throw new IDAL.DO.DalObjectAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }
        
        
        
        public void AddParcel() =>
            AddDalItem(DataSource.parcels, IdalDoType.Parcel);

        public void AddParcel(IDAL.DO.Parcel parcel)
        {
            List<IDAL.DO.Parcel> list = DataSource.parcels;
            if (list.Count + 1 > list.Capacity)
            {
                list.Add(parcel);
            }
            else
            {
                throw new IDAL.DO.DataSourceException();
            }
        }

        // Displaying all objects section

       
        public List<IDAL.DO.Parcel> GetAllParcels() => DisplayAllItems(DataSource.parcels);
        public List<IDAL.DO.Parcel> GetAllNotAssignedParcels() =>
            DisplayAllItems(DataSource.parcels, (IDAL.DO.Parcel p) => p.droneId == 0);
        
       
        public IDAL.DO.Parcel GetParcel(int ID) => DisplayOneItem(DataSource.parcels, ID);
       

       
    }
}
