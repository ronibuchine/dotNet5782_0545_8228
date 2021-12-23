using System;


namespace BL
{
    /// <summary>
    ///  a Package entity representing a package in our delivery system
    /// </summary>
    public class Package : BLEntity
    {
        public CustomerInPackage sender { get; set; }
        public CustomerInPackage receiver { get; set; } 
        public WeightCategories weightCategory { get; set; }
        public Priorities priority { get; set; }
        public DroneInDelivery drone { get; set; } 
        public DateTime? requested { get; set; }
        public DateTime? scheduled { get; set; }
        public DateTime? pickedUp { get; set; }
        public DateTime? delivered { get; set; }

        public Package(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();

            this.sender = new CustomerInPackage(dal.GetCustomer(senderID));
            this.receiver = new CustomerInPackage(dal.GetCustomer(receiverID));
            this.weightCategory = weight;
            this.priority = priority;
        }

        public Package(DO.Package package) : base(null)
        {
            ID = package.ID;

            DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();
            sender = new CustomerInPackage(dal.GetCustomer(package.senderId));
            receiver = new CustomerInPackage(dal.GetCustomer(package.recieverId));
            weightCategory = (WeightCategories)package.weight;
            priority = (Priorities)package.priority;
            drone = package.droneId != 0 ? new DroneInDelivery(dal.GetDrone(package.droneId)) : null;
            requested = package.requested;
            scheduled = package.scheduled;
            pickedUp = package.pickedUp;
            delivered = package.delivered;
        }


        public override string ToString()
        {
            return String.Format("ID = {0}, Sender = {1}, Receiver = {2}, Weight Category = {3}, Priority = {4}, Drone = {5}, Creation Time = {6}, Assignment Time = {7}, Collection Time = {8}, Delivering Time = {9}",
                ID, sender.ToString(), receiver.ToString(), weightCategory, priority, drone.ToString(), requested.ToString(), scheduled.ToString(), pickedUp.ToString(), delivered.ToString());
        }
    }
}

