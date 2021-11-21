using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
