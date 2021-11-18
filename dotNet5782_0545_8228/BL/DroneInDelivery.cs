using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class DroneInDelivery
        {
            public int ID { get; set; }
            public double battery { get; set; }
            public Location currentLocation { get; set; }
        }
    }
    
}
