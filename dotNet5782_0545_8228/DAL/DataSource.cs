using System;
using System.Collections.Generic;

namespace DalObject
{
    public class DataSource
    {
        internal enum IdalDoType { Drone, DroneStation, Customer, Parcel };

        const int MIN_DRONES = 5;
        const int MIN_DRONE_STATIONS = 2;
        const int MIN_CUSTOMERS = 10;
        const int MIN_PARCELS = 2;
        const int MIN_DRONE_CHARGE = 0;

        const int MAX_DRONES = 10;
        const int MAX_DRONE_STATIONS = 5;
        const int MAX_CUSTOMERS = 100;
        const int MAX_PARCELS = 10;
        const int MAX_DRONE_CHARGES = MAX_DRONES;


        public List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>(MAX_DRONES);
        public List<IDAL.DO.DroneStation> droneStations = new List<IDAL.DO.DroneStation>(MAX_DRONE_STATIONS);
        public List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>(MAX_CUSTOMERS);
        public List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>(MAX_PARCELS);
        public List<IDAL.DO.DroneCharge> droneCharges = new List<IDAL.DO.DroneCharge>(MAX_DRONE_CHARGES);

        internal Random rand;

        internal class Config
        {
            public static int NextAvailableDroneIndex;
            public static int NextAvailableDroneStationIndex;
            public static int NextAvailableCustomerIndex;
            public static int NextAvailableParcelIndex;
            public static int NextAvailableDroneChargeIndex;
            public static int PackageCount;

            public static int SetNextAvailable(int num, int max) => (num < max) ? num : -1;
        }

        public DataSource()
        {
            this.rand = new Random();
            InitializeList(MIN_DRONES, MAX_DRONES, drones, IdalDoType.Drone, ref Config.NextAvailableDroneIndex, rand);
            InitializeList(MIN_DRONE_STATIONS, MAX_DRONE_STATIONS, droneStations, IdalDoType.DroneStation, ref Config.NextAvailableDroneStationIndex, rand);
            InitializeList(MIN_CUSTOMERS, MAX_CUSTOMERS, customers, IdalDoType.Customer, ref Config.NextAvailableCustomerIndex, rand);
            InitializeList(MIN_PARCELS, MAX_PARCELS, parcels, IdalDoType.Parcel, ref Config.NextAvailableParcelIndex, rand);

            // That nice generic stuff instead of the following 4 times in a row
            /* int numDrone = rand.Next(2, MAX_DRONES + 1); */
            /* for (int i = 0; i < numDrone; ++i) */
            /* { */
            /*     Drones[i] = new IDAL.DO.Drone(i, rand); */
            /* } */
            /* Config.NextAvailableDroneIndex = Config.SetNextAvailable(numDrone, MAX_DRONE_STATIONS); */
        }

        // Adding objects section
        public void AddDrone() =>
            AddDalItem(ref Config.NextAvailableDroneIndex, MAX_DRONES, drones, IdalDoType.Drone);
        public void AddDroneStation() =>
            AddDalItem(ref Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, droneStations, IdalDoType.DroneStation);
        public void AddCustomer() =>
            AddDalItem(ref Config.NextAvailableCustomerIndex, MAX_CUSTOMERS, customers, IdalDoType.Customer);
        public void AddParcel() =>
            AddDalItem(ref Config.NextAvailableParcelIndex, MAX_PARCELS, parcels, IdalDoType.Parcel);

        // Displaying all objects section

