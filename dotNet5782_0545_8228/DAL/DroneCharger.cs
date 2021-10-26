using System;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharger : DalStruct
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }

            public DroneCharger(int DroneId, int StationId)
            {
                this.DroneId = DroneId;
                this.StationId = StationId;
            }

            public override string ToString()
            {
                // do DateTimes need a toString()?
                return String.Format("Drone(DroneId = {0}, StationId = {1}", DroneId, StationId);
            }
        }
    }
}
