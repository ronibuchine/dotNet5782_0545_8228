using System;

namespace DalObject
{
    public class DataSource
    {
        internal enum IdalDoType { Drone, DroneStation, Customer, Parcel};

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

        public void AddDrone() => 
            AddDalObject(ref Config.NextAvailableDroneIndex, MAX_DRONES, Drones, IdalDoType.Drone);

        public void AddDroneStation() => 
            AddDalObject(ref Config.NextAvailableDroneStationIndex, MAX_DRONE_STATIONS, DroneStations, IdalDoType.DroneStation);

        public void AddCustomer() => 
            AddDalObject(ref Config.NextAvailableCustomerIndex, MAX_CUSTOMERS, Customers, IdalDoType.Customer);

        public void AddParcel() => 
            AddDalObject(ref Config.NextAvailableParcelsIndex, MAX_PARCELS, Parcels, IdalDoType.Parcel);

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
    }
}
