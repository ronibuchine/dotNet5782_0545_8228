using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public void UpdateDrone(int ID, string newModel);
        public void UpdateStation(int stationID, string stationName); // either one of the last two parameters must be entered or both of them
        public void UpdateStation(int stationID, int numChargers);
        public void UpdateStation(int stationID, string stationName, int numChargers);
        public void UpdateCustomer(int ID, string name);
        public void UpdateCustomer(int ID, int phone);
        public void UpdateCustomer(int ID, string name, int phone);

    }
}
