using System;
using DalObjectNamespace;

namespace IBL
{
    namespace BO
    {
        public class Package : BLEntity
        {
            public CustomerInPackage sender { get; set; }
            public CustomerInPackage receiver { get; set; } // TODO set this somewhere
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Drone drone { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            public Package(int senderID, int receiverID, WeightCategories weight, Priorities priority)
            {
                this.sender = new CustomerInPackage(DalObject.GetInstance().GetCustomer(senderID));
                this.receiver = new CustomerInPackage(DalObject.GetInstance().GetCustomer(receiverID));
                this.weightCategory = weight;
                this.priority = priority;
            }

            public Package(IDAL.DO.Package package) : base(null)
            {
                ID = package.ID;
                sender = new CustomerInPackage(DalObject.GetInstance().GetCustomer(package.senderId));
                receiver = new CustomerInPackage(DalObject.GetInstance().GetCustomer(package.recieverId));
                weightCategory = (IBL.BO.WeightCategories)package.weight;
                priority = (IBL.BO.Priorities)package.priority;
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
}
