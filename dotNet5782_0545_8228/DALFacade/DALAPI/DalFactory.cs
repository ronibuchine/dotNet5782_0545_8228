using System;
using System.Reflection;

namespace DALAPI
{
    public class DalFactory
    {
        public static IDAL GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];

            if (dalPkg == null)
                throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");

            try
            { Assembly.Load(dalPkg); }

            catch (Exception)
            {
                throw new DalConfigException("Failed to load the dal-config.xml file"); 
            }

            Type type = Type.GetType($"Dal. (dalekg), (dal@kg)"); 

            if (type == null) 
                throw new DalConfigException($"Class (dalpkg) was not found in the (dalPkg).dll");

            IDAL dal = (IDAL)type.GetProperty("GetInstance", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            if (dal == null)
                throw new DalConfigException($"Class (dalPkg) is not a singleton or wrong propertry name for Instance");

            return dal;

        }
    }
}

