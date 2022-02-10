using System;
using System.Linq;
using DO;
using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALXml : IDAL
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int ID, string name, string phone, string password = null)
        {
            UpdateCustomerName(ID, name);
            UpdateCustomerPhone(ID, phone);
            UpdateCustomerPassword(ID, password);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerName(int ID, string name)
        {
            customersRoot = LoadXml("customers");
            var customer = customersRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .FirstOrDefault();
            if (customer == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            customer.Element("name").Value = name;
            SaveXml("customers");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPassword(int ID, string password)
        {
            customersRoot = LoadXml("customers");
            var customer = customersRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .FirstOrDefault();
            if (customer == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            customer.Element("password").Value = password;
            SaveXml("customers");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomerPhone(int ID, string phone)
        {
            customersRoot = LoadXml("customers");
            var customer = customersRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .FirstOrDefault();
            if (customer == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            customer.Element("phone").Value = phone;
            SaveXml("customers");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int ID, string newModel)
        {
            dronesRoot = LoadXml("drones");
            var drone = dronesRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .FirstOrDefault();
            if (drone == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system.");
            drone.Element("model").Value = newModel;
            SaveXml("drones");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName)
        {
            stationsRoot = LoadXml("stations");
            var station = stationsRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == stationID)
                .FirstOrDefault(); 
            if (station == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system."); 
            station.Element("name").Value = stationName;
            SaveXml("stations");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, int numChargers)
        {
            stationsRoot = LoadXml("stations");
            var station = stationsRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == stationID)
                .FirstOrDefault(); 
            if (station == null)
                throw new DataSourceException("The item you are trying to update does not exist in the system."); 
            station.Element("chargeSlots").Value = numChargers.ToString();
            SaveXml("stations");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int stationID, string stationName, int numChargers)
        {
            UpdateStation(stationID, stationName);
            UpdateStation(stationID, numChargers);
        }

    }
}
