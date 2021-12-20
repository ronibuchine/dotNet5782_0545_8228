using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IdalInterface
    {

        public void AddStation() =>
            AddDalItem(DataSource.stations, IdalDoType.STATION);

        public void AddStation(Station station) =>
            AddDalItem(DataSource.stations, station, IdalDoType.STATION);

        public IEnumerable<Station> GetAllStations() => GetAllItems(DataSource.stations);

        public IEnumerable<Station> GetAllUnoccupiedStations() =>
            GetAllItems(DataSource.stations, (Station ds) => ds.chargeSlots > 0);

        public Station GetStation(int ID) => GetOneItem(DataSource.stations, ID);

        public Station GetActualStation(int ID) => GetActualOneItem(DataSource.stations, ID);

        public void SendDroneToCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Add(new DroneCharge(droneID, stationID));
            GetActualStation(stationID).chargeSlots--;
        }

        public IEnumerable<DroneCharge> GetAllCharges()
        {
            IEnumerable<DroneCharge> newList = new();
            DataSource.droneCharges.ForEach(dc => newList.Add(dc.Clone()));
            return newList;
        }
        
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            DataSource.droneCharges.Remove(DataSource.droneCharges.Find((dc) => {return dc.DroneId == droneID;}));
            GetActualStation(stationID).chargeSlots++;
        }
    }
}
