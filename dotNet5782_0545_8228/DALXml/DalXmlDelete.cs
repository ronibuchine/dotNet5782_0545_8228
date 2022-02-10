using System;
using System.Linq;
using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALXml : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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
