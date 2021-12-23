using System.Collections.Generic;
using DO;
using DALAPI;
using System.Linq;
using System;

namespace DAL
{
    public partial class DalObject : IDAL
    {
        public void UpdateDrone(int droneID, string newModel)
        {
            try
            {
                DataSource.drones.Find(d => d.ID == droneID).model = newModel;
            }
            catch (ArgumentNullException e)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }
        public void UpdateStation(int stationID, string stationName)
        {
            try
            {
                DataSource.stations.Find(s => s.ID == stationID).name = stationName;
            }
            catch (ArgumentNullException e)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }
        public void UpdateStation(int stationID, int numChargers)
        {
            try
            {
                DataSource.stations.Find(s => s.ID == stationID).chargeSlots = numChargers;
            }
            catch (ArgumentNullException e)
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
                DataSource.customers.Find(c => c.ID == ID).name = name;
            }
            catch (ArgumentNullException e)
            {
                throw new DataSourceException("The item you requested does not exist in the system.");
            }
        }
        public void UpdateCustomerPhone(int ID, string phone)
        {
            try
            {
                DataSource.customers.Find(c => c.ID == ID).phone = phone;
            }
            catch (ArgumentNullException e)
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
