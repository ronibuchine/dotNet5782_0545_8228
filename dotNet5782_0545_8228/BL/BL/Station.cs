using System;
using System.Collections.Generic;
using System.Linq;


namespace BL
{
    /// <summary>
    /// An entity which represents a statino in our delivery system. It hosts drones which are currently charging
    /// </summary>
    public class Station : BLEntity
    {
        public string name { get; set; }
        public Location location { get; set; }
        public int chargeSlots { get; set; }
        public IEnumerable<Drone> chargingDrones { get; set; }

        public Station(string name, Location location, int chargeSlots)
        {
            this.name = name;
            this.location = location;
            this.chargeSlots = chargeSlots; // available chargeSlots
            this.chargingDrones = new List<Drone>(chargeSlots); // currently charging drones
        }


        public Station(DO.Station station) : base(null)
        {
            DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();
            ID = station.ID;
            name = station.name;
            location = new Location(station.longitude, station.latitude);
            chargeSlots = station.chargeSlots;
            chargingDrones = dal.GetAllCharges()
                .Where(dc => dc.StationId == ID)
                .Select(dc => BLFactory.GetBL().GetDrone(dc.DroneId));
        }

        public override string ToString()
        {
            return String.Format("ID = {0}, Name = {1}, Location = {2}, Charge Slots = {3}, Charging Drones = {4}",
                ID, name, location.ToString(), chargeSlots, chargingDrones.ToString());
        }
    }
}


