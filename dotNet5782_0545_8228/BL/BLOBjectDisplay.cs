using System;
using System.Collections.Generic;
using IBL.BO;

namespace BLOBjectNamespace
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
                if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
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
                if (!IsValidID(ID)) throw new InvalidIDException("ERROR: The ID is invalid");
                Package package = new Package(dal.GetPackage(ID));
                if (package != null) return package;
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            }
            catch (InvalidIDException e) { throw e; }
            catch (IDAL.DO.InvalidDalObjectException e) { throw new InvalidBlObjectException(e.Message); }
            catch (InvalidBlObjectException i) { throw i; }
            throw new Exception("UNKNOWN ERROR OCCURED. WHOOPS.");
        }

        public List<StationToList> GetStationList()
        {
            return dal.GetAllStations().ConvertAll((ds) => new StationToList(new Station(ds)));
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
            List<PackageToList> unassignedPackages = new();
            dal.GetAllNotAssignedPackages()
                .ForEach(p => unassignedPackages.Add(new PackageToList(new Package(p))));
            return unassignedPackages;
        }

        public List<Station> GetAvailableStations()
        {
            return dal.GetAllUnoccupiedStations().ConvertAll((s) => new Station(s));
        }
    }
}
