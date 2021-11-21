using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IdalInterface
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        public enum IdalDoType { Drone, DroneStation, Customer, Parcel };
        // Adding objects section

        /// <summary>
        /// Displays all the items in the array that pred returns true on
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="pred">A predicate taking an item of the same type as list, that returns whether or not it should be displayed</param>
        internal List<T> DisplayAllItems<T>(List<T> list, Predicate<T> pred) where T : IDAL.DO.DalEntity => list.FindAll(pred);

        internal bool AlwaysTrue<T>(T dalStruct) where T : IDAL.DO.DalEntity => true;

        /// <summary>
        /// Displays all the items in the array unconditionally
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        internal List<T> DisplayAllItems<T>(List<T> list) where T : IDAL.DO.DalEntity => DisplayAllItems(list, AlwaysTrue);

        // Displaying one object section

        /// <summary>
        /// Displays one item in the list
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="ID">The index of which item to display</param>
        internal T DisplayOneItem<T>(List<T> list, int ID) where T : IDAL.DO.DalEntity
        {
            T ret = list.Find((t) => {return t.ID == ID;});
            if (ret != null)
            {
                return ret;
            }
            else
            {
                throw new IDAL.DO.InvalidDalObjectException();
            }
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        internal void AddDalItem<T>(
                List<T> list,
                IdalDoType type)
            where T : IDAL.DO.DalEntity
        {
            if (list.Count + 1 > list.Capacity)
            {
                list.Add((T)DataSource.IdalDoFactory(list.Count - 1, DataSource.rand, type));
            }
            else
            {
                throw new IDAL.DO.DataSourceException();
            }
        }

        double[] IDAL.IdalInterface.PowerConsumptionRequest()
        {
            throw new NotImplementedException();
        }
    }
}
