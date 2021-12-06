using System;
using System.Collections.Generic;
using IBL.BO;
using Xunit;

namespace BL_TestSuite
{
    public class BL_Tests
    {

               
        [Theory]
        [InlineData("model1", WeightCategories.heavy)]
        public void AddGetDroneTest( string model, WeightCategories maxWeight)
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;           
            Drone drone = bl.AddDrone(model, maxWeight, stationID);
            Drone d = bl.GetDrone(drone.ID);
            Assert.True(d.battery == drone.battery && d.ID == drone.ID && d.currentLocation == drone.currentLocation, "Assertion for AddDrone failed"); 
            
            for (int i = 0; i < 4; i++)
            {
                bl.AddDrone($"{model}_{i+1}", maxWeight, stationID);
            }
            Assert.Throws<InvalidBlObjectException>(() =>
                    {
                        bl.AddDrone($"{model}_6", maxWeight, stationID);
                    }
                );
        }

        [Fact]        
        public void AddGetStationTest() 
        {
            string name = "name1";
            Location location = new Location(1, 1);
            int availableChargers = 5;

            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station station = bl.AddStation(name, location, availableChargers);
           

            Station s = bl.GetStation(station.ID);
            Assert.True((s.chargeSlots == station.chargeSlots &&
                s.ID == station.ID &&
                s.name == station.name &&
                s.location.Equals(station.location)), "AddStation assertion failed");

            for (int i = 0; i < 4; i++)
            {
                bl.AddStation($"{name}_{i + 1}", new Location(i+1, i+1), availableChargers);
            }
            Assert.Throws<InvalidBlObjectException>(() =>
                    {
                        bl.AddStation($"{name}_6", new Location(10, 10), availableChargers);
                    }
                );
        }

        [Fact]
        public void AddGetCustomerTest() 
        {
            string name = "testname";
            string phone = "0586693748";
            Location location = new Location(1, 1);

            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Customer customer = bl.AddCustomer(name, phone, location);


            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.phone == customer.phone &&
                c.ID == customer.ID &&
                c.name == customer.name &&
                c.currentLocation.Equals(customer.currentLocation), "AddCustomer assertion failed");

            for (int i = 0; i < 4; i++)
            {
                bl.AddCustomer($"{name}_{i + 1}", phone, new Location(i + 1, i + 1));
            }
            Assert.Throws<InvalidBlObjectException>(() =>
                    {
                        bl.AddCustomer($"{name}_6", phone, new Location(10, 10));
                    }
                );
        }

        [Theory]
        [InlineData(IBL.BO.WeightCategories.heavy, IBL.BO.Priorities.emergency)]
        public void AddGetPackageTest(WeightCategories weight, Priorities priority)
        {            
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            int senderID = bl.AddCustomer("name1", "123", new Location(1, 1)).ID;
            int receiverID = bl.AddCustomer("name2", "124", new Location(2, 2)).ID;

            Package package = bl.AddPackage(senderID, receiverID, weight, priority);
            Package p = bl.GetPackage(package.ID);
            Assert.True(package.ID == p.ID &&
                package.priority == p.priority &&
                package.weightCategory == p.weightCategory, "Assertion for AddGetPackage failed");

            for (int i = 0; i < 4; i++)
            {
                bl.AddPackage(senderID, receiverID, weight, priority);
            }
            Assert.Throws<InvalidBlObjectException>(() =>
                    {
                        bl.AddPackage(senderID, receiverID, weight, priority);
                    }
                );

        }

        [Fact]
        public void UpdateDroneTest() 
        {
            // add -> update -> get -> check
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1,1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s.ID);
            bl.UpdateDrone(d.ID, "newModel");
            d = bl.GetDrone(d.ID);
            Assert.True(d.model == "newModel", "drone model not updated"); 
        }

        [Fact]
        public void UpdateStationNameTest() 
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1,1), 5);
            bl.UpdateStation(s.ID, "newName");
            s = bl.GetStation(s.ID);
            Assert.True(s.name == "newName", "station model not updated"); 
        }

        [Fact]
        public void UpdateStationChargersTest() 
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1,1), 2);
            Drone d = bl.AddDrone("droneModel", WeightCategories.heavy, s.ID);
            s = bl.GetStation(s.ID);
            /* bl.SendDroneToCharge(d.ID); */
            Assert.Throws(typeof(InvalidBlObjectException), () => bl.UpdateStation(s.ID, 0));
            bl.UpdateStation(s.ID, 3);
            s = bl.GetStation(s.ID);
            Assert.True(s.chargeSlots == 3, "station model not updated"); 
        }

        [Fact]
        public void UpdateStationNameChargersTest() { Assert.True(false, "Test not yet implemented"); }

        [Theory]
        [InlineData("testname")]
        public void UpdateCustomerNameTest(string name) 
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Customer customer = bl.AddCustomer("different name", "0586693748", new Location(1, 1));
            bl.UpdateCustomerName(customer.ID, name);
            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.name == name && c.ID == customer.ID, "Update customer name assertion failed!");
        }

        [Theory]
        [InlineData(1, "0586693748")]
        public void UpdateCustomerPhoneTest(int ID, String phone)
        {
            Assert.True(false, "Test not yet implemented"); 
        }

        [Theory]
        [InlineData(1)]
        public void SendDroneToChargeTest(int droneID) 
        { 
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            //  test for non-existent ID
            Assert.Throws(typeof(IBL.BO.InvalidBlObjectException), () => bl.SendDroneToCharge(1));
            Station s = bl.AddStation("station", new Location(1,1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.light, s.ID);
            
        }

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
        public void GetStationList() 
        {
            // add a bunch -> check all IDs
            Assert.True(false, "Test not yet implemented");
        }

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
