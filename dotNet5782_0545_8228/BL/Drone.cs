using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    { 
        public class Drone
        {
            public int ID { get; set; }
            public double battery { get; set; }
            public string model { get; set; }
            public DroneStatuses status { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Location currentLocation { get; set; }


            public Drone(
                int ID,
                string model,
                double battery,
                WeightCategories category,
                DroneStatuses status,
                Location location)
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
                this.currentLocation = new Location(); // TODO: fix
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4}, Current Location = {5})",
                    ID, model, battery, weightCategory.ToString(), status, currentLocation);
            }
        }
    }
}
