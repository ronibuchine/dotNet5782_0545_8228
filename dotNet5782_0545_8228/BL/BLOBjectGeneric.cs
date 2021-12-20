using System;
using System.Linq;
using System.Collections.Generic;
using IBL.BO;
using DALAPI;
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
        public IDAL dal { get; }
        private IEnumerable<Drone> drones;

        public BLOBject(Object _)
        {
            this.dal = DalFactory.GetDal();
            dal.Clear();
            CommonCtor(this.dal);
        }

        public BLOBject()
        {
            this.dal = DalFactory.GetDal();
            CommonCtor(this.dal);
        }

        private void CommonCtor(IDAL dal)
        {
            BLEntity.nextID = dal.GetNextID();
            double[] powerConsumption = dal.PowerConsumptionRequest();
            free = powerConsumption[0];
            lightWeight = powerConsumption[1];
            midWeight = powerConsumption[2];
            heavyWeight = powerConsumption[3];
            chargingRate = powerConsumption[4];

            drones = dal.GetAllDrones().Select(d => new Drone(d));
            IEnumerable<Package> packages = dal.GetAllPackages().Select(p => new Package(p));
            IEnumerable<Customer> customers = dal.GetAllCustomers().Select(c => new Customer(c));
            IEnumerable<Station> stations = dal.GetAllStations().Select(s => new Station(s));

            CompleteStations(stations);
            CompletePackages(packages, customers);
            CompleteCustomersPackageList(customers, packages);

            // complete drones based on all other information
            Random rand = new Random();
            foreach (Drone drone in drones)
            {
                Package package = packages.First(p => p.drone != null && p.drone.ID == drone.ID);
                if (package != null)
                {
                    PackageInTransfer packageInTransfer = new(package);
                    drone.packageInTransfer = packageInTransfer;
                    if (package.delivered == null)
                    {
                        drone.status = DroneStatuses.delivery;

                        Location senderLocation = customers.First(c => c.ID == package.sender.ID).currentLocation;
                        Location closestStationLoc = GetClosestStationLocation(senderLocation, stations);

                        Location closestStationToDelivery = GetClosestStationLocation(packageInTransfer.deliveringLocation,
                                dal.GetAllStations().Select(s => new Station(s)));
                        double distanceRequired = packageInTransfer.deliveryDistance;
                        distanceRequired += GetDistance(packageInTransfer.deliveringLocation,closestStationToDelivery);

                        if (package.pickedUp == null) // not collected
                        {
                            drone.currentLocation = closestStationLoc;
                            distanceRequired += GetDistance(closestStationLoc, packageInTransfer.collectionLocation);
                        }
                        else //collected
                        {
                            drone.currentLocation = senderLocation;
                        }

                        double batteryRequired = GetConsumptionRate(drone.weightCategory) * distanceRequired;

                        if (batteryRequired > 100)
                            throw new OperationNotPossibleException("battery required is greater than 100. Battery required was " + batteryRequired);

                        drone.battery = rand.NextDouble() * (100 - batteryRequired) + batteryRequired;
                    }
                }
                else // drone has no associated package
                {
                    int randChoice = rand.Next(2);
                    if (randChoice == 0) // free
                    {
                        drone.status = DroneStatuses.free;
                        IEnumerable<Customer> recievingCustomers = customers.Where(c => c.packagesToCustomer.Count() != 0);
                        if (recievingCustomers.Count() == 0)
                        {
                            drone.currentLocation = new Location(1, 1);
                        }
                        else
                        {
                            Customer customer = recievingCustomers.ElementAt(rand.Next(recievingCustomers.Count()));
                            drone.currentLocation = customer.currentLocation;
                        }

                        drone.battery = rand.NextDouble() * 20;
                        Location closestStation = GetClosestStationLocation(drone.currentLocation, stations);
                        double minRequired = GetDistance(drone.currentLocation, closestStation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired);
                    }
                    else // maintenance
                    {
                        drone.status = DroneStatuses.maintenance;
                        var station = stations.ElementAt(rand.Next(stations.Count()));
                        drone.currentLocation = station.location;
                        dal.SendDroneToCharge(station.ID, drone.ID);
                        drone.battery = rand.NextDouble() * 20;
                    }
                }
            }
        }

        private void CompleteStations(IEnumerable<Station> stations)
        {
            // for each drone charge record that data in station.chargingDrones
            foreach (var droneCharge in dal.GetAllCharges())
                stations.First(s => s.ID == droneCharge.StationId).chargingDrones.Append(drones.First(d => d.ID == droneCharge.DroneId));
        }

        private void CompletePackages(IEnumerable<Package> packages, IEnumerable<Customer> customers)
        {
            // Find the sender and reciever for each package based on what is in dal
            // Find drone that is carrying package
            IEnumerable<DO.Package> dalPackages = dal.GetAllPackages();
            for (int i = 0; i < packages.Count(); i++)
            {
                packages.ElementAt(i).sender = new(customers.First(c => c.ID == dalPackages.ElementAt(i).senderId));
                packages.ElementAt(i).receiver = new(customers.First(c => c.ID == dalPackages.ElementAt(i).recieverId));
                if (dalPackages.ElementAt(i).droneId != 0)
                    packages.ElementAt(i).drone = new(drones.First(d => d.ID == dalPackages.ElementAt(i).droneId));
            }
        }

        private void CompleteCustomersPackageList(IEnumerable<Customer> customers, IEnumerable<Package> packages)
        {
            // get each sent/recieved package into the customers sent/recieved list
            foreach (var customer in customers)
            {
                customer.packagesToCustomer
                    .Concat(packages
                        .Where(p => p.receiver.ID == customer.ID)
                        .Select(p => new PackageAtCustomer(p)));

                customer.packagesFromCustomer
                    .Concat(packages
                            .Where(p => p.sender.ID == customer.ID)
                            .Select(p => new PackageAtCustomer(p)));
            }
        }

        private Location GetClosestStationLocation(Location location, IEnumerable<Station> stations)
        {
            return GetClosestStation(location, stations).location;
        }

        private Station GetClosestStation(Location location, IEnumerable<Station> stations)
        {
            double min = double.PositiveInfinity;
            if (stations.Count() == 0)
            {
                throw new BlObjectAccessException("No available stations currently");
            }
            Station minStation = stations.ElementAt(0);
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
            foreach (DO.Drone drone in dal.GetAllDrones())
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
            Location closestStationToDelivery = GetClosestStationLocation(package.deliveringLocation, dal.GetAllStations().Select(s => new Station(s)));
            double deliveryToClosestStation = GetDistance(package.deliveringLocation, closestStationToDelivery);
            double distanceRequired = droneToSender + senderToDelivery + deliveryToClosestStation;
            double batteryRequired = GetConsumptionRate(drone.weightCategory) * distanceRequired;
            return batteryRequired;
        }
    }
}
