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
            AddDalItem(DataSource.customers, DataSource.IdalDoType.Customer);

        public List<IDAL.DO.Customer> DisplayAllCustomers() => DisplayAllItems(DataSource.customers);

        public List<IDAL.DO.Customer> DisplayCustomer(int choice) => DisplayOneItem(DataSource.customers, choice);

        public void ProvidePackageToCustomer(int choice)
        {

            if (choice < 0 || choice > DataSource.parcels.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < DataSource.drones.Count; i++)
            {
                if (DataSource.parcels[choice].DroneId == DataSource.drones[i].ID) DataSource.drones[i].Status = IDAL.DO.DroneStatuses.free;
            }
            DataSource.parcels[choice].Delivered = DateTime.Now;
        }
    }
}
