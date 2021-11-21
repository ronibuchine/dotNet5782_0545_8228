using System;

namespace IDAL
{
    namespace DO
    {
        public class Parcel : IDAL.DO.DalEntity
        {
            public int senderId { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public DateTime requested { get; set; }
            public int droneId { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            public Parcel(
                    int ID,
                    int SenderId,
                    WeightCategories Weight,
                    Priorities Priority,
                    DateTime Requested,
                    int DroneId,
                    DateTime Scheduled,
                    DateTime PickedUp,
                    DateTime Delivered)
            {
                this.ID = ID;
                this.senderId = SenderId;
                this.weight = Weight;
                this.priority = Priority;
                this.requested = Requested;
                this.droneId =  DroneId;
                this.scheduled = Scheduled;
                this.pickedUp = PickedUp;
                this.delivered = Delivered;
            }

            public Parcel(int i, Random rand)
            {
                this.ID = i;
                this.senderId = i + 1000;
                this.weight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length - 1);
                this.priority = (IDAL.DO.Priorities) rand.Next(Enum.GetNames(typeof(IDAL.DO.Priorities)).Length - 1);
                this.requested = DateTime.Now;
                this.droneId = 0;
                this.scheduled = DateTime.MinValue;
                this.pickedUp = DateTime.MinValue;
                this.delivered = DateTime.MinValue;
            }

            public override string ToString()
            {
                // do DateTimes need a toString()?
                return String.Format("Parcel(ID = {0}, SenderId = {1}, Weight = {2}, Priority = {3}, Requested = {4}, DroneId = {5}, Scheduled = {6}, PickedUp = {7}, Delivered = {8}",
                        ID, senderId, weight.ToString(), priority.ToString(), requested, droneId, scheduled, pickedUp, delivered);
            }

        }
    }
}
