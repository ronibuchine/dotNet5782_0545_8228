using DO;
using DALAPI;
using System;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALObject : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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
                GetActualCustomer(ID).name = name;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPhone(int ID, string phone)
        {
            try
            {
                GetActualCustomer(ID).phone = phone;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPassword(int ID, string password)
        {
            try
            {
                GetActualCustomer(ID).password = password;
            }
            catch (ArgumentNullException)
            {
                throw new DataSourceException("The item you are trying to update trying to update does not exist in the system.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int ID, string name, string phone, string password = null)
        {
            UpdateCustomerPhone(ID, phone);
            UpdateCustomerName(ID, name);
            UpdateCustomerPassword(ID, password);
        }

    }
}
