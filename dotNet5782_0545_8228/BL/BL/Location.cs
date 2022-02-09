using System;


namespace BL
{ 
    /// <summary>
    ///  a Location object which stores a latitude and longitude value
    /// </summary>
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
            return String.Format("Longitude = {0}\nLatitude = {1}",
                longitude, latitude);
        }

        public override bool Equals(object obj)
        {
            return obj is Location location &&
                    this.longitude == location.longitude &&
                    this.latitude == location.latitude;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(longitude, latitude);
        }
    }
}

