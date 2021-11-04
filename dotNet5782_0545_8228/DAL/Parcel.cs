using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel : DalStruct
        {
            public int ID { get; set; }
            public int SenderId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

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
                this.SenderId = SenderId;
                this.Weight = Weight;
                this.Priority = Priority;
                this.Requested = Requested;
                this.DroneId =  DroneId;
                this.Scheduled = Scheduled;
                this.PickedUp = PickedUp;
                this.Delivered = Delivered;
            }

            public Parcel(int i, Random rand)
            {
                this.ID = i;
                this.SenderId = i + 1000;
                this.Weight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length - 1);
                this.Priority = (IDAL.DO.Priorities) rand.Next(Enum.GetNames(typeof(IDAL.DO.Priorities)).Length - 1);
                this.Requested = DateTime.Today;
                this.DroneId = 0;
                this.Scheduled = DateTime.MinValue;
                this.PickedUp = DateTime.MinValue;
                this.Delivered = DateTime.MinValue;
            }

            public override string ToString()
            {
                // do DateTimes need a toString()?
                return String.Format("Drone(ID = {0}, SenderId = {1}, Weight = {2}, Priority = {3}, Requested = {4}, DroneId = {5}, Scheduled = {6}, PickedUp = {7}, Delivered = {8}",
                        ID, SenderId, Weight.ToString(), Priority.ToString(), Requested, DroneId, Scheduled, PickedUp, Delivered);
            }

        }
    }
}
