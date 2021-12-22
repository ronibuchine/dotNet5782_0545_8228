using System;
using System.Linq;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public Station AddStation(string name, Location location, int availableChargers)
        {
            try
            {
                Station station = new(name, location, availableChargers);
                dal.AddStation(new DO.Station(station.ID, name, availableChargers, location.longitude, location.latitude));
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
                if (GetStation(stationID).chargeSlots > 0)
                {
                    Random rand = new();
                    double battery = rand.NextDouble() * 20 + 20;
                    DO.Station station = dal.GetStation(stationID);
                    Location location = new Location(station.longitude, station.latitude);
                    Drone drone = new Drone(model, maxWeight, rand.NextDouble() * 20 + 20, DroneStatuses.maintenance, location);
                    drones.Add(drone); // for bl
                    dal.AddDrone(new DO.Drone(drone.ID, model, (DO.WeightCategories)maxWeight)); // for dl
                    dal.SendDroneToCharge(stationID, drone.ID);
                    return drone;
                }
                else
                {
                    throw new OperationNotPossibleException("Station is full already");
                }
            }
            catch (Exception e) { throw new InvalidBlObjectException(e.Message); }
        }

        public Customer AddCustomer(string name, string phone, Location location)
        {
            try
            {
                Customer customer = new Customer(name, phone, location);
                dal.AddCustomer(new DO.Customer(customer.ID, name, phone, location.longitude, location.latitude));
                return customer;
            }
            catch (Exception e) { throw new InvalidBlObjectException(e.Message); }
        }

        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            try
            {
                if (senderID == receiverID) throw new InvalidIDException("ERROR: Sender ID cannot be the same as the receiver ID");
                Package package = new Package(senderID, receiverID, weight, priority);
                package.delivered = package.scheduled = package.pickedUp = package.requested = null;
                dal.AddPackage(new DO.Package(
                            package.ID,
                            senderID,
                            receiverID,
                            0,
                            (DO.WeightCategories)weight,
                            (DO.Priorities)priority,
                            package.requested,
                            null,
                            null,
                            null));
                return package;
            }
            catch (Exception e) { throw new InvalidBlObjectException(e.Message); }
        }
    }
}
