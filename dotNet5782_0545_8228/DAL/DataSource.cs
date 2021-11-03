﻿using System;

namespace DalObject
{
    public class DataSource
    {
        internal enum IdalDoType { Drone, DroneStation, Customer, Parcel };

        const int MIN_DRONES = 5;
        const int MIN_DRONE_STATIONS = 2;
        const int MIN_CUSTOMERS = 10;
        const int MIN_PARCELS = 10;
        const int MIN_DRONE_CHARGE = 0;

        const int MAX_DRONES = 10;
        const int MAX_DRONE_STATIONS = 5;
        const int MAX_CUSTOMERS = 100;
        const int MAX_PARCELS = 1000;
        const int MAX_DRONE_CHARGES = MAX_DRONES;


        public IDAL.DO.Drone[] drones = new IDAL.DO.Drone[MAX_DRONES];
        public IDAL.DO.DroneStation[] droneStations = new IDAL.DO.DroneStation[MAX_DRONE_STATIONS];
        public IDAL.DO.Customer[] customers = new IDAL.DO.Customer[MAX_CUSTOMERS];
        public IDAL.DO.Parcel[] parcels = new IDAL.DO.Parcel[MAX_PARCELS];
        public IDAL.DO.DroneCharge[] droneCharges = new IDAL.DO.DroneCharge[MAX_DRONE_CHARGES];

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
            AddDalObject(ref Config.NextAvailableDroneIndex, MAX_DRONES, drones, IdalDoType.Drone);
        public void AddDroneStation() =>
            AddDalObject(ref Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, droneStations, IdalDoType.DroneStation);
        public void AddCustomer() =>
            AddDalObject(ref Config.NextAvailableCustomerIndex, MAX_CUSTOMERS, customers, IdalDoType.Customer);
        public void AddParcel() =>
            AddDalObject(ref Config.NextAvailableParcelIndex, MAX_PARCELS, parcels, IdalDoType.Parcel);

        // Displaying all objects section

        public void DisplayAllDrones() => DisplayAllObjects(drones, Config.NextAvailableDroneIndex);
        public void DisplayAllDroneStations() => DisplayAllObjects(droneStations, Config.NextAvailableDroneStationIndex);
        public void DisplayAllCustomers() => DisplayAllObjects(customers, Config.NextAvailableCustomerIndex);
        public void DisplayAllParcels() => DisplayAllObjects(parcels, Config.NextAvailableParcelIndex);
        public void DisplayAllNotAssignedParcels() =>
            DisplayAllObjects(parcels, Config.NextAvailableParcelIndex, (IDAL.DO.Parcel p) => p.DroneId == 0);
        public void DisplayAllUnoccupiedStations() =>
            DisplayAllObjects(droneStations, Config.NextAvailableDroneStationIndex, (IDAL.DO.DroneStation ds) => ds.ChargeSlots > 0);

        // Displaying one object section

        public void DisplayDrone(int choice) => DisplayOneObject(drones, Config.NextAvailableDroneIndex, choice);
        public void DisplayDroneStation(int choice) => DisplayOneObject(droneStations, Config.NextAvailableDroneStationIndex, choice);
        public void DisplayCustomer(int choice) => DisplayOneObject(customers, Config.NextAvailableCustomerIndex, choice);
        public void DisplayParcel(int choice) => DisplayOneObject(parcels, Config.NextAvailableParcelIndex, choice);

        

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

        private void AddDalObject<T>(
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

        public void DisplayAllObjects<T>(T[] list, int nextAvailableIndex, Func<T, bool> pred) where T : IDAL.DO.DalStruct
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

        public void DisplayAllObjects<T>(T[] list, int nextAvailableIndex) where T : IDAL.DO.DalStruct
        {
            DisplayAllObjects(list, nextAvailableIndex, AlwaysTrue);
        }

        public void DisplayOneObject<T>(T[] list, int nextAvailableIndex, int choice) where T : IDAL.DO.DalStruct
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

        // Update objects section
        
        /// <summary>
        /// Given a package we want the drone that is assigned to it
        /// </summary>
        /// <param name="package">parcel that we would like the drone</param>
        /// <returns>the drone that is assigned to the package</returns>
        private IDAL.DO.Drone GetParcelDrone(IDAL.DO.Parcel package)
        {
            foreach (IDAL.DO.Drone drone in drones) if (drone.ID == package.DroneId) return drone;            
            throw new FieldAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }

        /// <summary>
        /// Takes index of a parcel and assigns to next available drone which can support the parcel weight
        /// Updates the scheduled time.
        /// </summary>
        /// <param name="choice">index of the package to assign</param>
        public void AssignPackageToDrone(int choice) 
        {
            if (choice < 0 || choice > Config.NextAvailableParcelIndex) throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            for (int i = 0; i < Config.NextAvailableDroneIndex; i++)
            {
               
                if (drones[i].MaxWeight >= parcels[choice].Weight && drones[i].Status == IDAL.DO.DroneStatuses.free && drones[i].Battery > 20)
                {
                    parcels[choice].DroneId = drones[i].ID;
                    drones[i].Status = IDAL.DO.DroneStatuses.delivery;
                    parcels[choice].Requested = DateTime.Now;
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
            if (choice < 0 || choice > Config.NextAvailableParcelIndex) throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
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
            if (choice < 0 || choice > Config.NextAvailableParcelIndex) throw new IndexOutOfRangeException("Invalid index, please try again later.\n");
            for (int i = 0; i < Config.NextAvailableDroneIndex; i++)
            {
                if (parcels[choice].DroneId == drones[i].ID) drones[i].Status = IDAL.DO.DroneStatuses.free;
            }            
            parcels[choice].Delivered = DateTime.Now;
        }
        public void SendDroneToCharge(int stationChoice, int droneChoice) 
        {
            if (droneChoice < 0 ||
                droneChoice > Config.NextAvailableDroneIndex ||
                stationChoice < 0 ||
                stationChoice > Config.NextAvailableDroneStationIndex)
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
        public void ReleaseDroneFromCharge(int choice) 
        {

        }

    }
}
