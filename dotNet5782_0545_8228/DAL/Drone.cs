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
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            

            public Drone(
                    int ID,
                    string Model,
                    WeightCategories MaxWeight)
            {
                this.ID = ID;
                this.Model = Model;
                this.MaxWeight = MaxWeight;
            }

            public Drone(int i, Random rand)
            {
                this.ID = i + 1;
                this.Model = "Drone_" + (i + 1).ToString();
                this.MaxWeight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length - 1);
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Model = {1}, Battery = {2}, MaxWeight = {3}, Status = {4})",
                        ID, Model, MaxWeight.ToString());
            }

        }
    }
}
