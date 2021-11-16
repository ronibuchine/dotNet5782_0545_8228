using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class ABCDalObject : IDAL.IdalInterface
    {
        public ABCDalObject()
        {
            DataSource.Initialize();
        }

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
        /// <param name="choice">The index of which item to display</param>
        internal List<T> DisplayOneItem<T>(List<T> list, int choice) where T : IDAL.DO.DalEntity
        {
            /* if (choice < 0 || choice >= list.Count) */
            /* { */
            /*     throw new IDAL.DO.DalObjectAccessException("The requested object does not exist"); */
            /* } */
            return new List<T> { list[choice] };
        }

        /// <summary>
        /// Adds a new IdalDoStruct to the array given
        /// </summary>
        /// <param name="list">An array of IdalDoStructs</param>
        /// <param name="rand">A Random object</param>
        internal void AddDalItem<T>(
                List<T> list,
                DataSource.IdalDoType type)
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
