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
            AddDalItem(DataSource.droneStations, IdalDoType.DroneStation);
        public void AddDroneStation(IDAL.DO.DroneStation droneStation)
        {
            List<IDAL.DO.DroneStation> list = DataSource.droneStations;
            if (list.Count + 1 > list.Capacity)
            {
                list.Add(droneStation);
            }
            else
            {
                throw new IDAL.DO.DataSourceException();
            }
        }
            

        public List<IDAL.DO.DroneStation> GetAllDroneStations() => DisplayAllItems(DataSource.droneStations);

        public List<IDAL.DO.DroneStation> GetAllUnoccupiedStations() =>
            DisplayAllItems(DataSource.droneStations, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        public IDAL.DO.DroneStation GetDroneStation(int ID) => DisplayOneItem(DataSource.droneStations, ID);

        public void SendDroneToCharge(int stationID, int droneID)
        {

            if (droneID < 0 ||
                droneID > DataSource.drones.Count ||
                stationID < 0 ||
                stationID > DataSource.droneStations.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            if (DataSource.droneStations[stationID].ChargeSlots > 0)
            {
                DataSource.droneCharges.Add(
                    new IDAL.DO.DroneCharge(DataSource.drones[droneID].ID,
                    DataSource.droneStations[stationID].ID));
                
                DataSource.droneStations[stationID].ChargeSlots--;
            }
        }

        public List<IDAL.DO.DroneCharge> GetAllCharges()
        {
            return DataSource.droneCharges;
        }
        

        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            if (droneID < 0 ||
               droneID > DataSource.drones.Count ||
               stationID < 0 ||
               stationID > DataSource.droneStations.Count)
            {
                throw new IDAL.DO.DalObjectAccessException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < DataSource.droneCharges.Count; i++)
            {
                if (DataSource.droneCharges[i].DroneId == DataSource.drones[droneID].ID &&
                         DataSource.droneCharges[i].StationId == DataSource.droneStations[stationID].ID)
                {
                    
                    DataSource.droneStations[stationID].ChargeSlots++;
                }
            }
        }
    }
}
