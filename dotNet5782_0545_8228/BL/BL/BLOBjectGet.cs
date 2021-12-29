﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {
      
        public Station GetStation(int ID)
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

       
        public Drone GetDrone(int ID)
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
         
      
        public Customer GetCustomer(int ID)
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

        public Package GetPackage(int ID)
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

        public bool GetEmployee(int ID)
        {
            if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
            try
            {
                return dal.GetEmployee(ID) != null;
            }
            catch (DO.InvalidDalObjectException e) { return false; }
        }

        public IEnumerable<StationToList> GetStationList()
        {
            return dal.GetAllStations().Select(ds => new StationToList(new Station(ds)));
        }

      
        public IEnumerable<DroneToList> GetDroneList()
        {
            return drones.Select((d) => new DroneToList(d));
        }

       
        public IEnumerable<CustomerToList> GetCustomerList()
        {
            return dal.GetAllCustomers().Select((c) => new CustomerToList(new Customer(c)));
        }

      
        public IEnumerable<PackageToList> GetPackageList()
        {
            return dal.GetAllPackages().Select((p) => new PackageToList(new Package(p)));
        }

    
        public IEnumerable<Package> GetUnassignedPackages()
        {
            return dal.GetAllUnassignedPackages().Select(p => new Package(p));
        }

    
        public IEnumerable<Station> GetAvailableStations()
        {               
            return dal.GetAllUnoccupiedStations().Select(s => new Station(s));
        }

       
        public IEnumerable<DroneToList> GetSpecificDrones(Func<DroneToList, bool> pred)
        {
            return GetDroneList().Where(pred);
        }
    }
}