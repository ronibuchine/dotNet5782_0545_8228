
using BL;
using IBL;
using Xunit;
using System;
using DALAPI;

namespace BL_TestSuite
{
    public partial class BL_Tests
    {

        Station s;
        Drone d;
        private void InitPersistence()
        {
            IBLInterface bl = new BLOBject(null);
            s = bl.AddStation("star destroyer", new Location(1, 1), 5);
            d = bl.AddDrone("model_a", WeightCategories.heavy, s.ID);
            s = bl.GetStation(s.ID);

        }

        [Fact]
        public void XMLPersistenceTest()
        {
            if (DalConfig.DalName == "xml")
            {
                InitPersistence();
                IBLInterface bl = new BLOBject();
                Station newS = bl.GetStation(s.ID);
                Drone newD = bl.GetDrone(d.ID);
                Assert.Equal(newS.ID, s.ID);
                Assert.Equal(newS.name, s.name);
                Assert.Equal(newS.location, s.location);
                Assert.Equal(newS.chargeSlots, s.chargeSlots);

                Assert.Equal(newD.ID, d.ID);
                Assert.Equal(newD.model, d.model);
                Assert.Equal(newD.weightCategory, d.weightCategory);
                Assert.Equal(newD.currentLocation, d.currentLocation);

            }
        }

    }
}
