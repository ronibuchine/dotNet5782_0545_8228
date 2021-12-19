using System;
using UTIL;

namespace IBL
{
    namespace BO
    { 
        public class PackageInTransfer
        {
            public int ID { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Boolean delivered { get; set; }
            public CustomerInPackage sender { get; set; }
            public CustomerInPackage receiver { get; set; }
            public Location collectionLocation { get; set; }
            public Location deliveringLocation { get; set; }
            public double deliveryDistance { get; set; }

            public override string ToString()
            {
                return String.Format("ID = {0}, Weight Category = {1}, Priority = {2}, Delivery Status = {3}, Sender = {4}, Recevier = {5}, Collection Location = {6}, Delivery Location = {7}, Delivery Distance = {8}",
                    ID, 
                    weightCategory, 
                    priority, 
                    delivered, 
                    PrintDebug.ToStringOrNull(sender),
                    PrintDebug.ToStringOrNull(receiver),
                    PrintDebug.ToStringOrNull(collectionLocation),
                    PrintDebug.ToStringOrNull(deliveringLocation),
                    PrintDebug.ToStringOrNull(deliveringLocation),
                    deliveryDistance);
            }

            public PackageInTransfer(Package package)
            {
                this.ID = package.ID;
                this.weightCategory = package.weightCategory;
                this.priority = package.priority;
                IDAL.DO.Customer dalSender = DalObjectNamespace.DalObject.GetInstance().GetCustomer(package.sender.ID);
                IDAL.DO.Customer dalReciever = DalObjectNamespace.DalObject.GetInstance().GetCustomer(package.receiver.ID);
                sender = new(dalSender);
                receiver = new(dalReciever);
                collectionLocation = new Location(dalSender.longitude, dalSender.latitude);
                deliveringLocation = new Location(dalReciever.longitude, dalReciever.latitude);
                deliveryDistance = UTIL.Distances.GetDistance(collectionLocation, deliveringLocation);
            }

            public PackageInTransfer(IDAL.DO.Package package)
            { 
                ID = package.ID;
                weightCategory = (WeightCategories)package.weight;
                priority = (Priorities)package.priority;
                /* delivered????? */
                IDAL.DO.Customer dalSender = DalObjectNamespace.DalObject.GetInstance().GetCustomer(package.senderId);
                IDAL.DO.Customer dalReciever = DalObjectNamespace.DalObject.GetInstance().GetCustomer(package.recieverId);
                sender = new(dalSender);
                receiver = new(dalReciever);
                collectionLocation = new Location(dalSender.longitude, dalSender.latitude);
                deliveringLocation = new Location(dalReciever.longitude, dalReciever.latitude);
                deliveryDistance = UTIL.Distances.GetDistance(collectionLocation, deliveringLocation);
            }

        }
    }
}
