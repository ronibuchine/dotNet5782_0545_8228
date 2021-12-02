﻿using System;

namespace IBL
{
    namespace BO
    {
        public class StationToList
        {
            public StationToList(Station droneStation)
            {
                ID = droneStation.ID;
                name = droneStation.name;
                availableChargeSlots = droneStation.chargeSlots;
                occupiedSlots = droneStation.chargingDrones != null ? droneStation.chargingDrones.Count : 0;
            }
            public int ID { get; set; }
            public string name { get; set; }
            public int availableChargeSlots { get; set; }
            public int occupiedSlots { get; set; }
            public override string ToString()
            {
                return String.Format("ID = {0}, Name = {1}, Current Available Charge Slots = {2}, Current Occupied Charge Slots = {3}",
                    ID, name, availableChargeSlots, occupiedSlots);
            }
        }
    }

}