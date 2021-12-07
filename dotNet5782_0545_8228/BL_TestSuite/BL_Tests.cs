using IBL.BO;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace BL_TestSuite
{
    public class BL_Tests
    {


        [Theory]
        [InlineData("model1", WeightCategories.heavy)]
        public void AddGetDroneTest(string model, WeightCategories maxWeight)
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;
            Drone drone = bl.AddDrone(model, maxWeight, stationID);
            Drone d = bl.GetDrone(drone.ID);
            Assert.True(d.ID == drone.ID, "Assertion for AddDrone failed");

            for (int i = 0; i < 4; i++)
            {
                bl.AddDrone($"{model}_{i + 1}", maxWeight, stationID);
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
                bl.AddStation($"{name}_{i + 1}", new Location(i + 1, i + 1), availableChargers);
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
            Station s = bl.AddStation("name", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s.ID);
            bl.UpdateDrone(d.ID, "newModel");
            d = bl.GetDrone(d.ID);
            Assert.True(d.model == "newModel", "drone model not updated");
        }

        [Fact]
        public void UpdateStationNameTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1, 1), 5);
            bl.UpdateStation(s.ID, "newName");
            s = bl.GetStation(s.ID);
            Assert.True(s.name == "newName", "station model not updated");
        }

        [Fact]
        public void UpdateStationChargersTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1, 1), 2);
            Drone d = bl.AddDrone("droneModel", WeightCategories.heavy, s.ID);
            s = bl.GetStation(s.ID);
            /* bl.SendDroneToCharge(d.ID); */
            Assert.Throws<InvalidBlObjectException>(() => bl.UpdateStation(s.ID, 0));
            bl.UpdateStation(s.ID, 3);
            s = bl.GetStation(s.ID);
            Assert.True(s.chargeSlots == 3, "station model not updated");
        }

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
        [InlineData("0586693748")]
        public void UpdateCustomerPhoneTest(string phone)
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Customer customer = bl.AddCustomer("name", "111111111", new Location(1, 1));
            bl.UpdateCustomerPhone(customer.ID, phone);
            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.phone == phone && c.ID == customer.ID, "Update customer phone assertion failed!");
        }

        [Fact]
        public void DroneIsTooFarAwayTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s1.ID);
            Station s2 = bl.AddStation("station", new Location(-1, -179), 5);
            Customer roni = bl.AddCustomer("Roni", "9999999999", new Location(1, 1));
            Customer eli = bl.AddCustomer("Eli", "9999999999", new Location(2, 35));
            bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.emergency);
            bl.ReleaseDroneFromCharge(d.ID, 1);
            bl.AssignPackageToDrone(d.ID);
            bl.CollectPackage(d.ID);
            bl.DeliverPackage(d.ID);
            Assert.Throws<InvalidBlObjectException>(() => bl.SendDroneToCharge(d.ID));
        }

        [Fact]
        public void DroneIsNotFreeTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s1.ID);
            Station s2 = bl.AddStation("station", new Location(-1, -179), 5);
            bl.AddDrone("model", WeightCategories.light, s2.ID);
            Customer roni = bl.AddCustomer("Roni", "9999999999", new Location(1, 1));
            Customer eli = bl.AddCustomer("Eli", "9999999999", new Location(2, 35));
            bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.emergency);
            Assert.Throws<InvalidBlObjectException>(() => bl.SendDroneToCharge(d.ID));
        }


        [Fact]
        public void NoAvailableChargersTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 1);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s1.ID);
            Station s2 = bl.AddStation("station", new Location(-1, -179), 1);
            bl.AddDrone("model", WeightCategories.light, s2.ID);
            Customer roni = bl.AddCustomer("Roni", "9999999999", new Location(1, 1));
            Customer eli = bl.AddCustomer("Eli", "9999999999", new Location(2, 35));
            bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.emergency);
            Assert.Throws<InvalidBlObjectException>(() => bl.SendDroneToCharge(d.ID));
        }
        [Fact]
        public void ReleaseDroneFromChargeTest()
        {
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
            // add a bunch -> check all names
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            for (int i = 0; i < 5; i++)
            {
                bl.AddStation($"name_{i}", new Location(i + 1, i + 1), 5);
            }
            List<StationToList> list = bl.GetStationList();
            int j = 0;
            foreach (StationToList station in list)
            {
                Assert.True(station.name == $"name_{j}");
                j++;
            }
        }

        [Fact]
        public void GetDroneList()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station station = bl.AddStation("name", new Location(1, 1), 10);
            for (int i = 0; i < 5; i++)
            {
                bl.AddDrone($"model_{i}", WeightCategories.light, station.ID);
            }
            List<DroneToList> list = bl.GetDroneList();
            int j = 0;
            foreach (DroneToList drone in list)
            {
                Assert.True(drone.model == $"model_{j}");
                j++;
            }
        }

        [Fact]
        public void GetCustomerList()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            for (int i = 0; i < 5; i++)
            {
                bl.AddCustomer($"name_{i}", $"000000000{i}", new Location(i + 1, i + 1));
            }
            List<CustomerToList> list = bl.GetCustomerList();
            int j = 0;
            foreach (CustomerToList customer in list)
            {
                Assert.True(customer.name == $"name_{j}");
                j++;
            }
        }

        [Fact]
        public void GetPackageList()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Customer sender = bl.AddCustomer("roni", "0000000000", new Location(1, 1));
            Customer receiver = bl.AddCustomer("eli", "1111111111", new Location(2, 2));
            List<int> idList = new(5);

            for (int i = 0; i < 5; i++)
            {
                idList.Add(bl.AddPackage(sender.ID, receiver.ID, WeightCategories.light, Priorities.regular).ID);
            }
            List<PackageToList> list = bl.GetPackageList();
            int j = 0;
            foreach (PackageToList package in list)
            {
                Assert.True(package.ID == idList[j]);
                j++;
            }
        }

        [Fact]
        public void GetUnassignedPackagesTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);
            Station station = bl.AddStation("name", new(1, 1), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.heavy, station.ID);
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1));
            Customer eli = bl.AddCustomer("eli", "00000000", new Location(2, 2));
            Package package1 = bl.AddPackage(roni.ID, eli.ID, WeightCategories.heavy, Priorities.emergency);
            bl.AssignPackageToDrone(drone.ID);
            Package package2 = bl.AddPackage(roni.ID, eli.ID, WeightCategories.heavy, Priorities.emergency);
            List<Package> unassignedPackages = bl.GetUnassignedPackages();

            Assert.True(unassignedPackages[0].ID == package2.ID && unassignedPackages.Count == 1, "Assertion for GetAllUnassignedPackages failed");

        }

        [Fact]
        public void GetAvailableStationsTest()
        {
            IBL.IBLInterface bl = new BLOBjectNamespace.BLOBject(null);

            Station station1 = bl.AddStation("empty station", new Location(1, 1), 5);
            Station station2 = bl.AddStation("full station", new(2, 2), 1);
            Drone drone = bl.AddDrone("model", WeightCategories.heavy, station2.ID);

            List<Station> availableStations = bl.GetAvailableStations();
            Assert.True(availableStations[0].ID == station1.ID && availableStations.Count == 1, "Assertion for GetAvailableStations failed");
        }

    }
}
