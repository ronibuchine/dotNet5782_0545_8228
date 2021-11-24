﻿using System;
using System.Collections.Generic;
using IBL.BO;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public DroneStation GetBaseStation(int ID)
        {
            try
            {
                CheckStationID(ID);
                DroneStation droneStation = new DroneStation(dal.GetDroneStation(ID));
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
            try
            {
                CheckDroneID(ID);
                Drone drone = drones.Find((d) => {return d.ID == ID;});
                if (drone != null) return drone;
                throw new InvalidBlObjectException("ERROR: This entity does not exist.");
            }
            catch (InvalidIDException e) { throw e; }
            catch (InvalidBlObjectException i) { throw i; }
            throw new Exception("UNKNOWN ERROR OCCURED. WHOOPS.");
        }
          
        public Customer GetCustomer(int ID)
        {
            try
            {
                CheckCustomerID(ID);
                Customer customer = new Customer(dal.GetCustomer(ID));
                if (customer != null) return customer;
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
                Package package = new Package(dal.GetParcel(ID));
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
            return dal.GetAllDroneStations().ConvertAll<BaseStationToList>((ds) => new BaseStationToList(new DroneStation(ds)));
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
            return dal.GetAllParcels().ConvertAll((p) => new PackageToList(new Package(p)));
        }
        public List<PackageToList> GetUnassignedPackages()
        {
            List<PackageToList> unassignedPackages = new List<PackageToList>(DalObject.DataSource.parcels.Count);
            foreach (IDAL.DO.Parcel package in DalObject.DataSource.parcels)
            {
                if (package.droneId == 0) unassignedPackages.Add(new PackageToList(new Package(package)));
            }
            return unassignedPackages;
        }
        public List<DroneStation> GetAvailableStations()
        {
            return dal.GetAllDroneStations().FindAll(
                    (s) => GetCountChargingDrones(s.ID) < s.ChargeSlots)
                .ConvertAll((s) => new DroneStation(s));
        }
    }
}
