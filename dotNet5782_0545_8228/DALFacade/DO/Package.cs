using System;


namespace DO
{
    public class Package : DalEntity
    {
        public int senderId { get; set; }
        public int recieverId { get; set; }
        public int droneId { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public DateTime? requested { get; set; }
        public DateTime? scheduled { get; set; }
        public DateTime? pickedUp { get; set; }
        public DateTime? delivered { get; set; }

        public Package(
                int ID,
                int senderId,
                int recieverId,
                int droneId,
                WeightCategories weight,
                Priorities priority,
                DateTime? requested,
                DateTime? scheduled,
                DateTime? pickedUp,
                DateTime? delivered)
        : base(ID)
        {
            this.senderId = senderId;
            this.recieverId = recieverId;
            this.droneId = droneId;
            this.weight = weight;
            this.priority = priority;
            this.requested = requested;
            this.scheduled = scheduled;
            this.pickedUp = pickedUp;
            this.delivered = delivered;
        }


        public Package(int ID, int senderId, int recieverId, int droneId = 0) : base(ID)
        {
            Random rand = new();
            this.senderId = senderId;
            this.recieverId = recieverId;
            this.droneId = droneId;
            this.weight = (WeightCategories)rand.Next(Enum.GetNames(typeof(WeightCategories)).Length);
            this.priority = (Priorities)rand.Next(Enum.GetNames(typeof(Priorities)).Length);
            this.requested = null;
            this.scheduled = null;
            this.pickedUp = null;
            this.delivered = null;
        }

        public override Package Clone() => this.MemberwiseClone() as Package;

        public override string ToString()
        {
            return String.Format("Parcel(ID = {0}, SenderId = {1}, Weight = {2}, Priority = {3}, Requested = {4}, DroneId = {5}, Scheduled = {6}, PickedUp = {7}, Delivered = {8}",
                    ID, senderId, weight.ToString(), priority.ToString(), requested, droneId, scheduled, pickedUp, delivered);
        }

    }
}

