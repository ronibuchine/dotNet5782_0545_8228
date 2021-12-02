using System;
using IBL.BO;

namespace BLOBjectNamespace
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public Station AddStation(string name, Location location, int availableChargers)
        {
            try
            {
                Station station = new(name, location, availableChargers);
                dal.AddStation(new IDAL.DO.Station(station.ID,name, availableChargers,location.longitude, location.latitude));
                return station;
            }
            catch (Exception e)
            {
                throw new InvalidBlObjectException(e.Message);
            }
        }

        public Drone AddDrone(string model, WeightCategories maxWeight, int stationID)
        {
            try
            {
                    Random rand = new();
                    double battery = rand.NextDouble() * 20 + 20;
                    IDAL.DO.Station station = dal.GetStation(stationID);
                    Location location = new Location(station.longitude, station.latitude);
                    Drone drone = new Drone(model, maxWeight, rand.NextDouble() * 20 + 20, DroneStatuses.maintenance, location);
                    drones.Add(drone);
                    dal.AddDrone(new IDAL.DO.Drone(drone.ID, model, (IDAL.DO.WeightCategories)maxWeight));
                    return drone;
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
        }

        public Customer AddCustomer(string name, string phone, Location location)
        {
            try 
            {
                Customer customer = new Customer(name, phone, location);
                dal.AddCustomer(new IDAL.DO.Customer(customer.ID, name, phone, location.longitude, location.latitude));
                return customer;
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
        }

        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            try 
            {
                if (senderID != receiverID) throw new InvalidIDException("ERROR: Sender ID cannot be the same as the receiver ID");
                Package package = new Package(senderID, receiverID, weight, priority);
                package.delivered = package.scheduled = package.pickedUp = DateTime.MinValue;
                package.requested = DateTime.Now;
                dal.AddPackage(new IDAL.DO.Package(
                            package.ID,
                            senderID,
                            receiverID,
                            0,
                            (IDAL.DO.WeightCategories)weight,
                            (IDAL.DO.Priorities)priority,
                            package.requested,
                            DateTime.MinValue,
                            DateTime.MinValue,
                            DateTime.MinValue));
                return package;
            }
            catch (Exception e) {throw new InvalidBlObjectException(e.Message);}
        }
    }
}
