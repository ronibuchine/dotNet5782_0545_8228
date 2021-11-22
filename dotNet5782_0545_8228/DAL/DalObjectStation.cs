using System.Collections.Generic;

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
            DataSource.droneCharges.Add(new IDAL.DO.DroneCharge(droneID, stationID));
            GetDroneStation(stationID).ChargeSlots--;
        }

        public List<IDAL.DO.DroneCharge> GetAllCharges()
        {
            return DataSource.droneCharges;
        }
        
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Remove(DataSource.droneCharges.Find((dc) => {return dc.DroneId == droneID;}));
            GetDroneStation(stationID).ChargeSlots++;
        }
    }
}
