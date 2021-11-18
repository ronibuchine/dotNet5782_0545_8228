using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class PackageAtCustomer
        {
            public int ID { get; set; }
            public double weight { get; set; }
            public Priorities priority { get; set; }
            public PackageStatuses status { get; set; }
            public CustomerInPackage senderReceiver { get; set; }
        }
    }
    
}
