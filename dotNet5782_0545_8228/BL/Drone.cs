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
            public double Battery { get; set; }
            public string Model { get; set; }
            public DroneStatuses Status { get; set; }
            public WeightCategories WeightCategory { get; set; }
            public Location CurrentLocation { get; set; }


            public Drone(
                int ID,
                string model,
                double battery,
                WeightCategories category,
                DroneStatuses status,
                Location location)
            {
                this.ID = ID;
                this.Model = model;
                this.Battery = battery;
                this.WeightCategory = category;
                this.Status = status;
                this.CurrentLocation = location;
            }

            public Drone(int i, Random rand)
            {
                this.ID = i + 1;
                this.Model = "Drone_" + (i + 1).ToString();
                this.Battery = 100;
                this.WeightCategory = (IBL.BO.WeightCategories)rand.Next(Enum.GetNames(typeof(IBL.BO.WeightCategories)).Length - 1);
                this.Status = DroneStatuses.free;
                this.CurrentLocation = new Location(); // TODO: fix
            }

            public Drone(IDAL.DO.Drone dalDrone)
            {
                this.ID = dalDrone.ID;
                this.Model = dalDrone.Model;
                this.Battery =  ;// TODODODODODOD
                this.WeightCategory = ;// TODODODODODOD 
                this.Status = ;// TODODODODODOD 
                this.CurrentLocation =;// TODODODODODOD   // TODO: fix

            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4}, Current Location = {5})",
                    ID, Model, Battery, WeightCategory.ToString(), Status, CurrentLocation);
            }
        }
    }
}
