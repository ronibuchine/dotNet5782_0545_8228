using System;
using System.Collections.Generic;
using IDAL;
using DalObject;
using IBL.BO;
using static UTIL.Distances;



namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public double chargingRate { get; set; }
        // need to have some field here for power consumption
        public double free { get; set; }
        public double lightWeight { get; set; }
        public double midWeight { get; set; }
        public double heavyWeight { get; set; }
        public IdalInterface dal { get; }
        private List<Drone> drones;

        private Location GetClosestStationLocation(Location location, List<DroneStation> stations)
        {
            return GetClosestStation(location, stations).location;
        }

        private DroneStation GetClosestStation(Location location, List<DroneStation> stations)
        {
            double min = double.PositiveInfinity;
            if (stations.Count == 0)
            {
                throw new BlObjectAccessException("No available stations currently");
            }
            DroneStation minStation = stations[0];
            foreach (DroneStation station in stations)
            {
                if (GetDistance(station.location, location) < min)
                {
                    min = GetDistance(station.location, location);
                    minStation = station;
                }
            }
            return minStation;
        }


        private double GetConsumptionRate(WeightCategories weight)
        {
            if (weight == WeightCategories.light)
            {
                return lightWeight;
            }
            else if (weight == WeightCategories.medium)
            {
                return midWeight;
            }
            else
            {
                return heavyWeight;
            }
        }

        private int GetCountChargingDrones(int stationID)
        {
            return dal.GetAllCharges().FindAll((c) => c.StationId == stationID).Count;
        }

        private double GetMinBatteryRequired(Drone drone, Location location)
        {
            return GetDistance(location, drone.currentLocation) * GetConsumptionRate(drone.weightCategory);
        }
        private bool CanArriveToLocation(Drone drone, Location location)
        {
            return GetMinBatteryRequired(drone, location) < drone.battery;
        }

        public BLOBject()
        {
            this.dal = DalObject.DalObject.GetInstance();
            double[] powerConsumption = dal.PowerConsumptionRequest();
            this.free = powerConsumption[0];
            this.lightWeight = powerConsumption[1];
            this.midWeight = powerConsumption[2];
            this.heavyWeight = powerConsumption[3];
            this.chargingRate = powerConsumption[4];

            drones = dal.GetAllDrones().ConvertAll(d => new Drone(d));
            List<IDAL.DO.Package> dalPackages = dal.GetAllPackages();
            List<Package> packages = dalPackages.ConvertAll(p => new Package(p));
            List<Customer> customers = dal.GetAllCustomers().ConvertAll(c => new Customer(c));
            drones.ForEach(drone => {
                for (int i = 0; i < packages.Count; i++)
                {
                    if (drone.ID == dalPackages[i].droneId)
                        packages[i].drone = drone;
                }
            });
            customers.ForEach((c) =>
            {
                for (int i = 0; i < packages.Count; i++)
                {
                    if (dalPackages[i].senderId == c.ID)
                        c.packagesToCustomer.Add(packages[i]);
                    if (dalPackages[i].recieverId == c.ID)
                        c.packagesToCustomer.Add(packages[i]);
                }
            });
            List<DroneStation> stations = dal.GetAllStations().ConvertAll(ds => new DroneStation(ds));

            Random rand = new Random();
            foreach (Drone drone in drones)
            {
                Package package = packages.Find(p => p.drone != null && p.drone.ID == drone.ID);
                if (package != null)
                {
                    drone.packageInTransfer = new PackageInTransfer(package);
                    if (DateTime.Compare(package.delivered, DateTime.Now) < 0 && package.drone.ID == drone.ID)
                    {
                        drone.status = DroneStatuses.delivery;
                        Location closestStationLoc = GetClosestStationLocation(package.sender.currentLocation, stations);
                        double minRequired = GetDistance(closestStationLoc, package.sender.currentLocation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired);
                        if (DateTime.Compare(package.pickedUp, DateTime.Now) > 0) // not collected
                            drone.currentLocation = closestStationLoc;
                        else //collected
                            drone.currentLocation = package.sender.currentLocation;
                        break;
                    }
                }
                else // drone has no associated package
                {
                    int randChoice = rand.Next(2);
                    if (randChoice == 0) // free
                    {
                        drone.status = DroneStatuses.free;
                        drone.currentLocation = stations[rand.Next(stations.Count)].location;
                        drone.battery = rand.NextDouble() * 20;

                    }
                    else // maintenance
                    {
                        drone.status = DroneStatuses.maintenance;
                        List<Customer> recievingCustomers = customers.FindAll(c => c.packagesToCustomer.Count != 0);
                        Customer customer = recievingCustomers[rand.Next(recievingCustomers.Count)];
                        drone.currentLocation = customer.currentLocation;
                        Location closestStationLoc = GetClosestStationLocation(customer.currentLocation, stations);
                        double minRequired = GetDistance(closestStationLoc, customer.currentLocation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired);

                    }
                }
            }
        }

        private Boolean IsValidID(int ID)
        {
            if (ID < 0) // other conditions here?
                return false;
            else
                return true;
        }
        private Boolean IsUniqueDroneID(int ID)
        {
            foreach (IDAL.DO.Drone drone in dal.GetAllDrones())
            {
                if (drone.ID == ID)
                    return false;
            }
            return true;
        }
        private Boolean CheckStationID(int ID)
        {
            if (ID < 0) throw new InvalidIDException("ERROR: ID cannot be negative");
            foreach (IDAL.DO.Station station in dal.GetAllStations())
            {
                if (station.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }
        private Boolean CheckCustomerID(int ID)
        {
            if (ID < 0) throw new InvalidIDException("ERROR: ID cannot be negative");
            foreach (IDAL.DO.Customer customer in dal.GetAllCustomers())
            {
                if (customer.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }
        private Boolean CheckPackageID(int ID)
        {
            if (ID < 0) throw new InvalidIDException("ERROR: ID cannot be negative");
            foreach (IDAL.DO.Package package in dal.GetAllParcels())
            {
                if (package.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }

    }
}
