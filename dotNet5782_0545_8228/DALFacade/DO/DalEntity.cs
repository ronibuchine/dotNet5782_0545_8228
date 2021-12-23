
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DALObject")]
/* [assembly: InternalsVisibleTo("DALXml")] */
namespace DO
{
    public abstract class DalEntity
    {
        public int ID { get; set; }

        public bool IsActive = true;

        protected DalEntity(int ID)
        {
            this.ID = ID;
        }
        
        public abstract object Clone();
    }

}

