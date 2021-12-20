using System;

namespace IBL
{
    namespace BO
    {
        public class PackageAtCustomer
        {
            public int ID { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public PackageStatuses status { get; set; }
            public CustomerInPackage senderReceiver { get; set; } 

            public PackageAtCustomer(DO.Package package)
            {
                ID = package.ID;
                weight = (WeightCategories)package.weight;
                priority = (Priorities)package.priority;
                if (package.delivered != null) // has been delivered
                    status = PackageStatuses.delivered;
                else if (package.pickedUp != null) // has been picked up
                    status = PackageStatuses.pickedUp;
                else if (package.scheduled != null) // has been scheduled
                    status = PackageStatuses.scheduled;
                else // has been assigned
                    status = PackageStatuses.requested;
                // TODO assign senderReceiver
            }

            public PackageAtCustomer(Package package)
            {
                ID = package.ID;
                weight = package.weightCategory;
                priority = package.priority;
                if (package.delivered != null) // has been delivered
                    status = PackageStatuses.delivered;
                else if (package.pickedUp != null) // has been picked up
                    status = PackageStatuses.pickedUp;
                else if (package.scheduled != null) // has been scheduled
                    status = PackageStatuses.scheduled;
                else // has been created
                    status = PackageStatuses.requested;
                // TODO assign senderReceiver
            }

            public override string ToString()
            {
                return String.Format("ID = {0}, Weight = {1}, Priority = {2}, Package Status = {3}, Other Party = {4}",
                    ID, weight, priority, status, senderReceiver.ToString());
            }
        }
    }
    
}
