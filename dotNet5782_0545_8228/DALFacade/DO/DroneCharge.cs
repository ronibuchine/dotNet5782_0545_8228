﻿using System;


namespace DO
{
    public class DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }

        public DroneCharge(int DroneId, int StationId)
        {
            this.DroneId = DroneId;
            this.StationId = StationId;
        }

        public DroneCharge Clone() => this.MemberwiseClone() as DroneCharge;

        public override string ToString()
        {
            // do DateTimes need a toString()?
            return String.Format("Drone(DroneId = {0}, StationId = {1}", DroneId, StationId);
        }
    }
}
