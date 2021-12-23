using BL;
using Xunit;

namespace BL_TestSuite
{
    public partial class BL_Tests
    {

        [Fact]
        public void UpdateDroneTest()
        {
            // add -> update -> get -> check
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1, 1), 5);
            Drone d = bl.AddDrone("model", WeightCategories.heavy, s.ID);
            bl.UpdateDrone(d.ID, "newModel");
            d = bl.GetDrone(d.ID);
            Assert.True(d.model == "newModel", "drone model not updated");
        }

        [Fact]
        public void UpdateStationNameTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1, 1), 5);
            bl.UpdateStation(s.ID, "newName");
            s = bl.GetStation(s.ID);
            Assert.True(s.name == "newName", "station model not updated");
        }

        [Fact]
        public void UpdateStationChargersTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station s = bl.AddStation("name", new Location(1, 1), 2);
            Drone d = bl.AddDrone("droneModel", WeightCategories.heavy, s.ID);
            s = bl.GetStation(s.ID);
            /* bl.SendDroneToCharge(d.ID); */
            Assert.Throws<InvalidBlObjectException>(() => bl.UpdateStation(s.ID, -1));
            bl.UpdateStation(s.ID, 3);
            s = bl.GetStation(s.ID);
            Assert.True(s.chargeSlots == 3, "station model not updated");
        }

        [Theory]
        [InlineData("testname")]
        public void UpdateCustomerNameTest(string name)
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer customer = bl.AddCustomer("different name", "0586693748", new Location(1, 1));
            bl.UpdateCustomerName(customer.ID, name);
            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.name == name && c.ID == customer.ID, "Update customer name assertion failed!");
        }

        [Theory]
        [InlineData("0586693748")]
        public void UpdateCustomerPhoneTest(string phone)
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer customer = bl.AddCustomer("name", "111111111", new Location(1, 1));
            bl.UpdateCustomerPhone(customer.ID, phone);
            Customer c = bl.GetCustomer(customer.ID);
            Assert.True(c.phone == phone && c.ID == customer.ID, "Update customer phone assertion failed!");
        }
    }
}
