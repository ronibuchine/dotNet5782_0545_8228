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
            BLOBject.BLOBject bl = new();
            /* Assert.Throws(typeof(IBL.BO.InvalidBlObjectException), () => { bl.AddDrone(ID, model, maxWeight, stationID); }); */
            Drone drone = bl.AddDrone(model, maxWeight, stationID);
            IBL.BO.Drone d = bl.GetDrone(drone.ID);
            Assert.True(d.battery == drone.battery, "Drone ID is incorrect");
        }
    }
}
