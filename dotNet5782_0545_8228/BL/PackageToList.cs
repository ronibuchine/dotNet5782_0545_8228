using System;

namespace IBL
{
    namespace BO
    {
        public class PackageToList
        {
            public int ID { get; set; }
            public string senderName { get; set; }
            public string receiverName { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public PackageStatuses status { get; set; }

            public PackageToList(Package package)
            {
                this.ID = package.ID;
                this.senderName = package.sender.name;
                this.receiverName = package.receiver.name;
                this.weightCategory = package.weightCategory;
                this.priority = package.priority;
                if (DateTime.Compare(package.delivered, DateTime.Now) > 0)
                    this.status = PackageStatuses.delivered;
                if (DateTime.Compare(package.pickedUp, DateTime.Now) > 0)
                    this.status = PackageStatuses.collected;
                if (DateTime.Compare(package.scheduled, DateTime.Now) > 0)
                    this.status = PackageStatuses.assigned;
                else
                    this.status = PackageStatuses.created;
            }

            public override string ToString()
            {
                return $"ID = {ID}, Sender = {senderName}, Recevier = {receiverName}, WeightCategory = {weightCategory}, Priority = {priority}, Package Status = {status}";
            }
        }
    }
   
}
