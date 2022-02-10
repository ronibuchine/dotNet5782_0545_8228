using System;
using DO;
using DALAPI;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{

    public partial class DALXml : IDAL
    {
        // TODO: store this stuff in some config xml
        internal const int MIN_DRONES = 5;
        internal const int MIN_STATIONS = 5;
        internal const int MIN_CUSTOMERS = 5;
        internal const int MIN_PACKAGES = 5;
        internal const int MIN_EMPLOYEES = 1;

        internal const int MAX_DRONES = 10;
        internal const int MAX_STATIONS = 10;
        internal const int MAX_CUSTOMERS = 10;
        internal const int MAX_PACKAGES = 10;
        internal const int MAX_DRONE_CHARGES = MAX_DRONES;
        internal const int MAX_EMPLOYEES = 5;

        internal const string ADMIN_PASSWORD = "admin";

        internal String xmlPath;
        internal String dronesPath = "drones.xml";
        internal String packagesPath = "packages.xml";
        internal String stationsPath = "stations.xml";
        internal String employeesPath = "employees.xml";
        internal String droneChargesPath = "droneCharge.xml";
        internal String customersPath = "customers.xml";
        internal String configPath = "config.xml";

        internal XElement dronesRoot;
        internal XElement packagesRoot;
        internal XElement stationsRoot;
        internal XElement employeesRoot;
        internal XElement droneChargesRoot;
        internal XElement customersRoot;
        internal XElement configRoot;

        [MethodImpl(MethodImplOptions.Synchronized)]
        static private Drone XelementToDrone(XElement el)
        {
            IEnumerable<XElement> details = el.Elements();
            int ID = int.Parse(details.First(e => e.Name == "ID").Value);
            bool isActive = details.First(e => e.Name == "ID").Value == "true" ? true : false;
            string model = details.First(e => e.Name == "model").Value;
            WeightCategories weight = ParseWeightCategory(details.First(e => e.Name == "maxWeight").Value);
            var drone = new Drone(ID, model, weight);
            drone.IsActive = isActive;
            return drone;
        }

        private static readonly Lazy<DALXml> lazy = new Lazy<DALXml>(() => new DALXml());

        public static DALXml Instance { get { return lazy.Value; } }

        public static int nextID;

        private DALXml()
        {
            var path = Directory.GetCurrentDirectory();
            var containing = Directory.GetFiles(path, "*.sln");
            while (containing.Length == 0)
            {
                path = Directory.GetParent(path).FullName;
                containing = Directory.GetFiles(path, "*.sln");
            }
            xmlPath = path + "/data/";
            dronesPath = xmlPath + dronesPath;
            packagesPath = xmlPath + packagesPath;
            stationsPath = xmlPath + stationsPath;
            employeesPath = xmlPath + employeesPath;
            droneChargesPath = xmlPath + droneChargesPath;
            customersPath = xmlPath + customersPath;
            configPath = xmlPath + configPath;

            Initialize();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Initialize()
        {
            try
            {
                configRoot = LoadXml("config");
            }
            catch (FileNotFoundException)
            {
                throw new DataSourceException("Dal config must be setup to use the DALXml module");
            }

            try
            {
                employeesRoot = LoadXml("employees");
                dronesRoot = LoadXml("drones");
                stationsRoot = LoadXml("stations");
                customersRoot = LoadXml("customers");
                packagesRoot = LoadXml("packages");
                droneChargesRoot = LoadXml("droneCharges");
            }
            catch (FileNotFoundException)
            {
                Random rand = new();
                Clear();
                int nextID = 1;
                XElement.Load(configPath).Element("nextID").Value = nextID.ToString();

                employeesRoot = Create(employeesPath);
                int num = rand.Next(MIN_EMPLOYEES, MIN_EMPLOYEES + 1);
                for (int i = 0; i < num; i++)
                {
                    employeesRoot.Add(EmployeeToXElement(new Employee(nextID++, ADMIN_PASSWORD)));
                }
                SaveXml("employees");

                dronesRoot = Create(dronesPath);
                num = rand.Next(MIN_DRONES, MAX_DRONES + 1);
                for (int i = 0; i < num; i++)
                {
                    dronesRoot.Add(DroneToXElement(new Drone(nextID++)));
                }
                SaveXml("drones");

                stationsRoot = Create(stationsPath);
                num = rand.Next(MIN_STATIONS, MAX_STATIONS + 1);
                for (int i = 0; i < num; i++)
                {
                    stationsRoot.Add(StationToXElement(new Station(nextID++)));
                }
                SaveXml("stations");

                customersRoot = Create(customersPath);
                num = rand.Next(MIN_CUSTOMERS, MAX_CUSTOMERS + 1);
                for (int i = 0; i < num; i++)
                {
                    customersRoot.Add(CustomerToXElement(new Customer(nextID++)));
                }
                SaveXml("customers");

                packagesRoot = Create(packagesPath);
                num = rand.Next(MIN_PACKAGES, MAX_PACKAGES + 1);
                List<int> ids = new();
                packagesRoot
                    .Elements()
                    .ToList()
                    .ForEach(p => ids.Add(Int32.Parse(p.Element("droneID").Value)));
                var unassignedDrones = dronesRoot
                    .Elements()
                    .Where(d => !ids
                            .Contains(Int32.Parse(d.Element("ID").Value)));

                for (int i = 0; i < num; i++)
                {
                    int randX = rand.Next(customersRoot.Elements().Count());
                    int randY = RandomExceptX(customersRoot.Elements().Count(), randX, rand);
                    int senderID = Int32.Parse(customersRoot.Elements().ElementAt(randX).Element("ID").Value);
                    int recieverID = Int32.Parse(customersRoot.Elements().ElementAt(randY).Element("ID").Value);
                    int droneID = rand.Next(2) == 0 ? 0 : Int32.Parse(unassignedDrones.ElementAt(rand.Next(unassignedDrones.Count() - 1)).Element("ID").Value);
                    packagesRoot.Add(PackageToXElement(new Package(nextID++, senderID, recieverID, droneID)));
                }
                SaveXml("packages");

                configRoot = XElement.Load(configPath);
                configRoot.Element("nextID").Value = nextID.ToString();
                SaveXml("config");
            }
        }

        private static int RandomExceptX(int n, int x, Random rand)
        {
            int result = rand.Next(n);
            if (result != x)
                return result;
            return (result + 1) % n;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private XElement LoadXml(String choice)
        {
            return choice switch
            {
                "drones" => XElement.Load(dronesPath),
                "packages" => XElement.Load(packagesPath),
                "stations" => XElement.Load(stationsPath),
                "employees" => XElement.Load(employeesPath),
                "droneCharges" => XElement.Load(droneChargesPath),
                "customers" => XElement.Load(customersPath),
                "config" => XElement.Load(configPath),
                _ => throw new InvalidDataException()
            };
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SaveXml(String choice)
        {
            switch (choice)
            {
                case "drones":
                    dronesRoot.Save(dronesPath);
                    break;
                case "packages":
                    packagesRoot.Save(packagesPath);
                    break;
                case "stations":
                    stationsRoot.Save(stationsPath);
                    break;
                case "employees":
                    employeesRoot.Save(employeesPath);
                    break;
                case "droneCharges":
                    droneChargesRoot.Save(droneChargesPath);
                    break;
                case "customers":
                    customersRoot.Save(customersPath);
                    break;
                case "config":
                    configRoot.Save(configPath);
                    break;
                default:
                    throw new InvalidDataException();
            };
        }

        private XElement LoadList(string path)
        {
            return XElement.Load(path).Element("list");
        }

        private XElement Create(String path)
        {
            var xmlFile = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("list", ""));
            xmlFile.Save(path);
            return xmlFile.Element("list");
        }



        private Drone XElementToDroneList()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Clear()
        {
            dronesRoot = new XElement("list", "");
            SaveXml("drones");

            stationsRoot = new XElement("list", "");
            SaveXml("stations");

            packagesRoot = new XElement("list", "");
            SaveXml("packages");

            employeesRoot = new XElement("list", "");
            SaveXml("employees");

            droneChargesRoot = new XElement("list", "");
            SaveXml("droneCharges");

            customersRoot = new XElement("list", "");
            SaveXml("customers");

            configRoot = LoadXml("config");
            configRoot.Element("nextID").Value = "1";
            SaveXml("config");

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetNextID()
        {
            return Int32.Parse(XElement.Load(configPath).Element("nextID").Value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] PowerConsumptionRequest()
        {
            XElement powerConsumption = XElement.Load(configPath).Element("powerConsumption");
            double[] ret = {
                Double.Parse(powerConsumption.Element("free").Value),
                Double.Parse(powerConsumption.Element("lightWeight").Value),
                Double.Parse(powerConsumption.Element("midWeight").Value),
                Double.Parse(powerConsumption.Element("heavyWeight").Value),
                Double.Parse(powerConsumption.Element("chargingRate").Value)};
            return ret;
        }

    }
}
