using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phone { get; set; }
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
