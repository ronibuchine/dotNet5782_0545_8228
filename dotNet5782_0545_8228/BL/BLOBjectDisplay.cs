using System;
using System.Collections.Generic;
using IBL.BO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public DroneStation DisplayBaseStation();
        public Drone DisplayDrone();
        public Customer DisplayCustomer();
        public Package DisplayPackage();
        public List<DroneStation> DisplayStationList();
        public List<DroneToList> DisplayDroneList();
        public List<CustomerToList> DisplayCustomerList();
        public List<PackageToList> DisplayPackageList();
        public List<PackageToList> DisplayUnassignedPackages();
        public List<BaseStationToList> DisplayAvailableStations();
    }
}
