using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int ID)
        {
            lock (dal)
            {

                try
                {
                    if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
                    Station droneStation = new Station(dal.GetStation(ID));
                    if (droneStation != null) return droneStation;
                    throw new InvalidBlObjectException("ERROR: This entity does not exist.");
                }
                catch (InvalidBlObjectException i) {throw i;}
                catch (InvalidIDException e) {throw e;}
                catch (DO.InvalidDalObjectException e) {throw new InvalidBlObjectException(e.Message);}
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int ID)
        {
            lock (dal)
            {

                Drone drone;
                try 
                {
                    drone = drones.First(d => d.ID == ID);
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidBlObjectException("ERROR: This entity does not exist.");
                }
                if (drone == null)
                    throw new InvalidBlObjectException("ERROR: This entity does not exist.");
                return drone;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int ID)
        {
            lock (dal)
            {

                if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
                try
                {
                    Customer customer = new Customer(dal.GetCustomer(ID));
                    if (customer == null) 
                        throw new InvalidBlObjectException("ERROR: This entity does not exist.");
                    return customer;
                }
                catch (DO.InvalidDalObjectException e) { throw new InvalidBlObjectException(e.Message); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int ID)
        {
            lock (dal)
            {

                if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
                try
                {
                    Package package = new Package(dal.GetPackage(ID));
                    if (package == null) 
                        throw new InvalidBlObjectException("ERROR: This entity does not exist.");
                    return package;
                }
                catch (DO.InvalidDalObjectException e) { throw new InvalidBlObjectException(e.Message); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool GetEmployee(int ID)
        {
            lock (dal)
            {

                if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
                try
                {
                    return dal.GetEmployee(ID) != null;
                }
                catch (DO.InvalidDalObjectException e) { return false; }
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> GetStationList()
        {
            lock (dal)
            {

                return dal.GetAllStations().Select(ds => new StationToList(new Station(ds)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetDroneList()
        {
            lock (dal)
            {
                return drones.Select((d) => new DroneToList(d));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> GetCustomerList()
        {
            lock (dal)
            {
                return dal.GetAllCustomers().Select((c) => new CustomerToList(new Customer(c)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<PackageToList> GetPackageList()
        {
            lock (dal)
            {
                return dal.GetAllPackages().Select((p) => new PackageToList(new Package(p)));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetUnassignedPackages()
        {
            lock (dal)
            {
                return dal.GetAllUnassignedPackages().Select(p => new Package(p));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAvailableStations()
        {     
            lock (dal)
            {

                return dal.GetAllUnoccupiedStations().Select(s => new Station(s));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> GetSpecificDrones(Func<DroneToList, bool> pred)
        {
            lock (dal)
            {
                return GetDroneList().Where(pred);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetChargingStation(int droneID)
        {
            lock (dal)
            {                
                var charge = dal.GetAllCharges().FirstOrDefault(dc => dc.droneId == droneID);
                if (charge != null)
                {
                    return charge.stationId;
                }
                else
                    return 0;                
               
            }
        }
    }
}
