using System;
using UTIL;


namespace BL
{
    /// <summary>
    /// The entity of a drone in our system. This class keeps all the relevant information on drones 
    /// </summary>
    public class Drone : BLEntity
    {
        public string model { get; set; }
        public WeightCategories weightCategory { get; set; }
        public double? battery { get; set; }
        public DroneStatuses? status { get; set; }
        public Location currentLocation { get; set; }
        public PackageInTransfer packageInTransfer { get; set; }

        public Drone(
            string model,
            WeightCategories category,
            double? battery = null,
            DroneStatuses? status = null,
            Location location = null,
            PackageInTransfer packageInTransfer = null)
        {
            this.model = model;
            this.battery = battery;
            this.weightCategory = category;
            this.status = status;
            this.currentLocation = location;
            this.packageInTransfer = packageInTransfer;
        }

        public Drone(DO.Drone drone) : base(null)
        {
            ID = drone.ID;
            model = drone.model;
            weightCategory = (WeightCategories)drone.maxWeight;
        }

        public override string ToString()
        {
            return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4}, Current Location = {5}, Package = {6})",
                ID, 
                model, 
                PrintDebug.ToStringOrNull(battery),
                weightCategory.ToString(),
                status,
                PrintDebug.ToStringOrNull(currentLocation),
                PrintDebug.ToStringOrNull(packageInTransfer));
        }
    }
}

