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
            public static int PackageCount;
        }

        public DataSource()
        {
            this.rand = new Random();
            InitializeList(MIN_DRONES, MAX_DRONES, drones, IdalDoType.Drone, rand);
            InitializeList(MIN_DRONE_STATIONS, MAX_DRONE_STATIONS, droneStations, IdalDoType.DroneStation, rand);
            InitializeList(MIN_CUSTOMERS, MAX_CUSTOMERS, customers, IdalDoType.Customer, rand);
            InitializeList(MIN_PARCELS, MAX_PARCELS, parcels, IdalDoType.Parcel, rand);

           
        }

        // Adding objects section

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        public void DisplayAllItems<T>(
                List<T> list,
                Func<T, bool> pred)
            where T : IDAL.DO.DalStruct
        {
            for (int i = 0; i < list.Count; i++)
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
        /// <param name="list">An array of IdalDoStructs</param>
        public void DisplayAllItems<T>(List<T> list) where T : IDAL.DO.DalStruct
        {
            DisplayAllItems(list, AlwaysTrue);
        }

        public void AddDrone() =>
            AddDalItem(drones, IdalDoType.Drone);
        public void AddDroneStation() =>
            AddDalItem(droneStations, IdalDoType.DroneStation);
        public void AddCustomer() =>
            AddDalItem(customers, IdalDoType.Customer);
        public void AddParcel() =>
            AddDalItem(parcels, IdalDoType.Parcel);

        // Displaying all objects section

        public void DisplayAllDrones() => DisplayAllItems(drones);
        public void DisplayAllDroneStations() => DisplayAllItems(droneStations);
        public void DisplayAllCustomers() => DisplayAllItems(customers);
        public void DisplayAllParcels() => DisplayAllItems(parcels);
        public void DisplayAllNotAssignedParcels() =>
            DisplayAllItems(parcels, (IDAL.DO.Parcel p) => p.DroneId == 0);
        public void DisplayAllUnoccupiedStations() =>
            DisplayAllItems(droneStations, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        // Displaying one object section

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="choice">The index of which item to display</param>
        public void DisplayOneItem<T>(List<T> list, int choice) where T : IDAL.DO.DalStruct
        {
            if (choice >= 0 && choice < list.Count)
            {
                Console.WriteLine(list[choice].ToString());
            }
            else
            {
                Console.WriteLine("Input not valid, please enter a valid index. STUPID.");
            }
        }

        public void DisplayDrone(int choice) => DisplayOneItem(drones, choice);
        public void DisplayDroneStation(int choice) => DisplayOneItem(droneStations, choice);
        public void DisplayCustomer(int choice) => DisplayOneItem(customers, choice);
        public void DisplayParcel(int choice) => DisplayOneItem(parcels, choice);



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
                Random rand)
            where T : IDAL.DO.DalStruct
        {
            int num = rand.Next(min, max + 1);
            for (int i = 0; i < num; ++i)
            {
                list.Add((T)IdalDoFactory(i, rand, type));
            }
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        private void AddDalItem<T>(
                List<T> list,
                IdalDoType type)
            where T : IDAL.DO.DalStruct
        {
            if (list.Count + 1 > list.Capacity)
            {
                list.Add((T)IdalDoFactory(list.Count - 1, this.rand, type));
                Console.WriteLine(String.Format("{0} added sucesfully", Enum.GetName(typeof(IdalDoType), (int)type)));
            }


            else
            {
                Console.WriteLine(String.Format("Max {0} already", Enum.GetName(typeof(IdalDoType), (int)type)));
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
                if (choice < 0 || choice > drones.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
                }
                for (int i = 0; i < drones.Count; i++)
                {

                    if (drones[i].MaxWeight >= parcels[choice].Weight)
                    {
                        parcels[choice].DroneId = drones[i].ID;
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
                if (choice < 0 || choice > parcels.Count)
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

                if (choice < 0 || choice > parcels.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
                }
                for (int i = 0; i < drones.Count; i++)
                {
                    if (parcels[choice].DroneId == drones[i].ID) drones[i].Status = IDAL.DO.DroneStatuses.free;
                }
                parcels[choice].Delivered = DateTime.Now;
            }

            public void SendDroneToCharge(int stationChoice, int droneChoice)
            {

                if (droneChoice < 0 ||
                    droneChoice > drones.Count ||
                    stationChoice < 0 ||
                    stationChoice > droneStations.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
                }
                if (drones[droneChoice].Battery != 100 && droneStations[stationChoice].ChargeSlots > 0)
                {
                    droneCharges.Add(new IDAL.DO.DroneCharge(drones[droneChoice].ID, droneStations[stationChoice].ID));
                    drones[droneChoice].Status = IDAL.DO.DroneStatuses.maintenance;
                    droneStations[stationChoice].ChargeSlots--;
                }
            }

            public void ReleaseDroneFromCharge(int stationChoice, int droneChoice)
            {
                if (droneChoice < 0 ||
                   droneChoice > drones.Count ||
                   stationChoice < 0 ||
                   stationChoice > droneStations.Count)
                {
                    throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
                }
                for (int i = 0; i < droneCharges.Count; i++)
                {
                    if (droneCharges[i].DroneId == drones[droneChoice].ID &&
                            droneCharges[i].StationId == droneStations[stationChoice].ID)
                    {
                        drones[droneChoice].Status = IDAL.DO.DroneStatuses.free;
                        droneStations[stationChoice].ChargeSlots++;
                    }
                }
            }
        }
    } 
}
