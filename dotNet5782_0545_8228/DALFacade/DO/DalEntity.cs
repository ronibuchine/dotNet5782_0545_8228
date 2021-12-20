
namespace DO
{
    public abstract class DalEntity
    {
        public int ID { get; set; }

        protected DalEntity(int ID)
        {
            this.ID = ID;
        }

        public abstract object Clone();
    }

}

