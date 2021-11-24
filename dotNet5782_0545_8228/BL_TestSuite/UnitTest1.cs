using IBL.BO;
using Xunit;

namespace BL_TestSuite
{
    public class UnitTest1
    {
        // example
        [Theory]
        [InlineData(1, "model1", IBL.BO.WeightCategories.heavy, 1)]
        [InlineData(100, "model1", IBL.BO.WeightCategories.heavy, 1)]
        public void AddDroneTest(int ID, string model, WeightCategories maxWeight, int stationID)
        {
            BLOBject.BLOBject bl = new();
            /* Assert.Throws(typeof(IBL.BO.InvalidBlObjectException), () => { bl.AddDrone(ID, model, maxWeight, stationID); }); */
            bl.AddDrone(ID, model, maxWeight, stationID);
            IBL.BO.Drone d = bl.GetDrone(ID);
            Assert.True(d.ID == ID, "Drone ID is incorrect");
        }
    }
}
