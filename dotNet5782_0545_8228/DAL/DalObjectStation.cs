using System.Collections.Generic;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddStation() =>
            AddDalItem(DataSource.stations, IdalDoType.STATION);

        public void AddStation(IDAL.DO.Station station) =>
            AddDalItem(DataSource.stations, station, IdalDoType.STATION);

        public List<IDAL.DO.Station> GetAllStations() => GetAllItems(DataSource.stations);

        public List<IDAL.DO.Station> GetAllUnoccupiedStations() =>
            GetAllItems(DataSource.stations, (IDAL.DO.Station ds) => ds.chargeSlots > 0);

        public IDAL.DO.Station GetStation(int ID) => GetOneItem(DataSource.stations, ID);

        public void SendDroneToCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Add(new IDAL.DO.DroneCharge(droneID, stationID));
            GetStation(stationID).chargeSlots--;
        }

        public List<IDAL.DO.DroneCharge> GetAllCharges()
        {
            return DataSource.droneCharges;
        }
        
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Remove(DataSource.droneCharges.Find((dc) => {return dc.DroneId == droneID;}));
            GetStation(stationID).chargeSlots++;
        }
    }
}
