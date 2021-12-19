using System;
using System.Collections.Generic;
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
                throw new OperationNotPossibleException("Drone is not free currently");
            Station closestAvailable = GetClosestStation(drone.currentLocation, GetAvailableStations());
            if (!CanArriveToLocation(drone, closestAvailable.location))
                throw new OperationNotPossibleException("Drone does not have enough battery to reach closest available station");
            drone.status = DroneStatuses.maintenance;
            dal.SendDroneToCharge(closestAvailable.ID, droneID);
        }

        public void ReleaseDroneFromCharge(int droneID, int hoursCharging)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.maintenance) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not currently in maintenance");
            drone.battery += hoursCharging * chargingRate;
            if (drone.battery > 100)
                drone.battery = 100;
            drone.status = DroneStatuses.free;
            int stationID = dal.GetAllCharges().Find((dc) => dc.DroneId == droneID).StationId;
            dal.ReleaseDroneFromCharge(stationID, droneID);
        }

        public void AssignPackageToDrone(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not free currently");

            List<PackageInTransfer> packages = dal.GetAllPackages()
                .FindAll(p => p.weight <= (IDAL.DO.WeightCategories)drone.weightCategory)
                .OrderByDescending(p => p.priority)
                .ThenByDescending(p => p.weight)
                .ThenBy(p =>
                {
                    Location senderLocation = GetCustomer(p.senderId).currentLocation;
                    return Distances.GetDistance(senderLocation, drone.currentLocation);
                })
                .ToList()
                .ConvertAll(p => new PackageInTransfer(p));

            foreach (PackageInTransfer package in packages)
            {
                double droneToSender = Distances.GetDistance(drone.currentLocation, package.collectionLocation);
                double senderToDelivery = package.deliveryDistance;
                Location closestStationToDelivery = GetClosestStationLocation(package.deliveringLocation, dal.GetAllStations().ConvertAll(s => new Station(s)));
                double deliveryToClosestStation = Distances.GetDistance(package.deliveringLocation, closestStationToDelivery); 
                double distanceRequired = droneToSender + senderToDelivery + deliveryToClosestStation;
                double batteryRequired = GetConsumptionRate(drone.weightCategory) * distanceRequired;
                if (batteryRequired <= drone.battery)
                {
                    drone.status = DroneStatuses.delivery;
                    dal.AssignPackageToDrone(package.ID, droneID);
                    return;
                }
            }
            throw new OperationNotPossibleException("There is no suitable package to assign");
        }

        public void CollectPackage(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not delivering currently");
            IDAL.DO.Package dalPackage = dal.GetAllPackages().Find(p => p.droneId == droneID);
            Customer sender = new Customer(dal.GetCustomer(dalPackage.senderId));
            double distanceTraveled = Distances.GetDistance(drone.currentLocation, sender.currentLocation);
            drone.battery -= (distanceTraveled * GetConsumptionRate(drone.weightCategory));
            if (drone.battery < 0 || drone.battery > 100)
                throw new Exception("oh shit, not enough battery to reach sender location" + drone.ToString()); // TODO debug this
            drone.currentLocation = sender.currentLocation;
            dal.CollectPackageToDrone(dalPackage.ID);
        }

        public void DeliverPackage(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.delivery) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not delivering currently");
            IDAL.DO.Package dalPackage = dal.GetAllPackages().Find(p => p.droneId == droneID);
            // not a fan of this comparison. Not precise at all
            if (dalPackage.pickedUp == null)
                throw new OperationNotPossibleException("package has not yet been collected");
            Customer reciever = new Customer(dal.GetCustomer(dalPackage.recieverId));
            double distanceTraveled = Distances.GetDistance(reciever.currentLocation, drone.currentLocation);
            drone.battery -= distanceTraveled * GetConsumptionRate(drone.weightCategory);
            if (drone.battery < 0 || drone.battery > 100)
                throw new Exception("Oh shit, not enough battery to reach delivery location" + drone.ToString()); // TODO debug this
            drone.currentLocation = reciever.currentLocation;
            drone.status = DroneStatuses.free;
            dal.ProvidePackageToCustomer(dalPackage.ID);
        }
    }
}
