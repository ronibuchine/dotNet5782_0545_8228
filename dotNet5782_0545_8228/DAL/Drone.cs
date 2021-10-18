using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            double battery { get; set; }
            WeightCategories maxWeight { get; set; }
            DroneStatuses status { get; set; }
            

        }
    }
}
