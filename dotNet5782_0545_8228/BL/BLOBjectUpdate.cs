using System;

namespace BLOBjectNamespace
{
    public partial class BLOBject : IBL.IBLInterface
    {

        public void UpdateDrone(int ID, string newModel)
        {
            try
            {
                dal.GetActualDrone(ID).model = newModel;
                IBL.BO.Drone drone = GetDrone(ID);
                drone.model = newModel;
            }
            catch (IDAL.DO.InvalidDalObjectException e)
            {
                throw new IBL.BO.InvalidBlObjectException(e.Message);
            }
        }

        public void UpdateStation(int stationID, string stationName) // either one of the last two parameters must be entered or both of them
        {
            try
            {
                dal.GetActualStation(stationID).name = stationName;
            }
            catch (IDAL.DO.InvalidDalObjectException e)
            {
                throw new IBL.BO.InvalidBlObjectException(e.Message);
            }
        }

        public void UpdateStation(int stationID, int numChargers)
        {
            if (GetCountChargingDrones(stationID) > numChargers)
            {
                throw new IBL.BO.InvalidBlObjectException("There are currently more drones charging here than update request");
            }
            try
            {
                dal.GetActualStation(stationID).chargeSlots = numChargers;
            }
            catch (IDAL.DO.InvalidDalObjectException e)
            {
                throw new IBL.BO.InvalidBlObjectException(e.Message);
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
                dal.GetActualCustomer(ID).name = name;
            }
            catch (IDAL.DO.InvalidDalObjectException e)
            {
                throw new IBL.BO.InvalidBlObjectException(e.Message);
            }
        }

        public void UpdateCustomerPhone(int ID, String phone)
        {
            try
            {
                // TODO validate phone number
                dal.GetActualCustomer(ID).phone = phone;
            }
            catch (IDAL.DO.InvalidDalObjectException e)
            {
                throw new IBL.BO.InvalidBlObjectException(e.Message);
            }
        }

        public void UpdateCustomer(int ID, string name, String phone)
        {
            UpdateCustomerName(ID, name);
            UpdateCustomerPhone(ID, phone);
        }

    }
}
