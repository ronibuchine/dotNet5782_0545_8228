using System;

namespace IBL
{
    namespace BO
    { 
        public class Drone
        {
            public Drone(int ID, string model, WeightCategories weightCategory)
            {
                this.ID = ID;
                this.model = model;
                this.weightCategory = weightCategory;
            }
            public Drone(IDAL.DO.Drone drone)
            {
                ID = drone.ID;
                model = drone.Model;
                weightCategory = (IBL.BO.WeightCategories)drone.MaxWeight;
            }
            public int ID { get; set; }
            public double battery { get; set; }
            public string model { get; set; }
            public DroneStatuses status { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Location currentLocation { get; set; }
            public PackageInTransfer packageInTransfer {get; set;}


            public Drone(
                int ID,
                string model,
                double battery,
                WeightCategories category,
                DroneStatuses status,
                Location location,
                PackageInTransfer packageInTransfer)
            {
                this.ID = ID;
                this.model = model;
                this.battery = battery;
                this.weightCategory = category;
                this.status = status;
                this.currentLocation = location;
            }

            public Drone(int i, Random rand)
            {
                this.ID = i + 1;
                this.model = "Drone_" + (i + 1).ToString();
                this.battery = 100;
                this.weightCategory = (IBL.BO.WeightCategories)rand.Next(Enum.GetNames(typeof(IBL.BO.WeightCategories)).Length - 1);
                this.status = DroneStatuses.free;
                this.currentLocation = new Location(i, i); // TODO: fix
                this.packageInTransfer = new PackageInTransfer();
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4}, Current Location = {5}, Package = {6})",
                    ID, model, battery, weightCategory.ToString(), status, currentLocation, packageInTransfer);
            }
        }
    }
}
