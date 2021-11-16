using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class ABCDalObject : IDAL.IdalInterface
    {

        public void AddDrone() =>
            AddDalItem(DataSource.drones, DataSource.IdalDoType.Drone);

        public List<IDAL.DO.Drone> DisplayAllDrones() => DisplayAllItems(DataSource.drones);
        public List<IDAL.DO.Drone> DisplayDrone(int choice) => DisplayOneItem(DataSource.drones, choice);

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

                if (DataSource.drones[i].MaxWeight >= DataSource.parcels[choice].Weight)
                {
                    DataSource.parcels[choice].DroneId = DataSource.drones[i].ID;
                    DataSource.parcels[choice].Scheduled = DateTime.Now;
                    return;
                }
            }
            throw new IDAL.DO.DalObjectAccessException("Error, no available drones. Try again later.\n");
        }

        /// <summary>
        /// Updates the pickup time for the package after checking to make sure the parcel is assigned to a drone
        /// </summary>
        /// <param name="choice">index of the parcel</param>
        public void CollectPackageFromDrone(int choice)
        {
            if (choice < 0 || choice > DataSource.parcels.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            IDAL.DO.Drone currentDrone = GetParcelDrone(DataSource.parcels[choice]);
            if (currentDrone.Status == IDAL.DO.DroneStatuses.delivery)
            {
                DataSource.parcels[choice].PickedUp = DateTime.Now;
                return;
            }
            throw new IDAL.DO.DalObjectAccessException("Error, you must first assign the Drone before it can collect a package.\n");
        }

    }
}
