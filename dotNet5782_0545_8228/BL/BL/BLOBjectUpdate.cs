using System;
using System.Linq;

namespace BL
{
    public partial class BLOBject : IBL.IBLInterface
    {

     
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

     
        public void UpdateStation(int stationID, string stationName, int numChargers)
        {
            UpdateStation(stationID, stationName);
            UpdateStation(stationID, numChargers);
        }

      
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

       
        public void UpdateCustomer(int ID, string name, String phone)
        {
            UpdateCustomerName(ID, name);
            UpdateCustomerPhone(ID, phone);
        }

    }
}
