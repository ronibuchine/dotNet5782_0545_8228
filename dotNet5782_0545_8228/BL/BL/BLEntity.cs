

namespace BL
{
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

