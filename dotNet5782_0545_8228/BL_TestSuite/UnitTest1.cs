using System;
using IBL.BO;
using Xunit;

namespace BL_TestSuite
{
    public class UnitTest1
    {
        // example
        [Theory]
        [InlineData("model1", IBL.BO.WeightCategories.heavy, 1)]
        public void AddDroneTest(string model, WeightCategories maxWeight, int stationID)
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject();
            /* Assert.Throws(typeof(IBL.BO.InvalidBlObjectException), () => { bl.AddDrone(ID, model, maxWeight, stationID); }); */
            Drone drone = bl.AddDrone(model, maxWeight, stationID);
            IBL.BO.Drone d = bl.GetDrone(drone.ID);
            Assert.True(d.battery == drone.battery, "Drone ID is incorrect");
        }

        [Theory]
        public void AddBaseStationTest(string name, Location location, int availableChargers) { }

        [Theory]
        public void AddCustomerTest(string name, string phone, Location location) { }

        [Theory]
        public void AddPackageTest(int senderID, int receiverID, WeightCategories weight, Priorities priority) { }

        [Theory]
        public void UpdateDroneTest(int ID, string newModel) { }

        [Theory]
        public void UpdateStationTest(int stationID, string stationName) { }

        [Theory]
        public void UpdateStationTest(int stationID, int numChargers) { }

        [Theory]
        public void UpdateStationTest(int stationID, string stationName, int numChargers) { }

        [Theory]
        public void UpdateCustomerNameTest(int ID, string name) { }

        [Theory]
        public void UpdateCustomerPhoneTest(int ID, String phone) { }

        [Theory]
        public void UpdateCustomerTest(int ID, string name, String phone) { }

        [Theory]
        public void SendDroneToChargeTest(int droneID) { }

        [Theory]
        public void ReleaseDroneFromChargeTest(int droneID, DateTime chargeTime) { }

        [Theory]
        public void AssignPackageToDroneTest(int droneID) { }

        [Theory]
        public void CollectPackageTest(int droneID) { }

        [Theory]
        public void DeliverPackageTest(int droneID) { }

        [Theory]
        public void GetBaseStationTest(int ID) { }

        [Theory]
        public void GetDroneTest(int ID) { }

        [Theory]
        public void GetCustomerTest(int ID) { }

        [Theory]
        public void GetPackageTest(int ID) { }

        [Theory]
        public void GetStationList() { }

        [Theory]
        public void GetDroneList() { }

        [Theory]
        public void GetCustomerList() { }

        [Theory]
        public void GetPackageList() { }

        [Theory]
        public void GetUnassignedPackages() { }

        [Theory]
        public void GetAvailableStations() { }

    }
}
