using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public void SendDroneToCharge(int droneID);
        public void ReleaseDroneFromCharge(int droneID, DateTime chargeTime);
        public void AssignPackageToDrone(int droneID);
        public void CollectPackage(int droneID);
        public void DeliverPackage(int droneID);

    }
}
