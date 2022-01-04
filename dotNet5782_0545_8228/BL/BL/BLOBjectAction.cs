using System;
using System.Collections.Generic;
using System.Linq;
using UTIL;
using BL;

namespace BL
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
            int stationID = dal.GetAllCharges()
                                .First((dc) => dc.DroneId == droneID).StationId;
            dal.ReleaseDroneFromCharge(stationID, droneID);
        }

       
        public void AssignPackageToDrone(int droneID)
        {
            Drone drone = GetDrone(droneID);
            if (drone.status != DroneStatuses.free) // is this always initialized?
                throw new OperationNotPossibleException("Drone is not free currently");

            try
            {
                PackageInTransfer package = dal.GetAllPackages()
                    .Where(p => p.weight <= (DO.WeightCategories)drone.weightCategory && p.scheduled == null)
                    .Select(p => new PackageInTransfer(p))
                    .Where(p => BatteryRequiredForDelivery(drone, p) <= drone.battery)
                    .OrderByDescending(p => p.priority)
                    .ThenByDescending(p => p.weightCategory)
                    .ThenBy(p =>
                    {
                        Location senderLocation = GetCustomer(p.sender.ID).currentLocation;
                        return Distances.GetDistance(senderLocation, drone.currentLocation);
                    })
                    .First();

                    drone.status = DroneStatuses.delivery;
                    drone.packageInTransfer = package;
                    dal.AssignPackageToDrone(package.ID, droneID);

            }
            catch (InvalidOperationException)
            {
                throw new OperationNotPossibleException("There is no suitable package to assign");
            }
        }

       
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
            drone.packageInTransfer = null;
            dal.ProvidePackageToCustomer(dalPackage.ID);
        }


        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            return dal.VerifyEmployeeCredentials(ID, password);            
        }

        public bool VerifyCustomerCredentials(int ID, string password)
        {
            return dal.VerifyCustomerCredentials(ID, password);
        }

    }
}
