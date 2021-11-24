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
            private static int nextID = 0;
            public string model { get; set; }
            public WeightCategories maxWeight { get; set; }
            

            public Drone(
                    int ID,
                    string model,
                    WeightCategories maxWeight)
            {
                this.ID = ID;
                this.model = model;
                this.maxWeight = maxWeight;
            }

            public Drone(string model, WeightCategories maxWeight)
            {
                this.ID = nextID++;
                this.model = model;
                this.maxWeight = maxWeight;
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4})",
                        ID, model, maxWeight.ToString());
            }

        }
    }
}
