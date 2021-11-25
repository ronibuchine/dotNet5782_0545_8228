using System;
using System.Collections.Generic;
using IBL.BO;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public Station GetBaseStation(int ID)
        {
            try
            {
                CheckStationID(ID);
                Station droneStation = new Station(dal.GetStation(ID));
                if (droneStation != null) return droneStation;
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            }
            catch (InvalidBlObjectException i) {throw i;}
            catch (InvalidIDException e) {throw e;}
            catch (IDAL.DO.InvalidDalObjectException e) {throw new InvalidBlObjectException(e.Message);}
            throw new Exception("UNKNOWN ERROR OCCURED. WHOOPS.");
        }

        public Drone GetDrone(int ID)
        {
            Drone drone = drones.Find(d => d.ID == ID);
            if (drone == null)
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            return drone;
        }
          
        public Customer GetCustomer(int ID)
        {
            try
            {
                CheckCustomerID(ID);
                Customer customer = new Customer(dal.GetCustomer(ID));
                if (customer != null) 
                    return customer;
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            }
            catch (InvalidIDException e) { throw e; }
            catch (IDAL.DO.InvalidDalObjectException e) { throw new InvalidBlObjectException(e.Message); }
            catch (InvalidBlObjectException i) { throw i; }
            throw new Exception("UNKNOWN ERROR OCCURED. WHOOPS.");
        }
        public Package GetPackage(int ID)
        {
            try
            {
                CheckPackageID(ID);
                Package package = new Package(dal.GetPackage(ID));
                if (package != null) return package;
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            }
            catch (InvalidIDException e) { throw e; }
            catch (IDAL.DO.InvalidDalObjectException e) { throw new InvalidBlObjectException(e.Message); }
            catch (InvalidBlObjectException i) { throw i; }
            throw new Exception("UNKNOWN ERROR OCCURED. WHOOPS.");
        }
        public List<BaseStationToList> GetStationList()
        {
            return dal.GetAllStations().ConvertAll<BaseStationToList>((ds) => new BaseStationToList(new Station(ds)));
        }
        public List<DroneToList> GetDroneList()
        {
            return drones.ConvertAll((d) => new DroneToList(d));
        }
        public List<CustomerToList> GetCustomerList()
        {
            return dal.GetAllCustomers().ConvertAll((c) => new CustomerToList(new Customer(c)));
        }
        public List<PackageToList> GetPackageList()
        {
            return dal.GetAllPackages().ConvertAll((p) => new PackageToList(new Package(p)));
        }
        public List<PackageToList> GetUnassignedPackages()
        {
            List<PackageToList> unassignedPackages = new List<PackageToList>();
            dal.GetAllNotAssignedPackages()
                .ForEach(p => unassignedPackages.Add(new PackageToList(new Package(p))));
            return unassignedPackages;
        }
        public List<Station> GetAvailableStations()
        {
            return dal.GetAllStations().FindAll(
                    (s) => GetCountChargingDrones(s.ID) < s.chargeSlots)
                .ConvertAll((s) => new Station(s));
        }
    }
}
