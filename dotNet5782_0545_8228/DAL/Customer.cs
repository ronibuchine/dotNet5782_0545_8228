using System;

namespace IDAL
{
    namespace DO
    {
        struct Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public override string ToString()
            {
                return String.Format("Customer(ID = {0}, Name = {1}, Phone = {2}, Longitude = {3}, Latitude = {4})",
                        ID, Name, Phone, Longitude, Latitude);
            }
        }
    }
}
