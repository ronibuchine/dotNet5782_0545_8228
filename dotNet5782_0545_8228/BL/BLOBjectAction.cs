using System;
using System.Collections.Generic
using System.Linq;
using UTIL;
using IBL.BO;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public void SendDroneToCharge(int droneID)
        {
            IBL.BO.Drone drone = GetDrone(droneID);
            if (drone.status != IBL.BO.DroneStatuses.free) // is this always initialized?
                throw new IBL.BO.InvalidBlObjectException("Drone is not free currently");
            IBL.BO.Station closestAvailable = GetClosestStation(drone.currentLocation, GetAvailableStations());
            if (!CanArriveToLocation(drone, closestAvailable.location))
                throw new IBL.BO.InvalidBlObjectException("Drone does not have enough battery to reach closest available station");
            drone.status = IBL.BO.DroneStatuses.maintenance;
            dal.SendDroneToCharge(closestAvailable.ID, droneID);
        }

        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime)
        {
            IBL.BO.Drone drone = GetDrone(droneID);
            if (drone.status != IBL.BO.DroneStatuses.maintenance) // is this always initialized?
                throw new IBL.BO.InvalidBlObjectException("Drone is not currently in maintenance");
            drone.battery = DateTime.UtcNow.Hour * chargingRate;
            drone.status = IBL.BO.DroneStatuses.free;
            int stationID = dal.GetAllCharges().Find((dc) => dc.DroneId == droneID).StationId;
            dal.ReleaseDroneFromCharge(stationID, droneID);
        }

        public void AssignPackageToDrone(int droneID)
        {
            IBL.BO.Drone drone = GetDrone(droneID);
            if (drone.status != IBL.BO.DroneStatuses.free) // is this always initialized?
                throw new IBL.BO.InvalidBlObjectException("Drone is not free currently");
            
            IBL.BO.Package package = new(dal.GetAllPackages()
                .FindAll(p => p.weight <= (IDAL.DO.WeightCategories)drone.weightCategory)
                .OrderByDescending(p => p.priority)
                .ThenByDescending(p => p.weight)
                .ThenByDescending(p =>
                {
                    Location senderLocation = GetCustomer(p.senderId).currentLocation;
                    return Distances.GetDistance(senderLocation, drone.currentLocation);
                })
                .First());

            double distanceRequired = Distances.GetDistance(package.sender.currentLocation, drone.currentLocation) +
                 Distances.GetDistance(package.sender.currentLocation, package.receiver.currentLocation);
            double batteryRequired = GetConsumptionRate(drone.weightCategory) * distanceRequired;
            if (batteryRequired < 0 || batteryRequired > 100)
                throw new Exception("Oh shit"); // TODO debug this
            if (batteryRequired < drone.battery)
                // really in this case it should look for some lower priority but closer package
                throw new OperationNotPossibleException("package selected is too far away");
            drone.status = DroneStatuses.delivery;
            dal.AssignPackageToDrone(package.ID, droneID);
        }


        public void CollectPackage(int droneID)
        {
            throw new NotImplementedException();
        }
        public void DeliverPackage(int droneID)
        {
            throw new NotImplementedException();
        }

    }
}
