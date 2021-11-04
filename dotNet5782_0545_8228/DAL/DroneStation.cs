using System;

namespace IDAL
{
    namespace DO
    {
        public struct DroneStation : DalStruct
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int ChargeSlots { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public DroneStation(
                    int ID,
                    string Name,
                    int ChargeSlots,
                    double Longitude,
                    double Latitude)
            {
                this.ID = ID;
                this.Name = Name;
                this.ChargeSlots = ChargeSlots;
                this.Longitude = Longitude;
                this.Latitude = Latitude;
            }

            public DroneStation(int i, Random rand)
            {
                this.ID = i + 1;
                this.Name = "Drone_" + (i + 1).ToString();
                this.ChargeSlots = rand.Next(5);
                this.Longitude = (rand.NextDouble() * 360) - 180;
                this.Latitude = (rand.NextDouble() * 180) - 90;
            }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Name = {1}, Longitude = {2}, Latitude = {3}, ChargeSlots = {4})",
                        ID, Name, Longitude, Latitude, ChargeSlots);
            }
        }
    }
}
