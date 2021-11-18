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
            public override string ToString()
            {
                return String.Format("ID = {0}, Weight = {1}, Priority = {2}, Package Status = {3}, Other Party = {4}",
                    ID, weight, priority, status, senderReceiver.ToString());
            }
        }
    }
    
}
