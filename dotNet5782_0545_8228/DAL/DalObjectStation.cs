using System.Collections.Generic;
using IDAL.DO;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {

        public void AddStation() =>
            AddDalItem(DataSource.stations, IdalDoType.STATION);

        public void AddStation(IDAL.DO.Station station) =>
            AddDalItem(DataSource.stations, station, IdalDoType.STATION);

        public List<Station> GetAllStations() => GetAllItems(DataSource.stations);

        public List<Station> GetAllUnoccupiedStations() =>
            GetAllItems(DataSource.stations, (IDAL.DO.Station ds) => ds.chargeSlots > 0);

        public Station GetStation(int ID) => GetOneItem(DataSource.stations, ID);

        private Station _GetStation(int ID) => _GetOneItem(DataSource.stations, ID);

        public void SendDroneToCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Add(new IDAL.DO.DroneCharge(droneID, stationID));
            _GetStation(stationID).chargeSlots--;
        }

        public List<DroneCharge> GetAllCharges()
        {
            List<DroneCharge> newList = new();
            DataSource.droneCharges.ForEach(dc => newList.Add(dc.Clone()));
            return newList;
        }
        
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Remove(DataSource.droneCharges.Find((dc) => {return dc.DroneId == droneID;}));
            _GetStation(stationID).chargeSlots++;
        }
    }
}
