using System;
using DO;
using DALAPI;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DAL
{

    public partial class DALXml : IDAL
    {
        // TODO: store this stuff in some config xml
        internal const int MIN_DRONES = 5;
        internal const int MIN_DRONE_STATIONS = 5;
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

        /* static void Main() */
        /* { */
        /*     var path = Directory.GetCurrentDirectory(); */
        /*     var containing = Directory.GetFiles(path, "*.sln"); */
        /*     while (containing.Length == 0) */
        /*     { */
        /*         path = Directory.GetParent(path).FullName; */
        /*         containing = Directory.GetFiles(path, "*.sln"); */
        /*     } */

        /*     XElement dalConfig = XElement.Load(path + @"/xml/drones.xml"); */
        /*     IEnumerable<Drone> drones = dalConfig.Element("list").Elements().Select(e => XelementToDrone(e)); */

        /*     foreach (var drone in drones) */
        /*     { */
        /*         Console.Out.Write("" + drone.ToString()); */
        /*     } */



        /*     /1* var drone = new Drone(1, "model", WeightCategories.heavy); *1/ */
        /*     /1* System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(drone.GetType()); *1/ */
        /*     /1* x.Serialize(Console.Out, drone); *1/ */
        /*     /1* Console.WriteLine(); *1/ */
        /* } */

        /* static private WeightCategories ParseWeightCategory(string weight) => */
        /*     weight switch */
        /*     { */
        /*         "heavy" => WeightCategories.heavy, */
        /*         "medium" => WeightCategories.medium, */
        /*         "light" => WeightCategories.light, */
        /*         _ => throw new Exception("not valid weight") */
        /*     }; */

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
            configPath = xmlPath + configPath;

            Initialize();
        }

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
                dronesRoot = LoadXml("drones");
            }
            catch (FileNotFoundException)
            {
                dronesRoot = Create(dronesPath).Element("list");
            }

            try
            {
                packagesRoot = LoadXml("packages");
            }
            catch (FileNotFoundException)
            {
                packagesRoot = Create(packagesPath).Element("list");
            }

            try
            {
                stationsRoot = LoadXml("stations");
            }
            catch (FileNotFoundException)
            {
                stationsRoot = Create(stationsPath).Element("list");
            }

            try
            {
                employeesRoot = LoadXml("employees");
            }
            catch (FileNotFoundException)
            {
                employeesRoot = Create(employeesPath).Element("list");
            }

            try
            {
                droneChargesRoot = LoadXml("droneCharges");
            }
            catch (FileNotFoundException)
            {
                droneChargesRoot = Create(droneChargesPath).Element("list");
            }

            try
            {
                customersRoot = LoadXml("customers");
            }
            catch (FileNotFoundException)
            {
                customersRoot = Create(customersPath).Element("list");
            }
        }

        private XElement LoadXml(String choice) 
        {
            try 
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
            catch (FileNotFoundException e)
            {
                return Create(e.FileName);
            }
        }


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
                    droneChargesRoot.Save(droneChargesPath);
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
            var name = Path.GetFileNameWithoutExtension(path);

            xmlFile.Save(path);
            return xmlFile.Element("list");
        }


        private Drone XElementToDroneList()
        {
            throw new NotImplementedException();
        }

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

        public int GetNextID()
        {
            return Int32.Parse(XElement.Load(configPath).Element("nextID").Value);
        }

        public double[] PowerConsumptionRequest()
        {
            XElement powerConsumption = XElement.Load(configPath).Element("powerConsumption");
            double[] ret = {
                Double.Parse(powerConsumption.Element("free").Value),
                Double.Parse(powerConsumption.Element("lightWeight").Value),
                Double.Parse(powerConsumption.Element("midWeight").Value),
                Double.Parse(powerConsumption.Element("heavyWeight").Value),
                Double.Parse(powerConsumption.Element("chargingRate").Value)};
            System.Console.WriteLine("Parsed power consumption");
            return ret;
        }

        public bool VerifyCustomerCredentials(int ID, string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            throw new NotImplementedException();
        }
    }
}
