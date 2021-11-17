using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// a public struct object for Drones which contains all drone fields and methods
        /// </summary>
        public class Drone : IDAL.DO.DalEntity
        {
            public int ID { get; set; }
            public double Battery { get; set; }
            public string Model { get; set; }
            public DroneStatuses Status { get; set; }
            public WeightCategories MaxWeight { get; set; }
            

            public Drone(
                    int ID,
                    string Model,
                    double Battery,
                    WeightCategories MaxWeight,
                    DroneStatuses Status)
            {
                this.ID = ID;
                this.Model = Model;
                this.Battery = Battery;
                this.MaxWeight = MaxWeight;
                this.Status = Status;
            }

            public Drone(int i, Random rand)
            {
                this.ID = i + 1;
                this.Model = "Drone_" + (i + 1).ToString();
                this.Battery = 100;
                this.MaxWeight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length - 1);
                this.Status = DroneStatuses.free;
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4})",
                        ID, Model, Battery, MaxWeight.ToString(), Status);
            }

        }
    }
}
