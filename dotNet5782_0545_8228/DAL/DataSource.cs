using System;
using System.Collections.Generic;
using static DalObjectNamespace.DalObject;

namespace DalObjectNamespace
{
    internal class DataSource
    {
        private const int MIN_DRONES = 2;
        private const int MIN_DRONE_STATIONS = 2;
        private const int MIN_CUSTOMERS = 2;
        private const int MIN_PACKAGES = 2;

        private const int MAX_DRONES = 5;
        private const int MAX_DRONE_STATIONS = 5;
        private const int MAX_CUSTOMERS = 5;
        private const int MAX_PACKAGES = 5;
        private const int MAX_DRONE_CHARGES = MAX_DRONES;

        public static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>(MAX_DRONES);
        public static List<IDAL.DO.Station> stations = new List<IDAL.DO.Station>(MAX_DRONE_STATIONS);
        public static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>(MAX_CUSTOMERS);
        public static List<IDAL.DO.Package> packages = new List<IDAL.DO.Package>(MAX_PACKAGES);
        public static List<IDAL.DO.DroneCharge> droneCharges = new List<IDAL.DO.DroneCharge>(MAX_DRONE_CHARGES);

        public static int nextID { get; set; } = 1;

        private static Random rand;

        internal class Config
        {
            internal static double free = 0;
            internal static double lightWeight = 150;
            internal static double midWeight = 100;
            internal static double heavyWeight = 75;
            internal static double chargingRate = 50; // in % per hour
            internal static int packageCount = 0;
        }

        public static void Initialize()
        {
            rand = new Random();
            drones = InitializeList<IDAL.DO.Drone>(MIN_DRONES, MAX_DRONES, IdalDoType.DRONE);
            stations = InitializeList<IDAL.DO.Station>(MIN_DRONE_STATIONS, MAX_DRONE_STATIONS, IdalDoType.STATION);
            customers = InitializeList<IDAL.DO.Customer>(MIN_CUSTOMERS, MAX_CUSTOMERS, IdalDoType.CUSTOMER);
            packages = InitializeList<IDAL.DO.Package>(MIN_CUSTOMERS, MAX_CUSTOMERS, IdalDoType.PACKAGE);
        }

        /// <summary>
        /// A factory function that returns a new DalStruct based on what type is requested
        /// </summary>
        /// <param name="i">seed integer, generally the index in the array where the struct is stored</param>
        /// <param name="rand">A Random struct</param>
        /// <param name="type">An instance of IdalDoType</param>
        public static IDAL.DO.DalEntity Insert(IdalDoType type)
        {
            switch (type)
            {
                case IdalDoType.DRONE:
                    return new IDAL.DO.Drone(nextID++);
                case IdalDoType.STATION:
                    return new IDAL.DO.Station(nextID++);
                case IdalDoType.CUSTOMER:
                    return new IDAL.DO.Customer(nextID++);
                case IdalDoType.PACKAGE:
                    int randX = rand.Next(customers.Count);
                    int randY = RandomExceptX(customers.Count, randX, rand);
                    int senderID = customers[randX].ID;
                    int recieverID = customers[randY].ID;
                    int droneID = drones[rand.Next(drones.Count)].ID;
                    return new IDAL.DO.Package(nextID++, senderID, recieverID, droneID);
                default:
                    throw new IDAL.DO.InvalidDalObjectException();
            }
        }

        // n > 1 
        // 0 <= x < n
        private static int RandomExceptX(int n, int x, Random rand) 
        {
            int result = rand.Next(n);
            if (result != x)
                return result;
            return (result + 1) % n;
        }

        /// <summary>
        /// Initializes an array of IdalDoStructs
        /// </summary>
        /// <param name="min">Minimum number of items allowed</param>
        /// <param name="max">Maximum number of items allowed</param>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="nextAvailableIndex">Reference to the next available index in the array</param>
        /// <param name="rand">A Random object</param>
        private static List<T> InitializeList<T>(
                int min,
                int max,
                IdalDoType type)
            where T : IDAL.DO.DalEntity
        {
            List<T> list = new();
            int num = rand.Next(min, max + 1);
            for (int i = 0; i < num; ++i)
            {
                list.Add((T)Insert(type));
            }
            return list;
        }



    }
}
