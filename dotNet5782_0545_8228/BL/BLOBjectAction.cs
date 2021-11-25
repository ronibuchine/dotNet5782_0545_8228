using System;
using System.Linq;
using UTIL;
using IBL.BO;

namespace BLOBjectNamespace
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public void SendDroneToCharge(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new InvalidBlObjectException("Drone is not free currently");
            Station closestAvailable = GetClosestStation(drone.currentLocation, GetAvailableStations());
            if (!CanArriveToLocation(drone, closestAvailable.location))
                throw new InvalidBlObjectException("Drone does not have enough battery to reach closest available station");
            drone.status = DroneStatuses.maintenance;
            dal.SendDroneToCharge(closestAvailable.ID, droneID);
        }

        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.maintenance) // is this always initialized?
                throw new InvalidBlObjectException("Drone is not currently in maintenance");
            drone.battery = DateTime.UtcNow.Hour * chargingRate;
            drone.status = DroneStatuses.free;
            int stationID = dal.GetAllCharges().Find((dc) => dc.DroneId == droneID).StationId;
            dal.ReleaseDroneFromCharge(stationID, droneID);
        }

        public void AssignPackageToDrone(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new InvalidBlObjectException("Drone is not free currently");
            
            Package package = new(dal.GetAllPackages()
                .FindAll(p => p.weight <= (IDAL.DO.WeightCategories)drone.weightCategory)
                .OrderByDescending(p => p.priority)
                .ThenByDescending(p => p.weight)
                .ThenBy(p =>
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
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new InvalidBlObjectException("Drone is not delivering currently");
            IDAL.DO.Package dalPackage = dal.GetAllPackages().Find(p => p.droneId == droneID);
            Customer sender = new Customer(dal.GetCustomer(dalPackage.senderId));
            double distanceTraveled = Distances.GetDistance(sender.currentLocation, drone.currentLocation);
            drone.battery -= distanceTraveled * GetConsumptionRate(drone.weightCategory);
            if (drone.battery < 0 || drone.battery > 100)
                throw new Exception("Oh shit"); // TODO debug this
            drone.currentLocation = sender.currentLocation;
            dal.CollectPackageToDrone(dalPackage.ID);
        }

        public void DeliverPackage(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new InvalidBlObjectException("Drone is not delivering currently");
            IDAL.DO.Package dalPackage = dal.GetAllPackages().Find(p => p.droneId == droneID);
            // not a fan of this comparison. Not precise at all
            if (DateTime.Compare(dalPackage.pickedUp, DateTime.MinValue) == 0)
                throw new InvalidBlObjectException("package has not yet been collected");
            Customer reciever = new Customer(dal.GetCustomer(dalPackage.recieverId));
            double distanceTraveled = Distances.GetDistance(reciever.currentLocation, drone.currentLocation);
            drone.battery -= distanceTraveled * GetConsumptionRate(drone.weightCategory);
            if (drone.battery < 0 || drone.battery > 100)
                throw new Exception("Oh shit"); // TODO debug this
            drone.currentLocation = reciever.currentLocation;
            drone.status = DroneStatuses.free;
            dal.ProvidePackageToCustomer(dalPackage.ID);
        }
    }
}
