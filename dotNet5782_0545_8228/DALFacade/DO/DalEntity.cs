using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DALObject")]
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

        // needed for serialization
        protected DalEntity(){}
        
        public abstract object Clone();

        public override string ToString()
        {
            return String.Format("ID ={0}, IsActive = {1}", ID, IsActive);
        }
    }

}

