using System.Collections.Generic;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {

        // Update objects section

        /// <summary>
        /// Given a package we want the drone that is assigned to it
        /// </summary>
        /// <param name="package">parcel that we would like the drone</param>
        /// <returns>the drone that is assigned to the package</returns>
        internal IDAL.DO.Drone GetParcelDrone(IDAL.DO.Package package)
        {
            foreach (IDAL.DO.Drone drone in DataSource.drones)
            {
                if (drone.ID == package.droneId)
                    return drone;
            }
            throw new IDAL.DO.DalObjectAccessException($"There is no drone assigned to this package: {package.ID}\n");
        }
        
        public void AddPackage() => AddDalItem(DataSource.packages, IdalDoType.PACKAGE);

        public void AddPackage(IDAL.DO.Package package) =>
            AddDalItem(DataSource.packages, package, IdalDoType.PACKAGE);

        public List<IDAL.DO.Package> GetAllPackages() => GetAllItems(DataSource.packages);

        public List<IDAL.DO.Package> GetAllNotAssignedPackages() =>
            GetAllItems(DataSource.packages, (IDAL.DO.Package p) => p.droneId == 0);

        public IDAL.DO.Package GetPackage(int ID) => GetOneItem(DataSource.packages, ID);

        public IDAL.DO.Package GetActualPackage(int ID) => GetActualOneItem(DataSource.packages, ID);
    }
}
