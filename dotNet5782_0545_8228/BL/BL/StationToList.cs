using System;
using System.Linq;


namespace BL
{
    /// <summary>
    /// A station entity which is used for list representation.
    /// </summary>
    public class StationToList
    {
        public StationToList(Station station)
        {
            ID = station.ID;
            name = station.name;
            availableChargeSlots = station.chargeSlots - station.chargingDrones.Count();
            occupiedSlots = station.chargingDrones.Count();
        }
        public int ID { get; set; }
        public string name { get; set; }
        public int availableChargeSlots { get; set; }
        public int occupiedSlots { get; set; }
        public override string ToString()
        {
            return String.Format("ID = {0}, Name = {1}, Current Available Charge Slots = {2}, Current Occupied Charge Slots = {3}",
                ID, name, availableChargeSlots, occupiedSlots);
        }
    }
}


