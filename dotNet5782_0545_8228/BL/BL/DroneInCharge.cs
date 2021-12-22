using System;

namespace BL
{
    
    class DroneInCharge : BLEntity
    {
        public double? battery { get; set; }

        internal DroneInCharge(Drone drone)
        {
            ID = drone.ID;
            battery = drone.battery;

        }
        public override string ToString()
        {
            return String.Format("ID = {0}, Battery Level = {1}", ID, battery);
        }
    }
    
    
}
