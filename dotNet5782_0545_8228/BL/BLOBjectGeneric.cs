using System;
using System.Collections.Generic;
using IDAL;
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
            this.dal = new DalObject.DalObject();

            this.free = DalObject.DataSource.Config.free;
            this.lightWeight = DalObject.DataSource.Config.lightWeight;
            this.midWeight = DalObject.DataSource.Config.midWeight;
            this.heavyWeight = DalObject.DataSource.Config.heavyWeight;
            this.chargingRate = DalObject.DataSource.Config.chargingRate;

            drones = dal.GetAllDrones().ConvertAll((d) =>
            {
                return new Drone(d);
            });

            List<Customer> customers = dal.GetAllCustomers().ConvertAll((c) =>
            {
                return new Customer(c);
            });

            List<DroneStation> stations = dal.GetAllDroneStations().ConvertAll((ds) =>
            {
                return new DroneStation(ds);
            });
            Random rand = new Random();
            foreach (IDAL.DO.Parcel package in dal.GetAllParcels())
            {
                Drone thisDrone = drones.Find((d) => { return d.ID == package.droneId; });
                Customer sender = customers.Find((c) => { return c.ID == package.senderId; });

                // in delivery
                if (thisDrone != null)
                {
                    thisDrone.status = DroneStatuses.delivery;
                    Location closestStationLoc = GetClosestStationLocation(sender.currentLocation, stations);
                    double minRequired = GetDistance(closestStationLoc, sender.currentLocation) * GetConsumptionRate(thisDrone.weightCategory);
                    thisDrone.battery = rand.NextDouble() * (100 - minRequired);
                    // not collected
                    if (DateTime.Compare(package.pickedUp, DateTime.Now) > 0)
                    {
                        thisDrone.currentLocation = closestStationLoc;
                    }
                    else // collected
                    {
                        thisDrone.currentLocation = sender.currentLocation;
                    }
                }
                else thisDrone.status = (DroneStatuses)rand.Next(Enum.GetNames(typeof(DroneStatuses)).Length - 1);

                if (thisDrone.status == DroneStatuses.free)
                {
                    List<Customer> customersThatRecievedPackaged =
                        customers.FindAll((c) => { return c.packagesToCustomer.Count != 0; });
                    thisDrone.currentLocation = customersThatRecievedPackaged[rand.Next(customersThatRecievedPackaged.Count)].currentLocation;
                    Location closestStationLoc = GetClosestStationLocation(thisDrone.currentLocation, stations);
                    double minRequired = GetDistance(closestStationLoc, thisDrone.currentLocation) * GetConsumptionRate(thisDrone.weightCategory);
                    thisDrone.battery = rand.NextDouble() * (100 - minRequired);
                }
                else if (thisDrone.status == DroneStatuses.maintenance)
                {
                    thisDrone.currentLocation = stations[rand.Next(stations.Count)].location;
                    thisDrone.battery = rand.NextDouble() * (20);
                }
            }

        }

        private Boolean CheckDroneID(int ID)
        {
            if (ID < 0) throw new InvalidIDException("ERROR: ID cannot be negative");
            foreach (IDAL.DO.Drone drone in dal.GetAllDrones())
            {
                if (drone.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }
        private Boolean CheckStationID(int ID)
        {
            if (ID < 0) throw new InvalidIDException("ERROR: ID cannot be negative");
            foreach (IDAL.DO.DroneStation station in dal.GetAllDroneStations())
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
            foreach (IDAL.DO.Parcel package in dal.GetAllParcels())
            {
                if (package.ID == ID) throw new InvalidIDException("ERROR: This entity already exists.");
            }
            return true;
        }

    }
}
