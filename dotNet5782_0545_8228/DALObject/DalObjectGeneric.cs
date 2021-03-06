using System;
using System.Collections.Generic;
using DO;
using DALAPI;
using System.Runtime.CompilerServices;

namespace DAL
{
    /// <summary>
    /// Lazy<T> is by default thread safe, as only one thread can access the
    /// ctor at a time. By the time the next thread accesses it, the
    /// singlton will be already be constructed and the previous intance
    /// will be returned
    /// </summary>
    public sealed partial class DALObject : IDAL
    {
       
        private static readonly Lazy<DALObject> lazy = new Lazy<DALObject>(() => new DALObject());

        public static DALObject Instance { get { return lazy.Value; } }

        public static int nextID;

        private DALObject()

        {
            DataSource.Initialize();
            nextID = DataSource.nextID;
        }


        public int GetNextID() => DataSource.nextID;

        public enum IdalDoType { DRONE, STATION, CUSTOMER, PACKAGE, EMPLOYEE };

        // Adding objects section

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private IEnumerable<T> GetAllItems<T>(List<T> list, Predicate<T> pred) where T : DalEntity
        {
            return list.FindAll(t => t.IsActive && pred(t)).ConvertAll(t => (T)t.Clone());
        }

        /// <summary>
        /// Displays all the items in the array unconditionally
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private IEnumerable<T> GetAllItems<T>(List<T> list) where T : DalEntity => GetAllItems(list, x => true);

        // Displaying one object section

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="ID">The index of which item to display</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private T GetOneItem<T>(List<T> list, int ID) where T : DalEntity => (T)GetActualOneItem<T>(list, ID).Clone();

        /// <summary>
        /// Retrieves an entity from the DataSource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private T GetActualOneItem<T>(List<T> list, int ID) where T : DalEntity
        {
            if (ID == 0)
                return null;
            T ret = list.Find((t) => { return t.ID == ID && t.IsActive; });
            if (ret == null)
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            return ret;
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddDalItem<T>(
                List<T> list,
                IdalDoType type)
            where T : DalEntity
        {
            if (list.Count + 1 <= list.Capacity)
                list.Add((T)DataSource.Insert(type));
            else
                throw new DataSourceException("The entity could not be added to the system.");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddDalItem<T>(
                List<T> list,
                T item)
            where T : DalEntity
        {
            if (list.Count + 1 <= list.Capacity)
                list.Add(item);
            else
                throw new DataSourceException("The entity could not be added to the system.");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] PowerConsumptionRequest()
        {
            double[] ret = {
                DataSource.Config.free,
                DataSource.Config.lightWeight,
                DataSource.Config.midWeight,
                DataSource.Config.heavyWeight,
                DataSource.Config.chargingRate};
            return ret;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Clear()
        {
            DataSource.customers.Clear();
            DataSource.drones.Clear();
            DataSource.stations.Clear();
            DataSource.packages.Clear();
            DataSource.droneCharges.Clear();
            nextID = 1;
        }
    }
}
