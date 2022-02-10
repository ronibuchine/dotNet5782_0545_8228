using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALObject : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int ID) => GetActualCustomer(ID).IsActive = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int ID) => GetActualDrone(ID).IsActive = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeletePackage(int ID) => GetActualPackage(ID).IsActive = false;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(int ID) => GetActualStation(ID).IsActive = false;


        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteEmployee(int ID) => GetActualEmployee(ID).IsActive = false;
    }
}
