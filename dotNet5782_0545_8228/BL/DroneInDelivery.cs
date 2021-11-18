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
            public override string ToString()
            {
                return String.Format("ID = {0}, Battery Level = {1}, Current Location = {2}",
                    ID, battery, currentLocation.ToString());
            }
        }
    }
    
}
