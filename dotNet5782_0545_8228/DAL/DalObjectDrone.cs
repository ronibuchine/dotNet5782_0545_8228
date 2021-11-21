using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddDrone() =>
            AddDalItem(DataSource.drones, IdalDoType.Drone);
        public void AddDrone(IDAL.DO.Drone drone) 
        {
            List<IDAL.DO.Drone> list = DataSource.drones;
            if (list.Count + 1 > list.Capacity)
            {
                list.Add(drone);
            }
            else
            {
                throw new IDAL.DO.DataSourceException();
            }
        }

        public List<IDAL.DO.Drone> GetAllDrones() => DisplayAllItems(DataSource.drones);
        public IDAL.DO.Drone GetDrone(int ID) => DisplayOneItem(DataSource.drones, ID);

        /// <summary>
        /// Takes index of a parcel and assigns to next available drone which can support the parcel weight
        /// Updates the scheduled time.
        /// </summary>
        /// <param name="choice">index of the package to assign</param>
        public void AssignPackageToDrone(int choice)
        {
            if (choice < 0 || choice > DataSource.drones.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < DataSource.drones.Count; i++)
            {

                if (DataSource.drones[i].MaxWeight >= DataSource.parcels[choice].weight)
                {
                    DataSource.parcels[choice].droneId = DataSource.drones[i].ID;
                    DataSource.parcels[choice].scheduled = DateTime.Now;
                    return;
                }
            }
            throw new IDAL.DO.DalObjectAccessException("Error, no available drones. Try again later.\n");
        }

        /// <summary>
        /// Updates the pickup time for the package after checking to make sure the parcel is assigned to a drone
        /// </summary>
        /// <param name="packageID">index of the parcel</param>
        public void CollectPackageFromDrone(int packageID)
        {
            if (packageID < 0 || packageID > DataSource.parcels.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            IDAL.DO.Drone currentDrone = GetParcelDrone(DataSource.parcels[packageID]);
            
            DataSource.parcels[packageID].pickedUp = DateTime.Now;
            
        }

    }
}
