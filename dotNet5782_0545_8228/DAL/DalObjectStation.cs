using System.Collections.Generic;
using DO;
using DALAPI;
using System.Linq;

namespace DAL
{
    public partial class DalObject : IDAL
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
            return (IEnumerable<DroneCharge>)DataSource.droneCharges.Select(dc => dc.Clone());
        }
        
        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            var temp = DataSource.droneCharges.ToList();
            temp.Remove(temp.Find(dc => dc.DroneId == droneID));
            DataSource.droneCharges = temp;
            GetActualStation(stationID).chargeSlots++;
        }
    }
}
