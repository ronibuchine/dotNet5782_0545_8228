using System;

namespace DalObject
{
    public class DataSource
    {
        internal enum IdalDoType { Drone, DroneStation, Customer, Parcel };

        const int MIN_DRONES = 5;
        const int MIN_DRONE_STATIONS = 2;
        const int MIN_CUSTOMERS = 10;
        const int MIN_PARCELS = 10;

        const int MAX_DRONES = 10;
        const int MAX_DRONE_STATIONS = 5;
        const int MAX_CUSTOMERS = 100;
        const int MAX_PARCELS = 1000;


        public IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[MAX_DRONES];
        public IDAL.DO.DroneStation[] DroneStations = new IDAL.DO.DroneStation[MAX_DRONE_STATIONS];
        public IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[MAX_CUSTOMERS];
        public IDAL.DO.Parcel[] Parcels = new IDAL.DO.Parcel[MAX_PARCELS];

        internal Random rand;

        internal class Config
        {
            public static int NextAvailableDroneIndex;
            public static int NextAvailableDroneStationIndex;
            public static int NextAvailableCustomerIndex;
            public static int NextAvailableParcelsIndex;
            public static int PackageCount;

            public static int SetNextAvailable(int num, int max) => (num < max) ? num : -1;
        }

        public DataSource()
        {
            this.rand = new Random();
            InitializeList(MIN_DRONES, MAX_DRONES, Drones, IdalDoType.Drone, ref Config.NextAvailableDroneIndex, rand);
            InitializeList(MIN_DRONE_STATIONS, MAX_DRONE_STATIONS, DroneStations, IdalDoType.DroneStation, ref Config.NextAvailableDroneStationIndex, rand);
            InitializeList(MIN_CUSTOMERS, MAX_CUSTOMERS, Customers, IdalDoType.Customer, ref Config.NextAvailableCustomerIndex, rand);
            InitializeList(MIN_PARCELS, MAX_PARCELS, Parcels, IdalDoType.Parcel, ref Config.NextAvailableParcelsIndex, rand);

            // That nice generic stuff instead of the following 4 times in a row
            /* int numDrone = rand.Next(2, MAX_DRONES + 1); */
            /* for (int i = 0; i < numDrone; ++i) */
            /* { */
            /*     Drones[i] = new IDAL.DO.Drone(i, rand); */
            /* } */
            /* Config.NextAvailableDroneIndex = Config.SetNextAvailable(numDrone, MAX_DRONE_STATIONS); */
        }

        public void AddDrone() =>
            AddDalItems(ref Config.NextAvailableDroneIndex, MAX_DRONES, Drones, IdalDoType.Drone);
        public void AddDroneStation() =>
            AddDalItems(ref Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, DroneStations, IdalDoType.DroneStation);
        public void AddCustomer() =>
            AddDalItems(ref Config.NextAvailableCustomerIndex, MAX_CUSTOMERS, Customers, IdalDoType.Customer);
        public void AddParcel() =>
            AddDalItems(ref Config.NextAvailableParcelsIndex, MAX_PARCELS, Parcels, IdalDoType.Parcel);

        public void DisplayAllDrones() => DisplayAllItems(Drones, Config.NextAvailableDroneIndex);
        public void DisplayAllDroneStations() => DisplayAllItems(DroneStations, Config.NextAvailableDroneStationIndex);
        public void DisplayAllCustomers() => DisplayAllItems(Customers, Config.NextAvailableCustomerIndex);
        public void DisplayAllParcels() => DisplayAllItems(Parcels, Config.NextAvailableParcelsIndex);
        public void DisplayAllNotAssignedParcels() =>
            DisplayAllItems(Parcels, Config.NextAvailableParcelsIndex, (IDAL.DO.Parcel p) => p.DroneId == 0);
        public void DisplayAllUnoccupiedStations() =>
            DisplayAllItems(DroneStations, Config.NextAvailableDroneStationIndex, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        public void DisplayDrone(int choice) => DisplayOneItem(Drones, Config.NextAvailableDroneIndex, choice);
        public void DisplayDroneStation(int choice) => DisplayOneItem(DroneStations, Config.NextAvailableDroneStationIndex, choice);
        public void DisplayCustomer(int choice) => DisplayOneItem(Customers, Config.NextAvailableCustomerIndex, choice);
        public void DisplayParcel(int choice) => DisplayOneItem(Parcels, Config.NextAvailableParcelsIndex, choice);



        /// <summary>
        /// A factory function that returns a new DalStruct based on what type is requested
        /// </summary>
        /// <param name="i">seed integer, generally the index in the array where the struct is stored</param>
        /// <param name="rand">A Random struct</param>
        /// <param name="type">An instance of IdalDoType</param>
        internal static IDAL.DO.DalStruct IdalDoFactory(int i, Random rand, IdalDoType type)
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
                    throw new NotSupportedException();
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
        internal static void InitializeList<T>(
                int min,
                int max,
                T[] list,
                IdalDoType type,
                ref int nextAvailableIndex,
                Random rand)
            where T : IDAL.DO.DalStruct
        {
            int num = rand.Next(min, max + 1);
            for (int i = 0; i < num; ++i)
            {
                list[i] = (T)IdalDoFactory(i, rand, type);
            }
            nextAvailableIndex = Config.SetNextAvailable(num, max);
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="nextAvailableIndex">Reference to the next available index in the array</param>
        /// <param name="max">Maximum number of items allowed</param>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        private void AddDalItems<T>(
                ref int nextAvailableIndex,
                int max,
                T[] list,
                IdalDoType type)
            where T : IDAL.DO.DalStruct
        {
            if (nextAvailableIndex != -1)
            {
                list[nextAvailableIndex] = (T)IdalDoFactory(nextAvailableIndex, this.rand, type);
                nextAvailableIndex = Config.SetNextAvailable(nextAvailableIndex + 1, max);
                Console.WriteLine(String.Format("{0} added sucesfully", Enum.GetName(typeof(IdalDoType), (int)type)));
            }
            else
            {
                Console.WriteLine(String.Format("Max {0} already", Enum.GetName(typeof(IdalDoType), (int)type)));
            }
        }

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="nextAvailableIndex">The next available index in the array</param>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        public void DisplayAllItems<T>(T[] list, int nextAvailableIndex, Func<T, bool> pred) where T : IDAL.DO.DalStruct
        {
            for (int i = 0; i < nextAvailableIndex; i++)
            {
                if (pred(list[i]))
                {
                    Console.WriteLine(String.Format("{0}: {1}", i, list[i].ToString()));
                }
            }
        }

        private bool AlwaysTrue<T>(T dalStruct) where T : IDAL.DO.DalStruct => true;

        /// <summary>
        /// Displays all the items in the array unconditionally
        /// </summary>
        /// <param name="nextAvailableIndex">The next available index in the array</param>
        /// <param name="list">An array of IdalDoStructs</param>
        public void DisplayAllItems<T>(T[] list, int nextAvailableIndex) where T : IDAL.DO.DalStruct
        {
            DisplayAllItems(list, nextAvailableIndex, AlwaysTrue);
        }

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="nextAvailableIndex">The next available index in the array</param>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="choice">The index of which item to display</param>
        public void DisplayOneItem<T>(T[] list, int nextAvailableIndex, int choice) where T : IDAL.DO.DalStruct
        {
            if (choice >= 0 && choice < nextAvailableIndex)
            {
                Console.WriteLine(list[choice].ToString());
            }
            else
            {
                Console.WriteLine("Input not valid, please enter a valid index. STUPID.");
            }
        }

    }
}
