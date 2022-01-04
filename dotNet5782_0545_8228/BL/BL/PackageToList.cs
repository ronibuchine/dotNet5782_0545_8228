using System;


namespace BL
{
    /// <summary>
    /// A package entity whcih is used for list represntation
    /// </summary>
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
            if (package.delivered != null)
                this.status = PackageStatuses.delivered;
            else if (package.pickedUp != null)
                this.status = PackageStatuses.pickedUp;
            else if (package.scheduled != null)
                this.status = PackageStatuses.scheduled;
            else
                this.status = PackageStatuses.requested;
        }

        public override string ToString()
        {
            return $"ID = {ID}, Sender = {senderName}, Recevier = {receiverName}, WeightCategory = {weightCategory}, Priority = {priority}, Package Status = {status}";
        }
    }
}
   

