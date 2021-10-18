using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// a public struct object for Drones which contains all drone fields anf methods
        /// </summary>
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public double Battery { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            

        }
    }
}
