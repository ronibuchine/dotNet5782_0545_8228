using System;
using System.Collections.Generic;
using DalObjectNamespace;

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

            public DroneStation(int ID, string name, Location location, int chargeSlots)
            {
                this.ID = ID;
                this.name = name;
                this.location = location;
                this.chargeSlots = chargeSlots; // available chargeSlots
                this.chargingDrones = new List<Drone>(chargeSlots);
            }
            
            public DroneStation(IDAL.DO.Station droneStation)
            {
                IDAL.IdalInterface dal = DalObject.GetInstance();
                ID = droneStation.ID;
                name = droneStation.name;
                location = new Location(droneStation.longitude, droneStation.latitude);
                chargeSlots = droneStation.chargeSlots;
            }
            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Location = {2}, Charge Slots = {3}, Charging Drones = {4}",
                    ID, name, location.ToString(), chargeSlots, chargingDrones.ToString());
            }
        }
    }
    
}
