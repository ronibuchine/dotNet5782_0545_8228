using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int ID {get; set;}
            public string name { get; set; }
            public int phoneNumber { get; set; }
            public int numberPackagesDelivered { get; set; }
            public int numberPackagesUndelivered { get; set; }
            public int numberReceivedPackages { get; set; }
            public int numberExpectedPackages { get; set; }

            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Phone Number = {2}, Packages Delivered = {3}, Packages Not Delivered = {4}, Received Packages = {5}, Expected Packages = {6}",
                    ID, name, phoneNumber, numberPackagesDelivered, numberPackagesUndelivered, numberReceivedPackages, numberExpectedPackages);
            }
    }
    }

}