using System;

namespace IBL
{
    namespace BO
    { 
        public class Location
        {
            public Location(double longitude, double latitude)
            {
                this.longitude = longitude;
                this.latitude = latitude;
            }
            public double longitude { get; set; }
            public double latitude { get; set; }

            public override string ToString()
            {
                return String.Format("Longitude = {0}, Latitude = {1}",
                    longitude, latitude);
            }

            public override bool Equals(object obj)
            {
                Location other = (Location)obj;
                return other.longitude == longitude && other.latitude == latitude;
            }
        }
    }
}
