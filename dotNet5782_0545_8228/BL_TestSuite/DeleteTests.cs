using BL;
using Xunit;

namespace BL_TestSuite
{
    public partial class BL_Tests
    {

        [Fact]
        public void DeleteDroneTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;

            Drone drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);
            bl.DeleteDrone(drone.ID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetDrone(drone.ID));

            drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            bl.DeleteDrone(drone.ID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetDrone(drone.ID));

            drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            int senderID = bl.AddCustomer("name1", "123", new Location(1, 1)).ID;
            int receiverID = bl.AddCustomer("name2", "124", new Location(2, 2)).ID;
            Package package = bl.AddPackage(senderID, receiverID, WeightCategories.light, Priorities.fast);
            bl.AssignPackageToDrone(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteDrone(drone.ID));

            bl.CollectPackage(drone.ID);
            bl.DeliverPackage(drone.ID);
            bl.DeleteDrone(drone.ID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetDrone(drone.ID));
        }

        [Fact]
        public void DeleteCustomerTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;
            int senderID = bl.AddCustomer("name1", "123", new Location(1, 1)).ID;
            int receiverID = bl.AddCustomer("name2", "124", new Location(2, 2)).ID;
            Drone drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);

            bl.DeleteCustomer(senderID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetCustomer(senderID));

            senderID = bl.AddCustomer("name1", "123", new Location(1, 1)).ID;
            int packageID = bl.AddPackage(senderID, receiverID, WeightCategories.heavy, Priorities.emergency).ID;
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            bl.AssignPackageToDrone(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteCustomer(senderID));
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteCustomer(receiverID));

            bl.CollectPackage(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteCustomer(senderID));
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteCustomer(receiverID));

            bl.DeliverPackage(drone.ID);
            bl.DeleteCustomer(senderID);
            bl.DeleteCustomer(receiverID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetCustomer(senderID));
            Assert.Throws<InvalidBlObjectException>(() => bl.GetCustomer(receiverID));
        }

        [Fact]
        public void DeletePackageTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;
            int senderID = bl.AddCustomer("name1", "123", new Location(1, 1)).ID;
            int receiverID = bl.AddCustomer("name2", "124", new Location(2, 2)).ID;
            Drone drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);
            int packageID = bl.AddPackage(senderID, receiverID, WeightCategories.heavy, Priorities.emergency).ID;

            bl.DeletePackage(packageID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetPackage(senderID));

            packageID = bl.AddPackage(senderID, receiverID, WeightCategories.heavy, Priorities.emergency).ID;
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            bl.AssignPackageToDrone(drone.ID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeletePackage(packageID));

            bl.CollectPackage(drone.ID);
            bl.DeliverPackage(drone.ID);
            bl.DeletePackage(packageID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetPackage(packageID));
        }

        [Fact]
        public void DeleteStationTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            int stationID = bl.AddStation("name", new Location(1, 1), 5).ID;

            bl.DeleteStation(stationID);
            Assert.Throws<InvalidBlObjectException>(() => bl.GetStation(stationID));

            stationID = bl.AddStation("name", new Location(1, 1), 5).ID;
            Drone drone = bl.AddDrone("m1", WeightCategories.heavy, stationID);
            Assert.Throws<OperationNotPossibleException>(() => bl.DeleteStation(stationID));
        }

    }
}
