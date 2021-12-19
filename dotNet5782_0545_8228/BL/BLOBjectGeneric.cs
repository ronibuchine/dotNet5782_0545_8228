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

        public BLOBject(Object _)
        {
            this.dal = new DalObject(null);
            dal.Clear();
            CommonCtor(this.dal);
        }

        public BLOBject()
        {
            this.dal = DalObject.GetInstance();
            CommonCtor(this.dal);
        }

        private void CommonCtor(IdalInterface dal)
        {
            BLEntity.nextID = dal.GetNextID();
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
            CompleteCustomersPackageList(customers, packages);

            // complete drones based on all other information
            Random rand = new Random();
            foreach (Drone drone in drones)
            {
                Package package = packages.Find(p => p.drone != null && p.drone.ID == drone.ID);
                if (package != null)
                {
                    drone.packageInTransfer = new PackageInTransfer(package);
                    if (package.delivered == null)
                    {
                        drone.status = DroneStatuses.delivery;
                        Location senderLocation = customers.Find(c => c.ID == package.sender.ID).currentLocation;
                        Location closestStationLoc = GetClosestStationLocation(senderLocation, stations);
                        double batteryRequired = BatteryRequiredForDelivery(drone, drone.packageInTransfer);
                        drone.battery = rand.NextDouble() * (100 - batteryRequired);
                        if (package.pickedUp == null) // not collected
                            drone.currentLocation = closestStationLoc;
                        else //collected
                            drone.currentLocation = senderLocation;
                    }
                }
                else // drone has no associated package
                {
                    int randChoice = rand.Next(2);
                    if (randChoice == 0) // free
                    {
                        drone.status = DroneStatuses.free;
                        List<Customer> recievingCustomers = customers.FindAll(c => c.packagesToCustomer.Count != 0);
                        if (recievingCustomers.Count == 0)
                        {
                            drone.currentLocation = new Location(1, 1);
                        }
                        else
                        {
                            Customer customer = recievingCustomers[rand.Next(recievingCustomers.Count)];
                            drone.currentLocation = customer.currentLocation;
                        }
                        /* drone.currentLocation = stations[rand.Next(stations.Count)].location; */
                        drone.battery = rand.NextDouble() * 20;
                        Location closestStation = GetClosestStationLocation(drone.currentLocation, stations);
                        double minRequired = GetDistance(drone.currentLocation, closestStation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired);
                    }
                    else // maintenance
                    {
                        drone.status = DroneStatuses.maintenance;
                        var station = stations[rand.Next(stations.Count - 1)];
                        drone.currentLocation = station.location;
                        dal.SendDroneToCharge(station.ID, drone.ID);
                        drone.battery = rand.NextDouble() * 20;
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
                packages[i].sender = new(customers.Find(c => c.ID == dalPackages[i].senderId));
                packages[i].receiver = new(customers.Find(c => c.ID == dalPackages[i].recieverId));
                if (dalPackages[i].droneId != 0)
                    packages[i].drone = new(drones.Find(d => d.ID == dalPackages[i].droneId));
            }
        }

        private void CompleteCustomersPackageList(List<Customer> customers, List<Package> packages)
        {
            // get each sent/recieved package into the customers sent/recieved list
            foreach (var customer in customers)
            {
                customer.packagesToCustomer.AddRange(packages.FindAll(p => p.receiver.ID == customer.ID).ConvertAll(p => new PackageAtCustomer(p)));
                customer.packagesFromCustomer.AddRange(packages.FindAll(p => p.sender.ID == customer.ID).ConvertAll(p => new PackageAtCustomer(p)));
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

        private double BatteryRequiredForDelivery(Drone drone, PackageInTransfer package)
        {
            double droneToSender = GetDistance(drone.currentLocation, package.collectionLocation);
            double senderToDelivery = package.deliveryDistance;
            Location closestStationToDelivery = GetClosestStationLocation(package.deliveringLocation, dal.GetAllStations().ConvertAll(s => new Station(s)));
            double deliveryToClosestStation = GetDistance(package.deliveringLocation, closestStationToDelivery);
            double distanceRequired = droneToSender + senderToDelivery + deliveryToClosestStation;
            double batteryRequired = GetConsumptionRate(drone.weightCategory) * distanceRequired;
            return batteryRequired;
        }
    }
}
