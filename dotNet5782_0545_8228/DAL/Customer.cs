using System;

namespace IDAL
{
    namespace DO
    {
        public class Customer : ABCDalObject
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public Customer(
                    int ID,
                    string Name,
                    string Phone,
                    double Longitude,
                    Double Latitude)
            {
                this.ID = ID;
                this.Name = Name;
                this.Phone = Phone;
                this.Longitude = Longitude;
                this.Latitude = Latitude;
            }

            public Customer( int i, Random rand)
            {
                this.ID = i;
                this.Name = "custName_" + i.ToString();
                this.Phone = (i * rand.Next()).ToString();
                this.Longitude = (rand.NextDouble() * 360) - 180;
                this.Latitude = (rand.NextDouble() * 180) - 90;
            }

            public override string ToString()
            {
                return String.Format("Customer(ID = {0}, Name = {1}, Phone = {2}, Longitude = {3}, Latitude = {4})",
                        ID, Name, Phone, Longitude, Latitude);
            }
        }
    }
}
