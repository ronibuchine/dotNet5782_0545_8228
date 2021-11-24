using System;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public Customer(int ID, string name, string phone, Location location)
            {
                this.ID = ID;
                this.name = name;
                this.phone = phone;
                this.currentLocation = location;
            }
            public Customer(IDAL.DO.Customer customer)
            {
                ID = customer.ID;
                name = customer.Name;
                phone = customer.Phone;
                currentLocation.longitude = customer.Longitude;
                currentLocation.latitude = customer.Latitude;
            }
            public int ID { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public Location currentLocation { get; set; }
            public List<Package> packagesFromCustomer { get; set; }
            public List<Package> packagesToCustomer { get; set; }
            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Current Location = {2}, Packages from Customer = {3}, Packages to Customer = {4}}",
                    ID, name, phone, currentLocation.ToString(), packagesFromCustomer.ToString(), packagesToCustomer.ToString());
            }
        }
    }
}