        public void DisplayAllDrones() => DisplayAllItems(drones, Config.NextAvailableDroneIndex, MAX_DRONES);
        public void DisplayAllDroneStations() => DisplayAllItems(droneStations, Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS);
        public void DisplayAllCustomers() => DisplayAllItems(customers, Config.NextAvailableCustomerIndex, MAX_CUSTOMERS);
        public void DisplayAllParcels() => DisplayAllItems(parcels, Config.NextAvailableParcelIndex, MAX_PARCELS);
        public void DisplayAllNotAssignedParcels() =>
            DisplayAllItems(parcels, Config.NextAvailableParcelIndex, MAX_PARCELS, (IDAL.DO.Parcel p) => p.DroneId == 0);
        public void DisplayAllUnoccupiedStations() =>
            DisplayAllItems(droneStations, Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        // Displaying one object section

        public void DisplayDrone(int choice) => DisplayOneItem(drones, Config.NextAvailableDroneIndex, MAX_DRONES, choice);
        public void DisplayDroneStation(int choice) => DisplayOneItem(droneStations, Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, choice);
        public void DisplayCustomer(int choice) => DisplayOneItem(customers, Config.NextAvailableCustomerIndex, MAX_CUSTOMERS, choice);
        public void DisplayParcel(int choice) => DisplayOneItem(parcels, Config.NextAvailableParcelIndex, MAX_PARCELS, choice);



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
                List<T> list,
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
        private void AddDalItem<T>(
                ref int nextAvailableIndex,
                int max,
                List<T> list,
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
        public void DisplayAllItems<T>(
                List<T> list,
                int nextAvailableIndex,
                int max,
                Func<T, bool> pred)
            where T : IDAL.DO.DalStruct
        {
            nextAvailableIndex = (nextAvailableIndex != -1) ? nextAvailableIndex : max;
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
        public void DisplayAllItems<T>(List<T> list, int nextAvailableIndex, int max) where T : IDAL.DO.DalStruct
        {
            DisplayAllItems(list, nextAvailableIndex, max, AlwaysTrue);
        }

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="nextAvailableIndex">The next available index in the array</param>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="choice">The index of which item to display</param>
        public void DisplayOneItem<T>(List<T> list, int nextAvailableIndex, int max, int choice) where T : IDAL.DO.DalStruct
        {
            nextAvailableIndex = (nextAvailableIndex != -1) ? nextAvailableIndex : max;
            if (choice >= 0 && choice < nextAvailableIndex)
            {
                Console.WriteLine(list[choice].ToString());
            }
            else
            {
                Console.WriteLine("Input not valid, please enter a valid index. STUPID.");
            }
        }

        // Update objects section

        /// <summary>
        /// Given a package we want the drone that is assigned to it
        /// </summary>
        /// <param name="package">parcel that we would like the drone</param>
        /// <returns>the drone that is assigned to the package</returns>
        private IDAL.DO.Drone GetParcelDrone(IDAL.DO.Parcel package)
        {
            foreach (IDAL.DO.Drone drone in drones)
            {
                if (drone.ID == package.DroneId) 
                {
                    return drone;
                }
            }
            throw new FieldAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }

        /// <summary>
        /// Takes index of a parcel and assigns to next available drone which can support the parcel weight
        /// Updates the scheduled time.
        /// </summary>
        /// <param name="choice">index of the package to assign</param>
        public void AssignPackageToDrone(int choice)
        {
            int amount = (Config.NextAvailableParcelIndex != -1) ? Config.NextAvailableParcelIndex : MAX_PARCELS;
            if (choice < 0 || choice > amount)
            {
                throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < Config.NextAvailableDroneIndex; i++)
            {

                if (drones[i].MaxWeight >= parcels[choice].Weight &&
                    drones[i].Status == IDAL.DO.DroneStatuses.free &&
                    drones[i].Battery > 20)
                {
                    parcels[choice].DroneId = drones[i].ID;
                    drones[i].Status = IDAL.DO.DroneStatuses.delivery;
                    parcels[choice].Scheduled = DateTime.Now;
                    return;
                }
            }
            throw new ApplicationException("Error, no available drones. Try again later.\n");
        }

        /// <summary>
        /// Updates the pickup time for the package after checking to make sure the parcel is assigned to a drone
        /// </summary>
        /// <param name="choice">index of the parcel</param>
        public void CollectPackageFromDrone(int choice)
        {
            int amount = (Config.NextAvailableParcelIndex != -1) ? Config.NextAvailableParcelIndex : MAX_PARCELS;
            if (choice < 0 || choice > amount)
            { 
                throw new IndexOutOfRangeException("Invalid index, please try again later.\n"); 
            }
            IDAL.DO.Drone currentDrone = GetParcelDrone(parcels[choice]);
            if (currentDrone.Status == IDAL.DO.DroneStatuses.delivery)
            {
                parcels[choice].PickedUp = DateTime.Now;
                return;
            }
            throw new ApplicationException("Error, you must first assign the Drone before it can collect a package.\n");
        }

        public void ProvidePackageToCustomer(int choice)
        {
            int amount = (Config.NextAvailableParcelIndex != -1) ? Config.NextAvailableParcelIndex : MAX_PARCELS;
            if (choice < 0 || choice > amount) 
            {
                throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < Config.NextAvailableDroneIndex; i++)
            {
                if (parcels[choice].DroneId == drones[i].ID) drones[i].Status = IDAL.DO.DroneStatuses.free;
            }
            parcels[choice].Delivered = DateTime.Now;
        }

        public void SendDroneToCharge(int stationChoice, int droneChoice)
        {
            int droneAmount = (Config.NextAvailableDroneIndex != -1) ? Config.NextAvailableDroneIndex : MAX_DRONES;
            int stationAmount = (Config.NextAvailableDroneStationIndex != -1) ? Config.NextAvailableDroneStationIndex : MAX_DRONE_STATIONS;
            if (droneChoice < 0 ||
                droneChoice > droneAmount ||
                stationChoice < 0 ||
                stationChoice > stationAmount)
            {
                throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            }
            if (drones[droneChoice].Battery != 100 && droneStations[stationChoice].ChargeSlots > 0)
            {
                droneCharges[Config.NextAvailableDroneChargeIndex] = new IDAL.DO.DroneCharge(drones[droneChoice].ID, droneStations[stationChoice].ID);
                drones[droneChoice].Status = IDAL.DO.DroneStatuses.maintenance;
                Config.NextAvailableDroneChargeIndex = Config.SetNextAvailable(Config.NextAvailableDroneChargeIndex + 1, MAX_DRONE_CHARGES);
                droneStations[stationChoice].ChargeSlots--;
            }
        }

        public void ReleaseDroneFromCharge(int stationChoice, int droneChoice)
        {
            int droneAmount = (Config.NextAvailableDroneIndex != -1) ? Config.NextAvailableDroneIndex : MAX_DRONES;
            int stationAmount = (Config.NextAvailableDroneStationIndex != -1) ? Config.NextAvailableDroneStationIndex : MAX_DRONE_STATIONS;
            if (droneChoice < 0 ||
               droneChoice > droneAmount ||
               stationChoice < 0 ||
               stationChoice > stationAmount)
            {
                throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            }
            for (int i = 0; i < droneCharges.Count; i++)
            {
                if (droneCharges[i].DroneId == drones[droneChoice].ID &&
                        droneCharges[i].StationId == droneStations[stationChoice].ID)
                {
                    drones[droneChoice].Status = IDAL.DO.DroneStatuses.free;
                    Config.NextAvailableDroneChargeIndex = Config.SetNextAvailable(Config.NextAvailableDroneChargeIndex - 1, MAX_DRONE_CHARGES);
                    droneStations[stationChoice].ChargeSlots++;
                }
            }
        }
    }
}
