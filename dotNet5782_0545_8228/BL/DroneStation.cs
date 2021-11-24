﻿using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public class DroneStation
        {
            public DroneStation(int ID, string name, Location location, int chargeSlots)
            {
                this.ID = ID;
                this.name = name;
                this.location = location;
                this.chargeSlots = chargeSlots; // available chargeSlots
                this.chargingDrones = new List<Drone>(chargeSlots);
            }
            public DroneStation(IDAL.DO.DroneStation droneStation)
            {
                ID = droneStation.ID;
                name = droneStation.Name;
                location.longitude = droneStation.Longitude;
                location.latitude = droneStation.Latitude;
            }
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
