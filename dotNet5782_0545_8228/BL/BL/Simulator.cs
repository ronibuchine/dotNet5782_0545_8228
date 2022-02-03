using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BL
{
    internal class Simulator
    {

        const double DRONE_SPEED = 100;
        const int DELAY = 500;

        internal Simulator(IBL.IBLInterface bl, int droneID, Action action, Func<bool> stopCheck)
        {
            Drone drone = bl.GetDrone(droneID);
            // the stopcheck function will assign a package to a drone if it is possible
            while (!stopCheck())
            {                
                action.Invoke();
                Thread.Sleep(DELAY);
                lock(bl)
                {
                    bl.CollectPackage(droneID);
                }
                action.Invoke();
                Thread.Sleep(DELAY);
                lock(bl)
                {
                    bl.DeliverPackage(droneID);
                }
                action.Invoke();
                Thread.Sleep(DELAY);
                while (bl.GetDrone(droneID).battery < 100)
                {
                    lock (bl) 
                    {
                        bl.SendDroneToCharge(droneID);
                    }
                    Thread.Sleep(DELAY*2);
                    action.Invoke();
                    lock (bl)
                    {
                        bl.ReleaseDroneFromCharge(droneID);
                    }
                    action.Invoke();
                }
                
            }
        }
    }
}
