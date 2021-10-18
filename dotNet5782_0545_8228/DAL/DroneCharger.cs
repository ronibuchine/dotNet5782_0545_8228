using System;

namespace IDAL
{
    namespace DO
    {
        struct DroneCharger
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }

            public override string ToString()
            {
                // do DateTimes need a toString()?
                return String.Format("Drone(DroneId = {0}, StationId = {1}", DroneId, StationId);
            }
        }
    }
}
