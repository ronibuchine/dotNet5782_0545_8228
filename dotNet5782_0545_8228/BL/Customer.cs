using System;
using System.Collections.Generic;
using DalObjectNamespace;

namespace IBL
{
    namespace BO
    {
        public class Customer : BLEntity
        {
            public string name { get; set; }
            public string phone { get; set; }
            public Location currentLocation { get; set; }
            public List<Package> packagesFromCustomer { get; set; }
            public List<Package> packagesToCustomer { get; set; }

            public Customer(string name, string phone, Location location)
            {
                this.name = name;
                this.phone = phone;
                this.currentLocation = location;
            }

            public Customer(IDAL.DO.Customer customer) : base(null)
            {
                ID = customer.ID;
                name = customer.name;
                phone = customer.phone;
                currentLocation = new Location(customer.longitude, customer.latitude);
                packagesFromCustomer = DalObject.GetInstance().GetAllPackages().FindAll(p => p.senderId == ID).ConvertAll(p => new Package(p));
                packagesToCustomer = DalObject.GetInstance().GetAllPackages().FindAll(p => p.recieverId == ID).ConvertAll(p => new Package(p));
            }

            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Current Location = {2}, Packages from Customer = {3}, Packages to Customer = {4}}",
                    ID, name, phone, currentLocation.ToString(), packagesFromCustomer.ToString(), packagesToCustomer.ToString());
            }
        }
    }
}
