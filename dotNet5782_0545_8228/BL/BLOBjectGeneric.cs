using System;
using System.Collections.Generic;
using IDAL;
using DalObjectNamespace;
using IBL.BO;
using static UTIL.Distances;



namespace BLOBjectNamespace
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public static double chargingRate { get; set; }
        public static double free { get; set; }
        public static double lightWeight { get; set; }
        public static double midWeight { get; set; }
        public static double heavyWeight { get; set; }
        public IdalInterface dal { get; }
        private List<Drone> drones;

        public BLOBject()
        {
            this.dal = DalObject.GetInstance();
            IBL.BO.BLEntity.nextID = dal.GetNextID();
            double[] powerConsumption = dal.PowerConsumptionRequest();
            free = powerConsumption[0];
            lightWeight = powerConsumption[1];
            midWeight = powerConsumption[2];
            heavyWeight = powerConsumption[3];
            chargingRate = powerConsumption[4];

            drones = dal.GetAllDrones().ConvertAll(d => new Drone(d));
            List<Package> packages = dal.GetAllPackages().ConvertAll(p => new Package(p));
            List<Customer> customers = dal.GetAllCustomers().ConvertAll(c => new Customer(c));
            List<Station> stations = dal.GetAllStations().ConvertAll(s => new Station(s));

            CompleteStations(stations);
            CompletePackages(packages, customers);
            CompleteCustomersPackageList(packages);

            // complete drones based on all other information
            Random rand = new Random();
            foreach (Drone drone in drones)
            {
                Package package = packages.Find(p => p.drone != null && p.drone.ID == drone.ID);
                if (package != null)
                {
                    drone.packageInTransfer = new PackageInTransfer(package);
                    if (DateTime.Compare(package.delivered, DateTime.Now) < 0)
                    {
                        drone.status = DroneStatuses.delivery;
                        Location closestStationLoc = GetClosestStationLocation(package.sender.currentLocation, stations);
                        double minRequired = GetDistance(closestStationLoc, package.sender.currentLocation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired);
                        if (DateTime.Compare(package.pickedUp, DateTime.Now) > 0) // not collected
                            drone.currentLocation = closestStationLoc;
                        else //collected
                            drone.currentLocation = package.sender.currentLocation;

                        drone.packageInTransfer = new(package);
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

        private void CompleteStations(List<Station> stations)
        {
            // for each drone charge record that data in station.chargingDrones
            foreach (IDAL.DO.DroneCharge droneCharge in dal.GetAllCharges())
                stations.Find(s => s.ID == droneCharge.StationId).chargingDrones.Add(drones.Find(d => d.ID == droneCharge.DroneId));
        }

        private void CompletePackages(List<Package> packages, List<Customer> customers)
        {
            // Find the sender and reciever for each package based on what is in dal
            // Find drone that is carrying package
            List<IDAL.DO.Package> dalPackages = dal.GetAllPackages();
            for (int i = 0; i < packages.Count; i++)
            {
                packages[i].sender = customers.Find(c => c.ID == dalPackages[i].senderId);
                packages[i].receiver = customers.Find(c => c.ID == dalPackages[i].recieverId);
                if (dalPackages[i].droneId != 0)
                    packages[i].drone = drones.Find(d => d.ID == dalPackages[i].droneId);
            }
        }

        private void CompleteCustomersPackageList(List<Package> packages)
        {
            // get each sent/recieved package into the customers sent/recieved list
            foreach (Package package in packages)
            {
                package.receiver.packagesToCustomer.Add(package); // a bit circular
                package.sender.packagesFromCustomer.Add(package); 
            }
        }

        private Location GetClosestStationLocation(Location location, List<Station> stations)
        {
            return GetClosestStation(location, stations).location;
        }

        private Station GetClosestStation(Location location, List<Station> stations)
        {
            double min = double.PositiveInfinity;
            if (stations.Count == 0)
            {
                throw new BlObjectAccessException("No available stations currently");
            }
            Station minStation = stations[0];
            foreach (Station station in stations)
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
            foreach (IDAL.DO.Package package in dal.GetAllPackages())
            {
                if (package.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }

    }
}
