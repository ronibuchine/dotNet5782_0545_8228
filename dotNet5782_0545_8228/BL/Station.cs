using System;
using System.Collections.Generic;
using DalObjectNamespace;

namespace IBL
{
    namespace BO
    {
        public class Station : BLEntity
        {
            public string name { get; set;}
            public Location location { get; set; }
            public int chargeSlots { get; set; }
            public List<Drone> chargingDrones { get; set; }

            public Station(string name, Location location, int chargeSlots)
            {
                this.name = name;
                this.location = location;
                this.chargeSlots = chargeSlots; // available chargeSlots
                this.chargingDrones = new List<Drone>(chargeSlots); // currently charging drones
            }
            
            public Station(IDAL.DO.Station station) : base(null)
            {
                IDAL.IdalInterface dal = DalObject.GetInstance();
                ID = station.ID;
                name = station.name;
                location = new Location(station.longitude, station.latitude);
                chargeSlots = station.chargeSlots;
                chargingDrones = new(chargeSlots);
                dal.GetAllCharges()
                    .FindAll(dc => dc.StationId == ID)
                    .ForEach(dc => chargingDrones.Add(new Drone(dal.GetDrone(dc.DroneId))));
            }

            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Location = {2}, Charge Slots = {3}, Charging Drones = {4}",
                    ID, name, location.ToString(), chargeSlots, chargingDrones.ToString());
            }
        }
    }
    
}
