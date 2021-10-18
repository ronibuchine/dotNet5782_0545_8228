using System;

namespace IDAL
{
    namespace DO
    {
        struct Parcel
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

            public override string ToString()
            {
                // do DateTimes need a toString()?
                return String.Format("Drone(ID = {0}, SenderId = {1}, Weight = {2}, Priority = {3}, Requested = {4}, DroneId = {5}, Scheduled = {6}, PickedUp = {7}, Delivered = {8}",
                        ID, SenderId, Weight.ToString(), Priority.ToString(), Requested, DroneId, Scheduled, PickedUp, Delivered);
            }

        }
    }
}
