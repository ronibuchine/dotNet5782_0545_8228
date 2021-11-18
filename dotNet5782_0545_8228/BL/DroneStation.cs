using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneStation
        {
            public int ID { get; set; }
            public string name { get; set;}
            public Location location { get; set; }
            public int chargeSlots { get; set; }
            public List<Drone> chargingDrones { get; set; }
        }
    }
    
}
