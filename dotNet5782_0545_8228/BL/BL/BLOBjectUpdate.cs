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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName) // either one of the last two parameters must be entered or both of them
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, int numChargers)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName, int numChargers)
        {
            UpdateStation(stationID, stationName);
            UpdateStation(stationID, numChargers);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerName(int ID, string name)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPhone(int ID, String phone)
        {
            try
            {
                // TODO validate phone number
                dal.UpdateCustomerPhone(ID, phone);
            }
            catch (DO.InvalidDalObjectException e)
            {
                throw new InvalidBlObjectException(e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPassword(int ID, string password)
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int ID, string name, string phone, string password = null)
        {
            dal.UpdateCustomer(ID, name, phone, password);
        }

    }
}
