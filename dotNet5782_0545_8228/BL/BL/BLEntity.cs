

namespace BL
{
    /// <summary>
    /// This is a parent class for all of the logical entities in the system. The entity contains an ID field which the child classes inherit.
    /// </summary>
    public abstract class BLEntity
    {
        public static int nextID = 1;
        public int ID { get; set; }

        // use when you need a new ID
        protected BLEntity()
        {
            this.ID = nextID++;
        }

        // use when you already have an ID from the system
        protected BLEntity(object o) { }

    }
}

