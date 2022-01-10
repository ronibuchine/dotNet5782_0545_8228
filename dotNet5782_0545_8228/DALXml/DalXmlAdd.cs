using DO;
using DALAPI;
using System.Linq;
using System.Xml.Linq;

namespace DAL
{
    public partial class DALXml : IDAL
    {
        public void AddCustomer(Customer customer)
        {
            int count = LoadXml("customers").Elements().Count();
            if (count + 1 <= MAX_CUSTOMERS)
                customersRoot.Add(CustomerToXElement(customer));
            else
                throw new DataSourceException("The entity could not be added to the system.");
            SaveXml("customers");
        }

        public void AddDrone(Drone drone)
        {
            int count = LoadXml("drones").Elements().Count();
            if (count + 1 <= MAX_DRONES)
                dronesRoot.Add(DroneToXElement(drone));
            else
                throw new DataSourceException("The entity could not be added to the system.");
            SaveXml("drones");
        }

        public void AddEmployee(int ID, string password)
        {
            int count = LoadXml("employees").Elements().Count();
            if (count + 1 <= MAX_EMPLOYEES)
                employeesRoot.Add(EmployeeToXElement(new Employee(ID, password)));
            else
                throw new DataSourceException("The entity could not be added to the system.");
            SaveXml("employees");
        }

        public void AddPackage(Package package)
        {
            int count = LoadXml("packages").Elements().Count();
            if (count + 1 <= MAX_PACKAGES)
                employeesRoot.Add(PackageToXElement(package));
            else
                throw new DataSourceException("The entity could not be added to the system.");
            SaveXml("packages");
        }

        public void AddStation(Station droneStation)
        {
            int count = LoadXml("stations").Elements().Count();
            if (count + 1 <= MAX_STATIONS)
                stationsRoot.Add(StationToXElement(droneStation));
            else
                throw new DataSourceException("The entity could not be added to the system.");
            SaveXml("stations");
        }

        private XElement DroneToXElement(Drone drone)
        {
            return new XElement("drone",
                    new XElement("ID", drone.ID),
                    new XElement("IsActive", drone.IsActive),
                    new XElement("model", drone.model),
                    new XElement("maxWeight", drone.maxWeight));
        }

        private XElement PackageToXElement(Package package)
        {
            return new XElement("package",
                    new XElement("ID", package.ID),
                    new XElement("IsActive", package.IsActive),
                    new XElement("senderId", package.senderId),
                    new XElement("recieverId", package.recieverId),
                    new XElement("droneId", package.droneId),
                    new XElement("weight", package.weight),
                    new XElement("priority", package.priority),
                    new XElement("requested", package.requested != null ? package.requested.ToString() : "null"),
                    new XElement("scheduled", package.scheduled != null ? package.scheduled.ToString() : "null"),
                    new XElement("pickedUp", package.pickedUp != null ? package.pickedUp.ToString() : "null"),
                    new XElement("delivered", package.delivered != null ? package.delivered.ToString() : "null"));
        }

        private XElement StationToXElement(Station station)
        {
            return new XElement("station",
                    new XElement("ID", station.ID),
                    new XElement("IsActive", station.IsActive),
                    new XElement("name", station.name),
                    new XElement("chargeSlots", station.chargeSlots),
                    new XElement("longitude", station.longitude),
                    new XElement("latitude", station.latitude));
        }

        private XElement DroneChargeToXElement(DroneCharge droneCharge)
        {
            return new XElement("dronCharge",
                    new XElement("DroneId", droneCharge.DroneId),
                    new XElement("StationId", droneCharge.StationId));
        }

        private XElement EmployeeToXElement(Employee employee)
        {
            return new XElement("employee",
                    new XElement("ID", employee.ID),
                    new XElement("IsActive", employee.IsActive),
                    new XElement("name", employee.password));
        }

        private XElement CustomerToXElement(Customer customer)
        {
            return new XElement("customer",
                    new XElement("ID", customer.ID),
                    new XElement("IsActive", customer.IsActive),
                    new XElement("name", customer.name),
                    new XElement("phone", customer.phone),
                    new XElement("longitude", customer.longitude),
                    new XElement("latitude", customer.latitude),
                    new XElement("password", customer.password));
        }
    }
}
