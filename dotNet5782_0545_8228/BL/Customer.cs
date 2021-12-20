using System;
using System.Linq;
using System.Collections.Generic;

namespace IBL
{
    namespace BO
    {
        public class Customer : BLEntity
        {
            public string name { get; set; }
            public string phone { get; set; }
            public Location currentLocation { get; set; }
            public IEnumerable<PackageAtCustomer> packagesFromCustomer { get; set; }
            public IEnumerable<PackageAtCustomer> packagesToCustomer { get; set; }

            public Customer(string name, string phone, Location location)
            {
                this.name = name;
                this.phone = phone;
                this.currentLocation = location;
            }

            public Customer(DO.Customer customer) : base(null)
            {
                ID = customer.ID;
                name = customer.name;
                phone = customer.phone;
                currentLocation = new Location(customer.longitude, customer.latitude);

                DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();
                packagesFromCustomer = dal.GetAllPackages()
                    .Where(p => p.senderId == ID)
                    .Select(p => new PackageAtCustomer(p));
                packagesToCustomer = dal.GetAllPackages()
                    .Where(p => p.recieverId == ID)
                    .Select(p => new PackageAtCustomer(p));
            }

            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Current Location = {2}, Packages from Customer = {3}, Packages to Customer = {4}}",
                    ID, name, phone, currentLocation.ToString(), packagesFromCustomer.ToString(), packagesToCustomer.ToString());
            }
        }
    }
}
