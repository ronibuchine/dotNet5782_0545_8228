using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddDroneStation() =>
            AddDalItem(DataSource.droneStations, DataSource.IdalDoType.DroneStation);

        public List<IDAL.DO.DroneStation> DisplayAllDroneStations() => DisplayAllItems(DataSource.droneStations);

        public List<IDAL.DO.DroneStation> DisplayAllUnoccupiedStations() =>
            DisplayAllItems(DataSource.droneStations, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        public List<IDAL.DO.DroneStation> DisplayDroneStation(int choice) => DisplayOneItem(DataSource.droneStations, choice);

        public void SendDroneToCharge(int stationChoice, int droneChoice)
        {

            if (droneChoice < 0 ||
                droneChoice > DataSource.drones.Count ||
                stationChoice < 0 ||
                stationChoice > DataSource.droneStations.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            if (DataSource.drones[droneChoice].Battery != 100 && DataSource.droneStations[stationChoice].ChargeSlots > 0)
            {
                DataSource.droneCharges.Add(new IDAL.DO.DroneCharge(DataSource.drones[droneChoice].ID, DataSource.droneStations[stationChoice].ID));
                DataSource.drones[droneChoice].Status = IDAL.DO.DroneStatuses.maintenance;
                DataSource.droneStations[stationChoice].ChargeSlots--;
            }
        }

        public void ReleaseDroneFromCharge(int stationChoice, int droneChoice)
        {
            if (droneChoice < 0 ||
               droneChoice > DataSource.drones.Count ||
               stationChoice < 0 ||
               stationChoice > DataSource.droneStations.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < DataSource.droneCharges.Count; i++)
            {
                if (DataSource.droneCharges[i].DroneId == DataSource.drones[droneChoice].ID &&
                         DataSource.droneCharges[i].StationId == DataSource.droneStations[stationChoice].ID)
                {
                    DataSource.drones[droneChoice].Status = IDAL.DO.DroneStatuses.free;
                    DataSource.droneStations[stationChoice].ChargeSlots++;
                }
            }
        }
    }
}
