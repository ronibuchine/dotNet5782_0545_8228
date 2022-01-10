using BL;
using System;
using System.Threading;
using Xunit;

namespace BL_TestSuite
{
    public partial class BL_Tests
    {

        [Fact]
        public void DroneIsTooFarAwayTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s1.ID);
            bl.ReleaseDroneFromCharge(d.ID);
            Station s2 = bl.AddStation("station", new Location(-1, -179), 5);
            Customer roni = bl.AddCustomer("Roni", "9999999999", new Location(1, 1), 123);
            Customer eli = bl.AddCustomer("Eli", "9999999999", new Location(2, 35), 12);

            Assert.Throws<OperationNotPossibleException>(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.emergency);
                    bl.AssignPackageToDrone(d.ID);
                    bl.CollectPackage(d.ID);
                    bl.DeliverPackage(d.ID);
                    bl.SendDroneToCharge(d.ID);
                    bl.ReleaseDroneFromCharge(d.ID);

                }
            });
        }

        [Fact]
        public void DroneIsNotFreeTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s1.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.SendDroneToCharge(d.ID));
        }


        [Fact]
        public void NoAvailableChargersTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 1);
            Drone d = bl.AddDrone("model1", WeightCategories.heavy, s1.ID);
            bl.ReleaseDroneFromCharge(d.ID);
            bl.AddDrone("model2", WeightCategories.heavy, s1.ID);
            Assert.Throws<BlObjectAccessException>(() => bl.SendDroneToCharge(d.ID));
        }

        [Fact]
        public void ReleaseDroneFromChargeTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 1);
            Drone d = bl.AddDrone("model1", WeightCategories.heavy, s1.ID);
            double? dronesBattery = d.battery;
            Thread.Sleep(1000);
            bl.ReleaseDroneFromCharge(d.ID);
            Assert.True(dronesBattery < d.battery, "Releasing drone from charge failed, battery didn't increase");

        }

        [Fact]
        public void DroneNotInMaintenanceTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s1 = bl.AddStation("station", new Location(1, 1), 1);
            Drone d = bl.AddDrone("model1", WeightCategories.heavy, s1.ID);
            bl.ReleaseDroneFromCharge(d.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.ReleaseDroneFromCharge(d.ID));
        }

        [Fact]
        public void AssignPackageWeightTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            var close = bl.AddCustomer("close", "000", new Location(2, 2), 123);
            var kindaClose = bl.AddCustomer("kinda close", "000", new Location(3, 3), 12);
            var far = bl.AddCustomer("far", "000", new Location(80, 80), 1);
            var s = bl.AddStation("s1", new Location(1, 1), 5);
            var drone = bl.AddDrone("model", WeightCategories.medium, s.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            var p1 = bl.AddPackage(close.ID, kindaClose.ID, WeightCategories.light, Priorities.regular);
            var p2 = bl.AddPackage(close.ID, kindaClose.ID, WeightCategories.heavy, Priorities.regular);
            bl.AssignPackageToDrone(drone.ID);
            p1 = bl.GetPackage(p1.ID);
            Assert.True(p1.drone.ID == drone.ID);
            Assert.True((p1.scheduled != null), "wrong package assigned");
        }

        [Fact]
        public void AssignPackagePriorityTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            var roni = bl.AddCustomer("roni", "000", new Location(2, 2), 123);
            var eli = bl.AddCustomer("eli", "000", new Location(3, 3), 12);
            var s = bl.AddStation("s1", new Location(1, 1), 5);
            var drone = bl.AddDrone("model", WeightCategories.medium, s.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            var p1 = bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.emergency);
            bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.regular);
            bl.AssignPackageToDrone(drone.ID);
            p1 = bl.GetPackage(p1.ID);
            Assert.True(p1.drone.ID == drone.ID);
        }

        [Fact]
        public void AssignPackageWeightOrderTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            var close = bl.AddCustomer("close", "000", new Location(2, 2), 123);
            var kindaClose = bl.AddCustomer("kinda close", "000", new Location(3, 3), 13);
            var far = bl.AddCustomer("far", "000", new Location(80, 80), 1);
            var s = bl.AddStation("s1", new Location(1, 1), 5);
            var drone = bl.AddDrone("model", WeightCategories.medium, s.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            var p1 = bl.AddPackage(close.ID, kindaClose.ID, WeightCategories.light, Priorities.regular);
            var p2 = bl.AddPackage(close.ID, kindaClose.ID, WeightCategories.medium, Priorities.regular);
            bl.AssignPackageToDrone(drone.ID);
            p2 = bl.GetPackage(p2.ID);
            Assert.True(p2.drone.ID == drone.ID);
        }

        [Fact]
        public void AssignPackageLocationTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            var close = bl.AddCustomer("close", "000", new Location(2, 2), 123);
            var kindaClose = bl.AddCustomer("kinda close", "000", new Location(3, 3), 12);
            var far = bl.AddCustomer("far", "000", new Location(80, 80), 1);
            var s = bl.AddStation("s1", new Location(1, 1), 5);
            var drone = bl.AddDrone("model", WeightCategories.medium, s.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            var p1 = bl.AddPackage(close.ID, kindaClose.ID, WeightCategories.light, Priorities.regular);
            var p2 = bl.AddPackage(kindaClose.ID, close.ID, WeightCategories.light, Priorities.regular);
            bl.AssignPackageToDrone(drone.ID);
            p1 = bl.GetPackage(p1.ID);
            Assert.True(p1.drone.ID == drone.ID);
        }


        [Fact]
        public void CollectPackageTest()
        {
            // initialize
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1), 123);
            Customer eli = bl.AddCustomer("eli", "111111111", new(2, 2), 13);
            Package package = bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.regular);
            Station station = bl.AddStation("name", new(3, 3), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.medium, station.ID);
            // perform appropriate prerequisite actions
            bl.ReleaseDroneFromCharge(drone.ID);
            bl.AssignPackageToDrone(drone.ID);
            DateTime? oldTime = package.pickedUp;
            // collect package and assert that the locaiton is the sender location and the pick up time is updated
            bl.CollectPackage(drone.ID);
            package = bl.GetPackage(package.ID);
            Assert.True(drone.currentLocation.Equals(roni.currentLocation) && package.pickedUp != null);
        }

        [Fact]
        public void PackageNotAbleToBeCollectedTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1), 123);
            Customer eli = bl.AddCustomer("eli", "111111111", new(1, 1), 13);
            Package package = bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.regular);
            Station station = bl.AddStation("name", new(1, 1), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.medium, station.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.CollectPackage(drone.ID));
            bl.ReleaseDroneFromCharge(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.CollectPackage(drone.ID));
        }

        [Fact]
        public void DeliverPackageTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1), 123);
            Customer eli = bl.AddCustomer("eli", "111111111", new(1, 1), 13);
            Package package = bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.regular);
            Station station = bl.AddStation("name", new(1, 1), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.medium, station.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            bl.AssignPackageToDrone(drone.ID);
            bl.CollectPackage(drone.ID);
            bl.DeliverPackage(drone.ID);
            DateTime? oldTime = package.delivered;
            package = bl.GetPackage(package.ID);

            Assert.True(drone.status == DroneStatuses.free &&
                drone.currentLocation.Equals(eli.currentLocation) &&
                package.delivered != null, "Asseriton for Delivered package failed");
        }

        [Fact]
        public void PackageNotAbleToBeDeliveredTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1), 123);
            Customer eli = bl.AddCustomer("eli", "111111111", new(1, 1), 23);
            Package package = bl.AddPackage(roni.ID, eli.ID, WeightCategories.light, Priorities.regular);
            Station station = bl.AddStation("name", new(1, 1), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.medium, station.ID);
            bl.ReleaseDroneFromCharge(drone.ID);
            bl.AssignPackageToDrone(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeliverPackage(drone.ID));
            bl.CollectPackage(drone.ID);
            bl.DeliverPackage(drone.ID);
        }

    }
}
