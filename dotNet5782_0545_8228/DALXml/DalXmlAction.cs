
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
            packagesRoot = LoadXml("packages");
            var package = packagesRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("ID").Value) == packageID)
                .First();
            package.Element("scheduled").Value = DateTime.Now.ToString();
            package.Element("droneId").Value = droneID.ToString();
            SaveXml("packages");
        }

        public void CollectPackageToDrone(int packageID)
        {
            packagesRoot = LoadXml("packages");
            var package = packagesRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("ID").Value) == packageID)
                .First();
            package.Element("pickedUp").Value = DateTime.Now.ToString();
            SaveXml("packages");
        }

        public void ProvidePackageToCustomer(int packageID)
        {
            packagesRoot = LoadXml("packages");
            var package = packagesRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("ID").Value) == packageID)
                .First();
            package.Element("delivered").Value = DateTime.Now.ToString();
            SaveXml("packages");
        }

        public void ReleaseDroneFromCharge(int stationID, int droneID)
        {
            droneChargesRoot = LoadXml("droneCharges");
            droneChargesRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("DroneId").Value) == droneID)
                .First()
                .Remove();
            SaveXml("droneCharges");

            stationsRoot = LoadXml("stations");
            var station = stationsRoot
                .Elements()
                .Where(s => Int32.Parse(s.Element("ID").Value) == stationID)
                .First();
            var chargeSlots = station.Element("chargeSlots");
            chargeSlots.Value = (Int32.Parse(chargeSlots.Value) + 1).ToString();
            SaveXml("stations");
        }

        public void SendDroneToCharge(int stationID, int droneID)
        {
            droneChargesRoot = LoadXml("droneCharges");
            droneChargesRoot.Add(DroneChargeToXElement(new DroneCharge(droneID, stationID, DateTime.Now)));
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
