using System;

namespace IDAL
{
    namespace DO
    {
        public class Customer : DalEntity
        {
            public string name { get; set; }
            public string phone { get; set; }
            public double longitude { get; set; }
            public double latitude { get; set; }

            public Customer(
                    int ID,
                    string name,
                    string phone,
                    double longitude,
                    Double latitude) 
                : base(ID)
            {
                this.name = name;
                this.phone = phone;
                this.longitude = longitude;
                this.latitude = latitude;
            }

            public Customer(int ID) : base(ID)
            {
                Random rand = new();
                this.name = "custName_" + ID.ToString();
                this.phone = (ID * rand.Next(1000000,9999999)).ToString();
                this.longitude = (rand.NextDouble() * 360) - 180;
                this.latitude = (rand.NextDouble() * 180) - 90;
            }

            public override Customer Clone() => this.MemberwiseClone() as Customer;

            public override string ToString()
            {
                return String.Format("Customer(ID = {0}, Name = {1}, Phone = {2}, Longitude = {3}, Latitude = {4})",
                        ID, name, phone, longitude, latitude);
            }
        }
    }
}
