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

        static private WeightCategories ParseWeightCategory(string weight) =>
            weight switch
            {
                "heavy" => WeightCategories.heavy,
                "medium" => WeightCategories.medium,
                "light" => WeightCategories.light,
                _ => throw new Exception("not valid weight")
            };

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
            nextID = Initialize();
        }

        private int Initialize()
        {
            GetFile("drones");
            var rand = new Random();
        }

        private XElement GetFile(string filename)
        {
            var path = Directory.GetCurrentDirectory();
            var containing = Directory.GetFiles(path, "*.sln");
            while (containing.Length == 0)
            {
                path = Directory.GetParent(path).FullName;
                containing = Directory.GetFiles(path, "*.sln");
            }

            XElement dalConfig = XElement.Load(path + $"/data/{filename}.xml");
            return dalConfig.Element("list");

        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int GetNextID()
        {
            throw new NotImplementedException();
        }

        public double[] PowerConsumptionRequest()
        {
            throw new NotImplementedException();
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
