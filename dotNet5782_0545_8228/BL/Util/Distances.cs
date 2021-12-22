using System;
using BL;

namespace UTIL
{
    public class Distances 
    {
        private static double toRadians(
            double angleIn10thofaDegree)
        {
            // Angle in 10th
            // of a degree
            return (angleIn10thofaDegree * 
                        Math.PI) / 180;
        }

        public static double GetDistance(Location l1, Location l2)                  
        {
    
            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            double lon1 = toRadians(l1.longitude);
            double lon2 = toRadians(l2.longitude);
            double lat1 = toRadians(l1.latitude);
            double lat2 = toRadians(l2.latitude);
    
            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Pow(Math.Sin(dlon / 2),2);
                
            double c = 2 * Math.Asin(Math.Sqrt(a));
    
            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            double r = 7000;
    
            // calculate the result
            return (c * r);
        }
    }
}
