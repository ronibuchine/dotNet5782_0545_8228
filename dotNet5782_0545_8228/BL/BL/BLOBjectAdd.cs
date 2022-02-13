using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station AddStation(string name, Location location, int availableChargers)
        {
            lock (dal)
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone AddDrone(string model, WeightCategories maxWeight, int stationID)
        {
            lock (dal)
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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer AddCustomer(string name, string phone, Location location, int ID, string password = null)
        {
            lock (dal)
            {
                try
                {
                    Customer customer = new Customer(ID, name, phone, location);
                    if (!(GetCustomerList().Select(c => c.ID).Contains(ID)))
                    {
                        dal.AddCustomer(new DO.Customer(customer.ID, name, phone, location.longitude, location.latitude, password));
                        return customer;
                    }
                    else
                        throw new InvalidBlObjectException("Customer already exists in the system.");
                }
                catch (Exception e) { throw new InvalidBlObjectException(e.Message); }
                
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package AddPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            lock (dal)
            {
                try
                {
                    if (senderID == receiverID) throw new InvalidIDException("ERROR: Sender ID cannot be the same as the receiver ID");
                    Package package = new Package(senderID, receiverID, weight, priority);
                    package.delivered = package.scheduled = package.pickedUp = null;
                    package.requested = DateTime.Now;
                    dal.AddPackage(new DO.Package(
                                package.ID,
                                senderID,
                                receiverID,
                                0,
                                (DO.WeightCategories)weight,
                                (DO.Priorities)priority,
                                DateTime.Now,
                                null,
                                null,
                                null));
                    return package;
                }
                catch (Exception e) { throw new InvalidBlObjectException(e.Message); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddEmployee(int ID, string password)
        {
            lock (dal)
            {
                try  // check to make sure the employee doesn't already exist
                {
                    dal.GetEmployee(ID);
                    throw new OperationNotPossibleException("This employee already exists in the system.\nPlease contact a system administrator for valid credentials.");

                }
                catch (DO.InvalidDalObjectException except)
                {
                    dal.AddEmployee(ID, password);
                    return;
                }
            }            
            
        }
    }
}
