using System;
using System.Collections.Generic;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddCustomer() => AddDalItem(DataSource.customers, IdalDoType.CUSTOMER);

        public void AddCustomer(IDAL.DO.Customer customer) =>
            AddDalItem(DataSource.customers, customer, IdalDoType.CUSTOMER);

        public List<IDAL.DO.Customer> GetAllCustomers() => GetAllItems(DataSource.customers);

        public IDAL.DO.Customer GetCustomer(int ID) => GetOneItem(DataSource.customers, ID);
        private IDAL.DO.Customer _GetCustomer(int ID) => _GetOneItem(DataSource.customers, ID);

        public void ProvidePackageToCustomer(int packageID)
        {
            if (packageID < 0 || packageID > DataSource.packages.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            
            DataSource.packages[packageID].delivered = DateTime.Now;
        }
    }
}
