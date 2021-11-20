using System;
using System.Collections.Generic;
using DalObject;
using IDAL;
using IBL.BO;



namespace BLOBject
{
    public partial class BLOBject : IBL.IBLInterface
    {
        public double chargingRate { get; set; }
        // need to have some field here for power consumption
        public BLOBject()
        {
            IDAL.IdalInterface dal = new DalObject.DalObject();
            List<Drone> drones = dal.DisplayAllDrones();
        }

    }
}
