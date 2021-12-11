﻿using System;

namespace IBL
{
    namespace BO
    {
        public class DroneInDelivery
        {
            public int ID { get; set; }
            /* public double battery { get; set; } */
            /* public Location currentLocation { get; set; } */
            
            public DroneInDelivery(IDAL.DO.Drone drone)
            {
                ID = drone.ID;
            }
            
            public DroneInDelivery(Drone drone)
            {
                ID = drone.ID;
            }
            
            public override string ToString()
            {
                return String.Format("ID = {0}, Current Location = {2}",
                    ID);
            }
        }
    }
    
}
