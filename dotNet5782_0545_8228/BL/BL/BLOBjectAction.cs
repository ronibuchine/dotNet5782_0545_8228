using System;
using System.Collections.Generic;
using System.Linq;
using UTIL;
using BL;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {
        /// <summary>
        /// This API call will send the given drone to the nearest charging station assuming that drone is able to make the journey.
        /// This will fail if the drone isn't free or it doesn't have enough battery.
        /// </summary>
        /// <param name="droneID">ID of the drone to send</param>
        public void SendDroneToCharge(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not free currently");
            Station closestAvailable = GetClosestStation(drone.currentLocation, GetAvailableStations());
            if (!CanArriveToLocation(drone, closestAvailable.location))
                throw new OperationNotPossibleException("Drone does not have enough battery to reach closest available station");
            drone.status = DroneStatuses.maintenance;
            dal.SendDroneToCharge(closestAvailable.ID, droneID);
        }

        /// <summary>
        /// This API call will release a specified drone from charging. It is released after a certain amount of hours which is specified by the user of the system.
        /// This call throws exceptions when the drone doesn't have the correct status, i.e. not in maintenance.
        /// </summary>
        /// <param name="droneID">the drone that is currently in charging to be released.</param>
        /// <param name="hoursCharging">Number of hours the drone was charging for.</param>
        public void ReleaseDroneFromCharge(int droneID, int hoursCharging)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.maintenance) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not currently in maintenance");
            drone.battery += hoursCharging * chargingRate;
            if (drone.battery > 100)
                drone.battery = 100;
            drone.status = DroneStatuses.free;
            int stationID = dal.GetAllCharges()
                                .First(dc => dc.DroneId == droneID).StationId;
            dal.ReleaseDroneFromCharge(stationID, droneID);
        }

        /// <summary>
        /// This API call will assign the best possible package to the drone that is supplied to the function call.
        /// Packages are assigned in order of suitability in terms of weight and priority.
        /// It throws an exception in the event that there is no suitable package or the drone cannot currently be assigned a package.
        /// </summary>
        /// <param name="droneID">The drone ID to assign to.</param>
        public void AssignPackageToDrone(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not free currently");

            IEnumerable<PackageInTransfer> packages = dal.GetAllPackages()
                .Where(p => p.weight <= (DO.WeightCategories)drone.weightCategory)
                .OrderByDescending(p => p.priority)
                .ThenByDescending(p => p.weight)
                .ThenBy(p =>
                {
                    Location senderLocation = GetCustomer(p.senderId).currentLocation;
                    return Distances.GetDistance(senderLocation, drone.currentLocation);
                })
                .Select(p => new PackageInTransfer(p));

            foreach (PackageInTransfer package in packages)
            {
                double batteryRequired = BatteryRequiredForDelivery(drone, package);
                if (batteryRequired <= drone.battery)
                {
                    drone.status = DroneStatuses.delivery;
                    dal.AssignPackageToDrone(package.ID, droneID);
                    return;
                }
            }
            throw new OperationNotPossibleException("There is no suitable package to assign");
        }

        /// <summary>
        /// This API call will allow the drone to collect the package it is assigned to.
        /// This call will fail in the event that the drone isn't currently assigned a package or it does not have enough battery to reach the sender.
        /// </summary>
        /// <param name="droneID">The drone to collect the package</param>
        public void CollectPackage(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not delivering currently");
            DO.Package dalPackage = dal.GetAllPackages().First(p => p.droneId == droneID);
            Customer sender = new Customer(dal.GetCustomer(dalPackage.senderId));
            double distanceTraveled = Distances.GetDistance(drone.currentLocation, sender.currentLocation);
            double batteryRequired = distanceTraveled * GetConsumptionRate(drone.weightCategory);
            if (batteryRequired > drone.battery)
                throw new Exception("oh shit, not enough battery to reach sender location" + drone.ToString()); // TODO debug this
            drone.battery -= batteryRequired;
            drone.currentLocation = sender.currentLocation;
            dal.CollectPackageToDrone(dalPackage.ID);
        }

        /// <summary>
        /// This API call will deliver a package to the customer the package is intended for.
        /// This call fails when the package hasn't been collect4ed yet, the drone does not have enough battery or the drone is not in the correct status.
        /// </summary>
        /// <param name="droneID">the drone to deliver the package</param>
        public void DeliverPackage(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not delivering currently");
            DO.Package dalPackage = dal.GetAllPackages().First(p => p.droneId == droneID);
            // not a fan of this comparison. Not precise at all
            if (dalPackage.pickedUp == null)
                throw new OperationNotPossibleException("package has not yet been collected");
            Customer reciever = new Customer(dal.GetCustomer(dalPackage.recieverId));
            double distanceTraveled = Distances.GetDistance(reciever.currentLocation, drone.currentLocation);
            double batteryRequired = distanceTraveled * GetConsumptionRate(drone.weightCategory);
            if (batteryRequired > drone.battery)
                throw new Exception("oh shit, not enough battery to reach delivery location" + drone.ToString()); // TODO debug this
            drone.battery -= batteryRequired;
            drone.currentLocation = reciever.currentLocation;
            drone.status = DroneStatuses.free;
            dal.ProvidePackageToCustomer(dalPackage.ID);
        }
    }
}
