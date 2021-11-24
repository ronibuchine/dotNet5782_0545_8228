using System;

namespace IDAL
{
    namespace DO
    {
        public class Package : IDAL.DO.DalEntity
        {
            public int senderId { get; set; }
            public int recieverId { get; set; }
            public WeightCategories weight { get; set; }
            public Priorities priority { get; set; }
            public int? droneId { get; set; }
            public DateTime requested { get; set; }
            public DateTime? scheduled { get; set; }
            public DateTime? pickedUp { get; set; }
            public DateTime? delivered { get; set; }

            public Package(
                    int senderId,
                    int recieverId,
                    WeightCategories weight,
                    Priorities priority,
                    int droneId,
                    DateTime requested,
                    DateTime scheduled,
                    DateTime pickedUp,
                    DateTime delivered)
            {
                this.senderId = senderId;
                this.recieverId = recieverId;
                this.weight = weight;
                this.priority = priority;
                this.requested = requested;
                this.droneId =  droneId;
                this.scheduled = scheduled;
                this.pickedUp = pickedUp;
                this.delivered = delivered;
            }

            /* public Package(int i, Random rand) */
            /* { */
            /*     this.senderId = i; */
            /*     this.recieverId = i; */
            /*     this.weight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length); */
            /*     this.priority = (IDAL.DO.Priorities) rand.Next(Enum.GetNames(typeof(IDAL.DO.Priorities)).Length); */
            /*     this.requested = DateTime.Now; */
            /*     this.droneId = 0; */
            /*     /1* this.scheduled = DateTime.MinValue; *1/ */
            /*     this.pickedUp = DateTime.MinValue; */
            /*     this.delivered = DateTime.MinValue; */
            /* } */

            public Package(int senderId, int recieverId, int? droneId = null)
            {
                Random rand = new();
                this.senderId = senderId;
                this.recieverId = recieverId;
                this.weight = (IDAL.DO.WeightCategories) rand.Next(Enum.GetNames(typeof(IDAL.DO.WeightCategories)).Length);
                this.priority = (IDAL.DO.Priorities) rand.Next(Enum.GetNames(typeof(IDAL.DO.Priorities)).Length);
                this.requested = DateTime.Now;
                this.droneId = droneId;
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
}
