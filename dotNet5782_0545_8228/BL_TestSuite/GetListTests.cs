using BL;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BL_TestSuite
{
    public partial class BL_Tests
    {

        [Fact]
        public void GetStationList()
        {
            // add a bunch -> check all names
            IBL.IBLInterface bl = new BL.BLOBject(null);
            for (int i = 0; i < 5; i++)
            {
                bl.AddStation($"name_{i}", new Location(i + 1, i + 1), 5);
            }
            IEnumerable<StationToList> list = bl.GetStationList();
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
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station station = bl.AddStation("name", new Location(1, 1), 10);
            for (int i = 0; i < 5; i++)
            {
                bl.AddDrone($"model_{i}", WeightCategories.light, station.ID);
            }
            IEnumerable<DroneToList> list = bl.GetDroneList();
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
            IBL.IBLInterface bl = new BL.BLOBject(null);
            for (int i = 0; i < 5; i++)
            {
                bl.AddCustomer($"name_{i}", $"000000000{i}", new Location(i + 1, i + 1));
            }
            IEnumerable<CustomerToList> list = bl.GetCustomerList();
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
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Customer sender = bl.AddCustomer("roni", "0000000000", new Location(1, 1));
            Customer receiver = bl.AddCustomer("eli", "1111111111", new Location(2, 2));
            List<int> idList = new(5);

            for (int i = 0; i < 5; i++)
            {
                idList.Add(bl.AddPackage(sender.ID, receiver.ID, WeightCategories.light, Priorities.regular).ID);
            }
            IEnumerable<PackageToList> list = bl.GetPackageList();
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
            IBL.IBLInterface bl = new BL.BLOBject(null);
            Station station = bl.AddStation("name", new(1, 1), 5);
            Drone drone = bl.AddDrone("model", WeightCategories.heavy, station.ID);
            bl.ReleaseDroneFromCharge(drone.ID, 1);
            Customer roni = bl.AddCustomer("roni", "00000000", new(1, 1));
            Customer eli = bl.AddCustomer("eli", "00000000", new Location(2, 2));
            Package package1 = bl.AddPackage(roni.ID, eli.ID, WeightCategories.heavy, Priorities.emergency);
            bl.AssignPackageToDrone(drone.ID);
            Package package2 = bl.AddPackage(roni.ID, eli.ID, WeightCategories.heavy, Priorities.emergency);
            IEnumerable<Package> unassignedPackages = bl.GetUnassignedPackages();

            Assert.True(unassignedPackages.ElementAt(0).ID == package2.ID && unassignedPackages.Count() == 1, "Assertion for GetAllUnassignedPackages failed");

        }

        [Fact]
        public void GetAvailableStationsTest()
        {
            IBL.IBLInterface bl = new BL.BLOBject(null);

            Station station1 = bl.AddStation("empty station", new Location(1, 1), 5);
            Station station2 = bl.AddStation("full station", new(2, 2), 1);
            Drone drone = bl.AddDrone("model", WeightCategories.heavy, station2.ID);

            IEnumerable<Station> availableStations = bl.GetAvailableStations();
            Assert.True(availableStations.ElementAt(0).ID == station1.ID && availableStations.Count() == 1, "Assertion for GetAvailableStations failed");
        }

    }
}
