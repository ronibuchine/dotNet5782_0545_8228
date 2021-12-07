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

        public IDAL.DO.Customer GetActualCustomer(int ID) => GetActualOneItem(DataSource.customers, ID);

        public void ProvidePackageToCustomer(int packageID)
        {
            GetActualPackage(packageID).delivered = DateTime.Now;
        }
    }
}
