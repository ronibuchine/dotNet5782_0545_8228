using System;
using UTIL;


namespace BL
{ 
    /// <summary>
    /// A package in our system which is currently in transfer to a customer
    /// </summary>
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

            DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();
            DO.Customer dalSender = dal.GetCustomer(package.sender.ID);
            DO.Customer dalReciever = dal.GetCustomer(package.receiver.ID);

            sender = new(dalSender);
            receiver = new(dalReciever);
            collectionLocation = new Location(dalSender.longitude, dalSender.latitude);
            deliveringLocation = new Location(dalReciever.longitude, dalReciever.latitude);
            deliveryDistance = UTIL.Distances.GetDistance(collectionLocation, deliveringLocation);
            if (package.delivered != null)
                this.delivered = true;
        }

        public PackageInTransfer(DO.Package package)
        { 
            ID = package.ID;
            weightCategory = (WeightCategories)package.weight;
            priority = (Priorities)package.priority;

            DALAPI.IDAL dal = DALAPI.DalFactory.GetDal();
            DO.Customer dalSender = dal.GetCustomer(package.senderId);
            DO.Customer dalReciever = dal.GetCustomer(package.recieverId);

            sender = new(dalSender);
            receiver = new(dalReciever);
            collectionLocation = new Location(dalSender.longitude, dalSender.latitude);
            deliveringLocation = new Location(dalReciever.longitude, dalReciever.latitude);
            deliveryDistance = UTIL.Distances.GetDistance(collectionLocation, deliveringLocation);
            if (package.delivered != null)
                this.delivered = true;
        }

    }
}

