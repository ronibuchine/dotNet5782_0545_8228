using System;
using System.Collections.Generic;
using DO;
using DALAPI;
using System.Linq;

namespace DAL
{
    public partial class DalObject : IDAL
    {

        private static DalObject _instance; 
        public static DalObject Instance 
        {
            get 
            {
                if (_instance == null)
                    _instance = new();
                return _instance;
            }
            set { }
        }

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
            Instance = this;
        }

        public static DalObject GetInstance()
        {
            if (Instance == null)
                Instance = new();
            return Instance;
        }


        public int GetNextID() => DataSource.nextID;

        public enum IdalDoType { DRONE, STATION, CUSTOMER, PACKAGE };

        // Adding objects section

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        private IEnumerable<T> GetAllItems<T>(List<T> list, Predicate<T> pred) where T : DalEntity
        {
            return list.FindAll(pred).ConvertAll(t => (T)t.Clone());
        }

        /// <summary>
        /// Displays all the items in the array unconditionally
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        private IEnumerable<T> GetAllItems<T>(List<T> list) where T : DalEntity => GetAllItems(list, x => true);

        // Displaying one object section

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="ID">The index of which item to display</param>
        private T GetOneItem<T>(List<T> list, int ID) where T : DalEntity
        {
            T ret = list.First((t) => { return t.ID == ID; });
            if (ret != null)
                return (T)ret.Clone();
            else
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
        }
        
        private T GetActualOneItem<T>(List<T> list, int ID) where T : DalEntity
        {
            if (ID == 0)
                return null;
            T ret = list.First((t) => { return t.ID == ID; });
            if (ret != null)
                return (T)ret;
            else
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
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

        private void AddDalItem<T>(
                List<T> list,
                T item,
                IdalDoType type)
            where T : DalEntity
        {
            if (list.Count + 1 <= list.Capacity)
                list.Add(item);
            else
                throw new DataSourceException("The entity could not be added to the system.");
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
