using System;
using System.Collections.Generic;
using IDAL;

namespace DalObject
{
    public class DataSource
    {
        

        const int MIN_DRONES = 5;
        const int MIN_DRONE_STATIONS = 2;
        const int MIN_CUSTOMERS = 10;
        const int MIN_PARCELS = 2;

        const int MAX_DRONES = 10;
        const int MAX_DRONE_STATIONS = 5;
        const int MAX_CUSTOMERS = 100;
        const int MAX_PARCELS = 10;
        const int MAX_DRONE_CHARGES = MAX_DRONES;


        public static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>(MAX_DRONES);
        public static List<IDAL.DO.DroneStation> droneStations = new List<IDAL.DO.DroneStation>(MAX_DRONE_STATIONS);
        public static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>(MAX_CUSTOMERS);
        public static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>(MAX_PARCELS);
        public static List<IDAL.DO.DroneCharge> droneCharges = new List<IDAL.DO.DroneCharge>(MAX_DRONE_CHARGES);

        internal static Random rand;

        internal class Config
        {
            public static double free;
            public static double lightWeight;
            public static double midWeight;
            public static double heavyWeight;
            public static double chargingRate; // in % per hour
            public static int packageCount; 
        }

        public static void Initialize()
        {
            rand = new Random();
            InitializeList(MIN_DRONES, MAX_DRONES, drones, IdalDoType.Drone, rand);
            InitializeList(MIN_DRONE_STATIONS, MAX_DRONE_STATIONS, droneStations, IdalDoType.DroneStation, rand);
            InitializeList(MIN_CUSTOMERS, MAX_CUSTOMERS, customers, IdalDoType.Customer, rand);
            InitializeList(MIN_PARCELS, MAX_PARCELS, parcels, IdalDoType.Parcel, rand);
        }

        /// <summary>
        /// A factory function that returns a new DalStruct based on what type is requested
        /// </summary>
        /// <param name="i">seed integer, generally the index in the array where the struct is stored</param>
        /// <param name="rand">A Random struct</param>
        /// <param name="type">An instance of IdalDoType</param>
        public static IDAL.DO.DalEntity IdalDoFactory(int i, Random rand, IdalDoType type)
        {
            switch (type)
            {
                case IdalDoType.Drone:
                    return new IDAL.DO.Drone(i, rand);
                case IdalDoType.DroneStation:
                    return new IDAL.DO.DroneStation(i, rand);
                case IdalDoType.Customer:
                    return new IDAL.DO.Customer(i, rand);
                case IdalDoType.Parcel:
                    return new IDAL.DO.Parcel(i, rand);
                default:
                    throw new IDAL.DO.InvalidDalObjectException();
            }
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
                List<T> list,
                IdalDoType type,
                Random rand)
            where T : IDAL.DO.DalEntity
        {
            int num = rand.Next(min, max + 1);
            for (int i = 0; i < num; ++i)
            {
                list.Add((T)IdalDoFactory(i, rand, type));
            }
        }

       
       
    }
}
