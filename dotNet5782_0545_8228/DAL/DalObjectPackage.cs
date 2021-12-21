using System.Collections.Generic;
using DO;
using DALAPI;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        // Update objects section

        /// <summary>
        /// Given a package we want the drone that is assigned to it
        /// </summary>
        /// <param name="package">parcel that we would like the drone</param>
        /// <returns>the drone that is assigned to the package</returns>
        internal Drone GetParcelDrone(Package package)
        {
            foreach (Drone drone in DataSource.drones)
            {
                if (drone.ID == package.droneId)
                    return drone;
            }
            throw new DalObjectAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }
        
        public void AddPackage() => AddDalItem(DataSource.packages, IdalDoType.PACKAGE);

        public void AddPackage(Package package) =>
            AddDalItem(DataSource.packages, package, IdalDoType.PACKAGE);

        public IEnumerable<Package> GetAllPackages() => GetAllItems(DataSource.packages);

        public IEnumerable<Package> GetAllNotAssignedPackages() =>
            GetAllItems(DataSource.packages, (Package p) => p.droneId == 0);

        public Package GetPackage(int ID) => GetOneItem(DataSource.packages, ID);

        public Package GetActualPackage(int ID) => GetActualOneItem(DataSource.packages, ID);
    }
}
