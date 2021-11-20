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

            double free = DalObject.DataSource.Config.free;
            double lightWeight = DalObject.DataSource.Config.lightWeight;
            double midWeight = DalObject.DataSource.Config.midWeight;
            double heavyWeight = DalObject.DataSource.Config.heavyWeight;
            double chargingRate = DalObject.DataSource.Config.chargingRate;

        }

    }
}
