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
            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Location = {2}, Charge Slots = {3}, Charging Drones = {4}",
                    ID, name, location.ToString(), chargeSlots, chargingDrones.ToString());
            }
        }
    }
    
}
