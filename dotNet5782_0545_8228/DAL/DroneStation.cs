using System;

namespace IDAL
{
    namespace DO
    {
        struct DroneStation
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlots { get; set; }

            public override string ToString()
            {
                return String.Format("Drone(ID = {0}, Name = {1}, Longitude = {2}, Latitude = {3}, ChargeSlots = {4})",
                        ID, Name, Longitude, Latitude, ChargeSlots);
            }
        }
    }
}
