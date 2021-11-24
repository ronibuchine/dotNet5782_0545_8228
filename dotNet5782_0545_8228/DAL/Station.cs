using System;

namespace IDAL
{
    namespace DO
    {
        /// <summary>
        /// An object representing a Drone Charging station. It contains an ID, Name, the number of remaining available charging slots and its location
        /// denoted by la
        /// </summary>
        public class Station : IDAL.DO.DalEntity
        {
            public string name { get; set; }
            public int chargeSlots { get; set; } // current available
            public double longitude { get; set; }
            public double latitude { get; set; }

            public Station(int ID, string name, int chargeSlots, double longitude, double latitude)
            {
                this.name = name;
                this.chargeSlots = chargeSlots;
                this.longitude = longitude;
                this.latitude = latitude;
            }

            public Station(string name = null, int initialChargeSlots = 5)
            {
                this.name = "station_Name_" + ID.ToString(); 
                this.chargeSlots = initialChargeSlots;
                Random rand = new();
                this.longitude = (rand.NextDouble() * 360) - 180;
                this.latitude = (rand.NextDouble() * 180) - 90;
            }

            public override Station Clone() => this.MemberwiseClone() as Station;

            public override string ToString()
            {
                return String.Format("DroneStation(ID = {0}, Name = {1}, Longitude = {2}, Latitude = {3}, ChargeSlots = {4})",
                        ID, name, longitude, latitude, chargeSlots);
            }
        }
    }
}
