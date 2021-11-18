using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Customer
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int phone { get; set; }
            public Location currentLocation { get; set; }
            public List<Package> packagesFromCustomer { get; set; }
            public List<Package> packagesToCustomer { get; set; }
        }
    }
}
