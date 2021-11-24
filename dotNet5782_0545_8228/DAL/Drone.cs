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
            public string model { get; set; }
            public WeightCategories maxWeight { get; set; }
            

            public Drone(string model, WeightCategories maxWeight)
            {
                this.model = model;
                this.maxWeight = maxWeight;
            }

            public Drone()
            {
                Random rand = new();
                this.model = "Drone_" + (ID + 1).ToString();
                this.maxWeight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length - 1);
            }

            public override Drone Clone() => this.MemberwiseClone() as Drone;

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4})",
                        ID, model, maxWeight.ToString());
            }

        }
    }
}
