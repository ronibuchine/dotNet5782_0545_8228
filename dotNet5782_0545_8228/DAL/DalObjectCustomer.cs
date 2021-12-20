using System;
using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IdalInterface
    {

        public void AddCustomer() => AddDalItem(DataSource.customers, IdalDoType.CUSTOMER);

        public void AddCustomer(Customer customer) =>
            AddDalItem(DataSource.customers, customer, IdalDoType.CUSTOMER);

        public List<Customer> GetAllCustomers() => GetAllItems(DataSource.customers);

        public Customer GetCustomer(int ID) => GetOneItem(DataSource.customers, ID);

        public Customer GetActualCustomer(int ID) => GetActualOneItem(DataSource.customers, ID);

        public void ProvidePackageToCustomer(int packageID)
        {
            GetActualPackage(packageID).delivered = DateTime.Now;
        }
    }
}
