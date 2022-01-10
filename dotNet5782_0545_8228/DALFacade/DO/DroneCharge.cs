using System;


namespace DO
{
    public class DroneCharge
    {
        public int droneId { get; set; }
        public int stationId { get; set; }
        public DateTime beganCharge { get; set; }

        public DroneCharge(int droneId, int stationId, DateTime beganCharge)
        {
            this.droneId = droneId;
            this.stationId = stationId;
            this.beganCharge = beganCharge;
        }

        public DroneCharge() : base() {}

        public DroneCharge Clone() => this.MemberwiseClone() as DroneCharge;

        public override string ToString()
        {
            // do DateTimes need a toString()?
            return String.Format("Drone(DroneId = {0}, StationId = {1}, Began Charging At: {2}", droneId, stationId, beganCharge);
        }
    }
}

