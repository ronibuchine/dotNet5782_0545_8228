using System;

namespace IBL
{
    namespace BO
    {
        public class CustomerInPackage
        {
            public int ID { get; set; }
            public string name { get; set; }
            
            public CustomerInPackage(IDAL.DO.Customer customer)
            {
                ID = customer.ID;
                name = customer.name;
            }

            public CustomerInPackage(Customer customer)
            {
                ID = customer.ID;
                name = customer.name;
            }

            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}",
                    ID, name);
            }
        }
    }
    
}
