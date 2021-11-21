using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddCustomer() =>
            AddDalItem(DataSource.customers, IdalDoType.Customer);

        public void AddCustomer(IDAL.DO.Customer customer)
        {
            List<IDAL.DO.Customer> list = DataSource.customers;
            if (list.Count + 1 > list.Capacity)
            {
                list.Add(customer);
            }
            else
            {
                throw new IDAL.DO.DataSourceException();
            }
        }
            

        public List<IDAL.DO.Customer> GetAllCustomers() => DisplayAllItems(DataSource.customers);

        public IDAL.DO.Customer GetCustomer(int ID) => DisplayOneItem(DataSource.customers, ID);

        public void ProvidePackageToCustomer(int packageID)
        {

            if (packageID < 0 || packageID > DataSource.parcels.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            
            DataSource.parcels[packageID].delivered = DateTime.Now;
        }
    }
}
