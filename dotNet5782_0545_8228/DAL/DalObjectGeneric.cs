using System;
using System.Collections.Generic;

namespace DalObjectNamespace
{
    public partial class DalObject : IDAL.IdalInterface
    {
        private static DalObject dalInstance = null;
        public static int nextID;

        private DalObject() 
        {
            DataSource.Initialize();
            nextID = DataSource.nextID;
        }

        // for debugging/test only
        public DalObject(Object o) 
        {
            nextID = DataSource.nextID;
            dalInstance = this;
        }

        public static DalObject GetInstance()
        {
            if (dalInstance == null)
                dalInstance = new();
            return dalInstance;
        }


        public int GetNextID() => DataSource.nextID;

        public enum IdalDoType { DRONE, STATION, CUSTOMER, PACKAGE };

        // Adding objects section

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        private List<T> GetAllItems<T>(List<T> list, Predicate<T> pred) where T : IDAL.DO.DalEntity
        {
            List<T> newList = new();
            list.FindAll(pred).ForEach(t => newList.Add((T)t.Clone()));
            return newList;
        }

        /// <summary>
        /// Displays all the items in the array unconditionally
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        private List<T> GetAllItems<T>(List<T> list) where T : IDAL.DO.DalEntity => GetAllItems(list, (x) => true);

        // Displaying one object section

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="ID">The index of which item to display</param>
        private T GetOneItem<T>(List<T> list, int ID) where T : IDAL.DO.DalEntity
        {
            T ret = list.Find((t) => { return t.ID == ID; });
            if (ret != null)
                return (T)ret.Clone();
            else
                throw new IDAL.DO.InvalidDalObjectException();
        }
        
        private T GetActualOneItem<T>(List<T> list, int ID) where T : IDAL.DO.DalEntity
        {
            if (ID == 0)
                return null;
            T ret = list.Find((t) => { return t.ID == ID; });
            if (ret != null)
                return (T)ret;
            else
                throw new IDAL.DO.InvalidDalObjectException();
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        private void AddDalItem<T>(
                List<T> list,
                IdalDoType type)
            where T : IDAL.DO.DalEntity
        {
            if (list.Count + 1 > list.Capacity)
                list.Add((T)DataSource.Insert(type));
            else
                throw new IDAL.DO.DataSourceException();
        }

        private void AddDalItem<T>(
                List<T> list,
                T item,
                IdalDoType type)
            where T : IDAL.DO.DalEntity
        {
            if (list.Count + 1 <= list.Capacity)
                list.Add(item);
            else
                throw new IDAL.DO.DataSourceException();
        }

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
    }
}
