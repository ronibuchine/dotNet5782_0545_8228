using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int ID, string newModel)
        {
            lock (dal)
            {
                try
                {
                    dal.UpdateDrone(ID, newModel);
                    Drone drone = GetDrone(ID);
                    drone.model = newModel;
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName) // either one of the last two parameters must be entered or both of them
        {
            lock (dal)
            {
                try
                {
                    dal.UpdateStation(stationID, stationName);
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, int numChargers)
        {
            lock (dal)
            {
                Station s = GetStation(stationID);
                if (s.chargingDrones.Count() > numChargers)
                {
                    throw new InvalidBlObjectException("There are currently more drones charging here than update request");
                }
                try
                {
                    dal.UpdateStation(stationID, numChargers);
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName, int numChargers)
        {
            UpdateStation(stationID, stationName);
            UpdateStation(stationID, numChargers);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerName(int ID, string name)
        {
            lock (dal)
            {
                try
                {
                    dal.UpdateCustomerName(ID, name);
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPhone(int ID, String phone)
        {
            lock (dal)
            {
                try
                {
                    dal.UpdateCustomerPhone(ID, phone);
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPassword(int ID, string password)
        {
            lock (dal)
            {
                try
                {
                    dal.UpdateCustomerPassword(ID, password);
                }
                catch (DO.InvalidDalObjectException e)
                {
                    throw new InvalidBlObjectException(e.Message);
                }
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int ID, string name, string phone, string password = null)
        {
            lock (dal)
            {
                dal.UpdateCustomer(ID, name, phone, password);
            }
        }

    }
}
