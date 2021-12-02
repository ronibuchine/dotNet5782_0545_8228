using System;
using System.Collections.Generic;
using IBL.BO;
using Xunit;

namespace BL_TestSuite
{
    public class BL_Tests
    {

        private T GetFirstIDFromList<T>(List<T> entityList) => entityList[0];       


        
        [Theory]
        [InlineData("model1", IBL.BO.WeightCategories.heavy)]
        public void AddGetDroneTest(string model, WeightCategories maxWeight)
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject();
            int stationId = GetFirstIDFromList(bl.GetStationList()).ID;
            Drone drone = bl.AddDrone(model, maxWeight, stationId);
            Drone d = bl.GetDrone(drone.ID);
            Assert.True(d.battery == drone.battery && d.ID == drone.ID && d.currentLocation == drone.currentLocation, "Drone ID is incorrect");

            
        }

        [Fact]        
        public void AddGetStationTest() 
        {
            string name = "name1";
            Location location = new Location(1, 1);
            int availableChargers = 5;

            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject();
            Station station = bl.AddStation(name, location, availableChargers);
           

            Station s = bl.GetStation(station.ID);
            Assert.True((s.chargeSlots == station.chargeSlots &&
                s.ID == station.ID &&
                s.name == station.name &&
                s.location.Equals(station.location)), "AddStation assertion failed");
        }

        [Fact]
        public void AddGetCustomerTest() 
        {
            string name = "testname";
            string phone = "0586693748";
            Location location = new Location(1, 1);

            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject();
            Customer customer = bl.AddCustomer(name, phone, location);


            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.phone == customer.phone &&
                c.ID == customer.ID &&
                c.name == customer.name &&
                c.currentLocation.Equals(customer.currentLocation), "AddCustomer assertion failed");
        }

        [Theory]
        [InlineData(1, 2, IBL.BO.WeightCategories.heavy, IBL.BO.Priorities.emergency)]
        public void AddGetPackageTest(int senderID, int receiverID, WeightCategories weight, Priorities priority)
        {
            Assert.True(false, "Test not yet implemented");
        }

        [Theory]
        [InlineData(1, "newmodel")]
        public void UpdateDroneTest(int ID, string newModel) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, "testname")]
        public void UpdateStationNameTest(int stationID, string stationName) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, 5)]
        public void UpdateStationChargersTest(int stationID, int numChargers) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, "testname", 5)]
        public void UpdateStationNameChargersTest(int stationID, string stationName, int numChargers) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, "testname")]
        public void UpdateCustomerNameTest(int ID, string name) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, "0586693748")]
        public void UpdateCustomerPhoneTest(int ID, String phone) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1, "testname", "0586693748")]
        public void UpdateCustomerTest(int ID, string name, String phone) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1)]
        public void SendDroneToChargeTest(int droneID) { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void ReleaseDroneFromChargeTest()
        {
            int droneID = 1;
            DateTime chargeTime = DateTime.Now;
            Assert.True(false, "Test not yet implemented");
        }

        [Theory]
        [InlineData(1)]
        public void AssignPackageToDroneTest(int droneID) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1)]
        public void CollectPackageTest(int droneID) { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData(1)]
        public void DeliverPackageTest(int droneID) { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetStationList() { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetDroneList() { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetCustomerList() { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetPackageList() { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetUnassignedPackages() { Assert.True(false, "Test not yet implemented"); }

        [Fact]
        public void GetAvailableStations() { Assert.True(false, "Test not yet implemented"); }

    }
}
