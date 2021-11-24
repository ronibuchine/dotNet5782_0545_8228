using System;
using IBL.BO;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public DroneStation AddBaseStation(int stationID, string name, Location location, int availableChargers)
        {
            try
            {
                if (CheckStationID(stationID))
                {
                    DroneStation station = new(stationID, name, location, availableChargers);
                    dal.AddDroneStation(new IDAL.DO.DroneStation(stationID, name, availableChargers,  location.longitude, location.latitude));
                    return station;
                }
            }
            catch (Exception e)
            {
                throw new InvalidBlObjectException(e.Message);
            }
            return null;
            
        }

        public Drone AddDrone(int ID, string model, WeightCategories maxWeight, int stationID)
        {
            try
            {
                if (IsValidID(ID))
                {
                    Drone drone = new Drone(ID, model, maxWeight);
                    drone.status = DroneStatuses.maintenance;
                    drone.battery = new Random().NextDouble() * 20 + 20;
                    foreach (IDAL.DO.DroneStation station in dal.GetAllDroneStations())
                    {
                        if (station.ChargeSlots != 0) 
                        {
                            drone.currentLocation = new Location(station.Longitude, station.Latitude);
                            // break; ?
                        }
                        
                    }
                    dal.AddDrone(new IDAL.DO.Drone(ID, model, (IDAL.DO.WeightCategories)maxWeight));
                    return drone;
                }
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
            return null;
        }

        public Customer AddCustomer(int customerID, string name, string phone, Location location)
        {
            try 
            {
                if (CheckCustomerID(customerID))
                {
                    Customer customer = new Customer(customerID, name, phone, location);
                    dal.AddCustomer(new IDAL.DO.Customer(customerID, name, phone, location.longitude, location.latitude));
                    return customer;
                }                
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
            return null;
        }

        public Package AddPackage(int packageID, int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            try 
            {
                if (CheckPackageID(packageID) && CheckCustomerID(senderID) && CheckCustomerID(receiverID))
                {
                    if (senderID != receiverID) throw new InvalidIDException("ERROR: Sender ID cannot be the same as the receiver ID");
                    Package package = new Package(0, senderID, receiverID, weight, priority); // what should ID for package be here?
                    package.deliveryTime = package.scheduledTime = package.pickedUpTime = DateTime.MinValue;
                    package.requestedTime = DateTime.Now;
                    dal.AddParcel(new IDAL.DO.Parcel(0, senderID, (IDAL.DO.WeightCategories)weight, (IDAL.DO.Priorities)priority, package.requestedTime, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue));
                    return package;
                }
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
            return null;
        }
    }
}
