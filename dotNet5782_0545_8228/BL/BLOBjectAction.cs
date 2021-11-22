using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBject
{
    /// <summary>
    /// The Main function which displays the menu to the user in the console interface
    /// </summary>
    /// <param name="args">arguments passed to the CLI</param>
    public partial class BLOBject : IBL.IBLInterface
    {
        public void SendDroneToCharge(int droneID)
        {
            IBL.BO.Drone drone = GetDrone(droneID);
            if (drone.status != IBL.BO.DroneStatuses.free) // is this always initialized?
            {
                throw new IBL.BO.InvalidBlObjectException("Drone is not free currently");
            }
            IBL.BO.DroneStation closestAvailable = GetClosestStation(drone.currentLocation, GetAvailableStations());
            if (!CanArriveToLocation(drone, closestAvailable.location))
            {
                throw new IBL.BO.InvalidBlObjectException("Drone does not have enough battery to reach closest available station");
            }
            drone.status = IBL.BO.DroneStatuses.maintenance;
            dal.SendDroneToCharge(closestAvailable.ID, droneID);
        }

        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime)
        {
            IBL.BO.Drone drone = GetDrone(droneID);

        }
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);

    }
}
