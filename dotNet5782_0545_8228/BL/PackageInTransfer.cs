using System;

namespace IBL
{
    namespace BO
    { 
        public class PackageInTransfer
        {
            public int ID { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Boolean deliveryStatus { get; set; }
            public CustomerInPackage sender { get; set; }
            public CustomerInPackage receiver { get; set; }
            public Location collectionLocation { get; set; }
            public Location deliveringLocation { get; set; }
            public double deliveryDistance { get; set; }

            public override string ToString()
            {
                return String.Format("ID = {0}, Weight Category = {1}, Priority = {2}, Delivery Status = {3}, Sender = {4}, Recevier = {5}, Collection Location = {6}, Delivery Location = {7}, Delivery Distance = {8}",
                    ID, weightCategory, priority, deliveryStatus.ToString(), sender.ToString(), receiver.ToString(), collectionLocation.ToString(), deliveringLocation.ToString(), deliveryDistance);
            }

            public PackageInTransfer(Package package)
            {
                this.ID = package.ID;
                this.weightCategory = package.weightCategory;
                this.priority = package.priority;
            }

            public PackageInTransfer(IDAL.DO.Package package)
            { 
            }

        }
    }
}
