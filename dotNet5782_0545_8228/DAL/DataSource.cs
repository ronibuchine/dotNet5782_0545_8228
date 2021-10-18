
namespace DalObject
{
    class DataSource
    {
        public static IDAL.DO.Drone[] Drones = new IDAL.DO.Drone[10];
        public static IDAL.DO.DroneStation[] DroneStations = new IDAL.DO.DroneStation[5];
        public static IDAL.DO.Customer[] Customers = new IDAL.DO.Customer[100];
        public static IDAL.DO.Parcel[] Parcels = new IDAL.DO.Parcel[1000];


        internal class Config
        {
            static int NextAvailableDroneIndex;
            static int NextAvailableDroneStationIndex;
            static int NextAvailableCustomerIndex;
            static int NextAvailableParcelsIndex;
            static int PackageCount;
        }

        public static void InitializeArrays()
        {
            // TODO: wtf goes here??
        }

    }
}
