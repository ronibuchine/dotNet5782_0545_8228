namespace IDAL
{
    namespace DO
    {
        public abstract class DalEntity
        {
            protected static int nextID = 1;
            public int ID { get; set; }

            protected DalEntity()
            {
                this.ID = nextID++;
            }

            public abstract object Clone();
        }

    }
}
