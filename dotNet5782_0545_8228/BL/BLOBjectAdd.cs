using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public void AddBaseStation(int stationID, string name, Location location, int availableChargers);
        public void AddDrone(int ID, string model, WeightCategories maxWeight, int stationID);
        public void NewCustomer(int customerID, string name, int phone, Location location);
        public void NewPackage(int senderID, int receiverID, WeightCategories weight, Priorities priority);
    }
}
