using System.Linq;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {
        ///<summary>
        ///Delete a customer
        ///</summary>
        ///<param name="ID">
        ///The ID of the customer to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///If there is a package in the system that needs to be delivered to the customer
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the customer does not exist in the system
        ///</exception>
        public void DeleteCustomer(int ID)
        {
            var customer = GetCustomerList().First(p => p.ID == ID);
            if (customer.numberExpectedPackages != 0)
                throw new OperationNotPossibleException("There are currently packaged in transit to the customer");
            if (customer.numberPackagesUndelivered != 0)
                throw new OperationNotPossibleException("There are currently packaged in transit from the customer");
            dal.DeleteCustomer(ID);
        }

        ///<summary>
        ///Delete a drone
        ///</summary>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///If the drone is in delivery status it cannot be deleted
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the drone does not exist in the system
        ///</exception>
        public void DeleteDrone(int ID)
        {
            if (GetDrone(ID).status == DroneStatuses.delivery)
                throw new OperationNotPossibleException("The drone is currently doing a delivery");
            drones.RemoveAll(d => d.ID == ID);
            dal.DeleteDrone(ID);
        }

        ///<summary>
        ///Delete a package
        ///</summary>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///The package can only be deleted if it has not be assigned yet, or if has already been delivered
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the package does not exist in the system
        ///</exception>
        public void DeletePackage(int ID)
        {
            var package = dal.GetPackage(ID);
            if (package.scheduled != null && package.delivered == null)
                throw new OperationNotPossibleException("The package has been assigned for delivery and has not yet been delivered");
            dal.DeletePackage(ID);

            
        }

        ///<summary>
        ///Delete a package
        ///</summary>
        ///<remarks>
        ///Be very careful when calling this. There may be drones that are counting on this station in the future and will be stranded if the station dissapears
        ///</remarks>
        ///<param name="ID">
        ///The ID of the drone to delete
        ///</param>
        ///<exception cref="OperationNotPossibleException">
        ///The station can only be deleted if there are no drones charging on it
        ///</exception>
        ///<exception cref="InvalidBlObjectException">
        ///If the package does not exist in the system
        ///</exception>
        public void DeleteStation(int ID)
        {
            if (dal.GetAllCharges()
                    .Where(s => s.StationId == ID)
                    .Any())
                throw new OperationNotPossibleException("The station currently has drones charging at it");
            dal.DeleteStation(ID);
        }
    }
}
