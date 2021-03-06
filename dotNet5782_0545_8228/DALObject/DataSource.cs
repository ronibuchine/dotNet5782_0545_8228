using System;
using System.Collections.Generic;
using static DAL.DALObject;
using DO;

namespace DAL
{
    internal class DataSource
    {
        // TODO make these numbers bigger... and better
        internal const int MIN_DRONES = 5;

        internal const int MIN_STATIONS = 5;

        internal const int MIN_CUSTOMERS = 5;
        internal const int MIN_PACKAGES = 50;
        internal const int MIN_EMPLOYEES = 1;

        internal const int MAX_DRONES = 20;
        internal const int MAX_STATIONS = 20;
        internal const int MAX_CUSTOMERS = 50;
        internal const int MAX_PACKAGES = 50;
        internal const int MAX_DRONE_CHARGES = MAX_DRONES;
        internal const int MAX_EMPLOYEES = 5;

        internal const string ADMIN_PASSWORD = "admin";

        internal static List<Drone> drones = new List<Drone>(MAX_DRONES);
        internal static List<Station> stations = new List<Station>(MAX_STATIONS);
        internal static List<Customer> customers = new List<Customer>(MAX_CUSTOMERS);
        internal static List<Package> packages = new List<Package>(MAX_PACKAGES);
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>(MAX_DRONE_CHARGES);
        internal static List<Employee> employees = new List<Employee>(MAX_EMPLOYEES);

        public static int nextID { get; set; } = 1;

        private static Random rand;

        internal class Config
        {
            internal static double free = 0;
            internal static double lightWeight = 1d/480;
            internal static double midWeight = 1d/450;
            internal static double heavyWeight = 1d/420;
            internal static double chargingRate = 5; // in % per second
            internal static int packageCount = 0;
        }

        public static void Initialize()
        {
            rand = new Random();
            // employee accounts must be initialized first to recieve ID 1
            InitializeList<Employee>(MIN_EMPLOYEES, MIN_EMPLOYEES, IdalDoType.EMPLOYEE, employees);
            InitializeList<Drone>(MIN_DRONES, MAX_DRONES, IdalDoType.DRONE, drones);
            InitializeList<Station>(MIN_STATIONS, MAX_STATIONS, IdalDoType.STATION, stations);
            InitializeList<Customer>(MIN_CUSTOMERS, MAX_CUSTOMERS, IdalDoType.CUSTOMER, customers);
            InitializeList<Package>(MIN_CUSTOMERS, MAX_CUSTOMERS, IdalDoType.PACKAGE, packages);
        }
        

        /// <summary>
        /// A factory function that returns a new DalStruct based on what type is requested
        /// </summary>
        /// <param name="i">seed integer, generally the index in the array where the struct is stored</param>
        /// <param name="rand">A Random struct</param>
        /// <param name="type">An instance of IdalDoType</param>
        public static DalEntity Insert(IdalDoType type)
        {

            List<int> ids = new();
            packages.ForEach(p => ids.Add(p.droneId));
            List<Drone> unassignedDrones = drones.FindAll(d => !ids.Contains(d.ID));
            switch (type)
            {               
                case IdalDoType.DRONE:
                    return new Drone(nextID++);
                case IdalDoType.STATION:
                    return new Station(nextID++);
                case IdalDoType.CUSTOMER:
                    return new Customer(nextID++);
                case IdalDoType.PACKAGE:
                    int randX = rand.Next(customers.Count);
                    int randY = RandomExceptX(customers.Count, randX, rand);
                    int senderID = customers[randX].ID;
                    int recieverID = customers[randY].ID;
                    int droneID = rand.Next(2) == 0 || unassignedDrones.Count == 0 ? 0 : unassignedDrones[rand.Next(unassignedDrones.Count)].ID;
                    return new Package(nextID++, senderID, recieverID, droneID);
                case IdalDoType.EMPLOYEE:
                    return new Employee(nextID++, ADMIN_PASSWORD);
                default:
                    throw new InvalidDalObjectException();
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
        private static void InitializeList<T>(
                int min,
                int max,
                IdalDoType type,
                List<T> list)
            where T : DalEntity
        {
            int num = rand.Next(min, max + 1);
            for (int i = 0; i < num; ++i)
            {
                list.Add((T)Insert(type));
            }          
        }
    }
}
