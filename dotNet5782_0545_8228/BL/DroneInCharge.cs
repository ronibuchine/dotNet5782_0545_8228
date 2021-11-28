using System;

namespace BL
{
    namespace BO
    {
        class DroneInCharge
        {
            public int ID { get; set; }
            public double battery { get; set; }
            public override string ToString()
            {
                return String.Format("ID = {0}, Battery Level = {1}", ID, battery);
            }
        }
    }
    
}
