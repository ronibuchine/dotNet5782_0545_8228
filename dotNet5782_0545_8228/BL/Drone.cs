using System;

namespace IBL
{
    namespace BO
    { 
        public class Drone
        {
            public int ID { get; set; }
            public string model { get; set; }
            public WeightCategories weightCategory { get; set; }
            public double? battery { get; set; }
            public DroneStatuses? status { get; set; }
            public Location currentLocation { get; set; }
            public PackageInTransfer packageInTransfer {get; set;}

            public Drone(IDAL.DO.Drone drone)
            {
                ID = drone.ID;
                model = drone.model;
                weightCategory = (IBL.BO.WeightCategories)drone.maxWeight;
            }

            public Drone(
                int ID,
                string model,
                WeightCategories category,
                double? battery = null,
                DroneStatuses? status = null,
                Location location = null,
                PackageInTransfer packageInTransfer = null)
            {
                this.ID = ID;
                this.model = model;
                this.battery = battery;
                this.weightCategory = category;
                this.status = status;
                this.currentLocation = location;
                this.packageInTransfer = packageInTransfer;
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4}, Current Location = {5}, Package = {6})",
                    ID, model, battery, weightCategory.ToString(), status, currentLocation, packageInTransfer);
            }
        }
    }
}
