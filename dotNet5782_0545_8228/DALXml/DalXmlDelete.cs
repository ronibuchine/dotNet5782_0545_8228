
using System;
using System.Linq;
using DO;

using DALAPI;

namespace DAL
{
    public partial class DALXml : IDAL
    {
        public void DeleteCustomer(int ID)
        {
            customersRoot = LoadXml("customers");
            customersRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .First()
                .Element("IsActive")
                .Value = "false";
            SaveXml("customers");
        }

        public void DeleteDrone(int ID)
        {
            dronesRoot = LoadXml("drones");
            dronesRoot
                .Elements()
                .Where(d => Int32.Parse(d.Element("ID").Value) == ID)
                .First()
                .Element("IsActive")
                .Value = "false";
            SaveXml("drones");
        }

        public void DeleteEmployee(int ID)
        {
            employeesRoot = LoadXml("employees");
            employeesRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .First()
                .Element("IsActive")
                .Value = "false";
            SaveXml("employees");
        }

        public void DeletePackage(int ID)
        {
            packagesRoot = LoadXml("packages");
            packagesRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .First()
                .Element("IsActive")
                .Value = "false";
            SaveXml("packages");
        }

        public void DeleteStation(int ID)
        {
            stationsRoot = LoadXml("stations");
            stationsRoot
                .Elements()
                .Where(c => Int32.Parse(c.Element("ID").Value) == ID)
                .First()
                .Element("IsActive")
                .Value = "false";
            SaveXml("stations");
        }

    }
}
