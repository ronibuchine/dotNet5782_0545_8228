using System;
using System.Linq;
using System.Collections.Generic;
using DALAPI;
using static UTIL.Distances;



namespace BL
{
    /// <summary>
    /// This class is the class which controls the implementations of the BLAPI.
    /// The class contains Add, Update, Remove, Get and other various action calls.
    /// </summary>
    public partial class BLOBject : IBL.IBLInterface
    {
        private static double chargingRate { get; set; }
        private static double free { get; set; }
        private static double lightWeight { get; set; }
        private static double midWeight { get; set; }
        private static double heavyWeight { get; set; }
        internal IDAL dal { get; }
        private List<Drone> drones;

        /// <summary>
        /// Constructor used for testing and debugging, empty initializes the data layer.
        /// </summary>
        /// <param name="_">meant to be a null object</param>
        public BLOBject(Object _)
        {
            this.dal = DalFactory.GetDal();
            dal.Clear();
            CommonCtor(this.dal);
        }

        /// <summary>
        /// Calls the DALFactory to construct a new data layer.
        /// </summary>
        public BLOBject()
        {
            this.dal = DalFactory.GetDal();
            CommonCtor(this.dal);
        }

        /// <summary>
        /// Performs logical checks on the random data objects which were initialized in the data layer.
        /// Ensures entities which are in valid states and battery levels
        /// </summary>
        /// <param name="dal"></param>
        private void CommonCtor(IDAL dal)
        {
            BLEntity.nextID = dal.GetNextID();
            double[] powerConsumption = dal.PowerConsumptionRequest();
            free = powerConsumption[0];
            lightWeight = powerConsumption[1];
            midWeight = powerConsumption[2];
            heavyWeight = powerConsumption[3];
            chargingRate = powerConsumption[4];

            drones = dal.GetAllDrones().Select(d => new Drone(d)).ToList();
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
                Package package = packages.FirstOrDefault(p => p.drone != null && p.drone.ID == drone.ID);                
                if (package == null) 
                {

                    int randChoice = rand.Next(2);
                    var droneCharge = dal.GetAllCharges().FirstOrDefault(dc => dc.droneId == drone.ID);
                    if (droneCharge != null) // for existing drones from DALXml
                    {
                        drone.status = DroneStatuses.maintenance;
                        var station = stations.FirstOrDefault(s => s.ID == droneCharge.stationId);
                        drone.currentLocation = station.location;
                        drone.battery = rand.NextDouble() * 20;
                    }
                    else if (randChoice == 0) // free
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
                       
                        Location closestStation = GetClosestStationLocation(drone.currentLocation, stations);
                        double minRequired = GetDistance(drone.currentLocation, closestStation) * GetConsumptionRate(drone.weightCategory);
                        drone.battery = rand.NextDouble() * (100 - minRequired) + minRequired;
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
                else
                {
                    PackageInTransfer packageInTransfer = new(package);
                    if (!packageInTransfer.delivered)
                        drone.packageInTransfer = packageInTransfer;

                    Location senderLocation = customers.First(c => c.ID == package.sender.ID).currentLocation;
                    Location reciverLocation = customers.First(c => c.ID == package.receiver.ID).currentLocation;
                    Location closestStationLoc = GetClosestStationLocation(senderLocation, stations);
                    Location closestStationToDelivery = GetClosestStationLocation(packageInTransfer.deliveringLocation, dal.GetAllStations().Select(s => new Station(s)));

                    if (package.delivered == null)
                    {
                        drone.status = DroneStatuses.delivery;
                        double distanceRequired = packageInTransfer.deliveryDistance;
                        distanceRequired += GetDistance(packageInTransfer.deliveringLocation, closestStationToDelivery);

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
                    else // post delivery
                    {
                        drone.status = DroneStatuses.free;
                        drone.currentLocation = reciverLocation;
                        drone.battery = GetDistance(reciverLocation, closestStationToDelivery) * GetConsumptionRate(drone.weightCategory) + 0.1;
                    }

                }
                
                
            }
            
        }

        /// <summary>
        /// Ensures that all the Station fields are set correctly.
        /// </summary>
        /// <param name="stations"></param>
        private void CompleteStations(IEnumerable<Station> stations)
        {
            // for each drone charge record that data in station.chargingDrones
            foreach (var droneCharge in dal.GetAllCharges())
                stations.First(s => s.ID == droneCharge.stationId).chargingDrones.Append(drones.First(d => d.ID == droneCharge.droneId));
        }

        /// <summary>
        /// Ensures tht all the Package fields are initialized correctly.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="customers"></param>
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

        /// <summary>
        /// Ensures that the package list inside the Customer entity is initialized correctly.
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="packages"></param>
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

        /// <summary>
        /// Given a location, this function will determine the closest station to that location.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="stations"></param>
        /// <returns>The closest station to the passed location.</returns>
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

        /// <summary>
        /// This will retrieve the consumption rate for each weight category in the system
        /// </summary>
        /// <param name="weight"></param>
        /// <returns>Consumption rate per hour of use</returns>
        internal double GetConsumptionRate(WeightCategories weight)
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

        /// <summary>
        /// Determines the minimum battery required for a drone to reach a specified location
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="location"></param>
        /// <returns>The minimum battery level needed to travel the distance</returns>
        private double GetMinBatteryRequired(Drone drone, Location location)
        {
            return GetDistance(location, drone.currentLocation) * GetConsumptionRate(drone.weightCategory);
        }

        
        private bool CanArriveToLocation(Drone drone, Location location)
        {
            return GetMinBatteryRequired(drone, location) < drone.battery;
        }

        /// <summary>
        /// ID validation method
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>a boolean value if the ID is valid</returns>
        private Boolean IsValidID(int ID)
        {
            if (ID < 0) // other conditions here?
                return false;
            else
                return true;
        }

        /// <summary>
        /// Determines the battery level required for a given drone to deliver a specific package.
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="package"></param>
        /// <returns>The desired battery level needed</returns>
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
