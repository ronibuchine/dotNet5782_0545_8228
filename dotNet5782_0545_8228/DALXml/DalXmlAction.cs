
using System;
using DO;
using DALAPI;
using System.Linq;

namespace DAL
{
    public partial class DALXml : IDAL
    {
        public void AssignPackageToDrone(int packageID, int droneID)
        {
            throw new NotImplementedException();
        }

        public void CollectPackageToDrone(int packageID)
        {
            throw new NotImplementedException();
        }

        public void ProvidePackageToCustomer(int packageID)
        {
            throw new NotImplementedException();
        }

        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            throw new NotImplementedException();
        }

        public void SendDroneToCharge(int stationID, int droneID)
        {
            droneChargesRoot = LoadXml("droneCharges");
            droneChargesRoot.Add(DroneChargeToXElement(new DroneCharge(stationID, droneID, DateTime.Now)));
            stationsRoot = LoadXml("stations");
            var station = stationsRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("ID").Value) == stationID)
                .First();
            var chargeSlots = station.Element("chargeSlots");
            chargeSlots.Value = (Int32.Parse(chargeSlots.Value) - 1).ToString();
            SaveXml("droneCharges");
            SaveXml("stations");
        }


    }
}
