using System;
using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        public void AddCustomer() => AddDalItem((List<Customer>)DataSource.customers, DataSource.MAX_CUSTOMERS, IdalDoType.CUSTOMER);

        public void AddCustomer(Customer customer) =>
            AddDalItem((List<Customer>)DataSource.customers, DataSource.MAX_CUSTOMERS, customer, IdalDoType.CUSTOMER);

        public IEnumerable<Customer> GetAllCustomers() => GetAllItems(DataSource.customers);

        public Customer GetCustomer(int ID) => GetOneItem(DataSource.customers, ID);

        public Customer GetActualCustomer(int ID) => GetActualOneItem(DataSource.customers, ID);

        public void ProvidePackageToCustomer(int packageID)
        {
            GetActualPackage(packageID).delivered = DateTime.Now;
        }
    }
}
