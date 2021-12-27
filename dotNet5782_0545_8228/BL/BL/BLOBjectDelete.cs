using System.Linq;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public void DeleteCustomer(int ID)
        {
            var customer = GetCustomerList().First(p => p.ID == ID);
            if (customer.numberExpectedPackages != 0)
                throw new OperationNotPossibleException("There are currently packaged in transit to the customer");
            if (customer.numberPackagesUndelivered != 0)
                throw new OperationNotPossibleException("There are currently packaged in transit from the customer");
            dal.DeleteCustomer(ID);
        }

        public void DeleteDrone(int ID)
        {
            if (GetDrone(ID).status == DroneStatuses.delivery)
                throw new OperationNotPossibleException("The drone is currently doing a delivery");
            drones.RemoveAll(d => d.ID == ID);
            dal.DeleteDrone(ID);
        }

        public void DeletePackage(int ID)
        {
            var package = dal.GetPackage(ID);
            if (package.scheduled != null && package.delivered == null)
                throw new OperationNotPossibleException("The package has been assigned for delivery and has not yet been delivered");
            dal.DeletePackage(ID);

            
        }

        public void DeleteStation(int ID)
        {
            if (dal.GetAllCharges()
                    .Where(s => s.StationId == ID)
                    .Any())
                throw new OperationNotPossibleException("The station currently has drones charging at it");
            dal.DeleteStation(ID);
        }

        public void DeleteEmployee(int ID)
        {
            dal.DeleteEmployee(ID);
        }
    }
}
