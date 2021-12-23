using DO;
using DALAPI;
using System;

namespace DAL
{
    public partial class DalObject : IDAL
    {
        public void UpdateDrone(int droneID, string newModel)
        {
            try
            {
                GetActualDrone(droneID).model = newModel;
            }
            catch (InvalidDalObjectException)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }

        public void UpdateStation(int stationID, string stationName)
        {
            try
            {
                GetActualStation(stationID).name = stationName;
            }
            catch (InvalidDalObjectException)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }

        public void UpdateStation(int stationID, int numChargers)
        {
            try
            {
                GetActualStation(stationID).chargeSlots = numChargers;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
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
                GetActualCustomer(ID).name = name;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }

        public void UpdateCustomerPhone(int ID, string phone)
        {
            try
            {
                GetActualCustomer(ID).phone = phone;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }

        public void UpdateCustomer(int ID, string name, string phone)
        {
            UpdateCustomerPhone(ID, phone);
            UpdateCustomerName(ID, name);
        }

    }
}
