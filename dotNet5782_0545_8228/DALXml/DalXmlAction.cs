using System;
using DO;
using DALAPI;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALXml : IDAL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool VerifyCustomerCredentials(int ID, string password)
        {
            customersRoot = LoadXml("customers");
            return customersRoot
                .Elements()
                .First(c => Int32.Parse(c.Element("ID").Value) == ID && c.Element("password").Value == password) != null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            customersRoot = LoadXml("employees");
            return employeesRoot
                .Elements()
                .First(c => Int32.Parse(c.Element("ID").Value) == ID && c.Element("password").Value == password) != null;
        }
    }
}
