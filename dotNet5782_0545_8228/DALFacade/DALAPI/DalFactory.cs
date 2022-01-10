using System;
using System.IO;
using System.Reflection;

namespace DALAPI
{
    /// <summary>
    /// This is a Singleton factory class used to construct DALObjects, whether they are implemented as DalXML or DalOBject with a software datasource
    /// </summary>
    public class DalFactory
    {
        public static IDAL GetDal()
        {
            /* System.Console.WriteLine(Directory.GetCurrentDirectory()); */
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];

            if (dalPkg == null)
                throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");

            var path = Directory.GetCurrentDirectory();
            var containing = Directory.GetFiles(path, "*.sln");
            while (containing.Length == 0)
            {
                path = Directory.GetParent(path).FullName;
                containing = Directory.GetFiles(path, "*.sln");
            }

            Assembly dalAssembly;
            try
            {
                /* System.Console.WriteLine(path); */
                /* System.Console.WriteLine(dalPkg); */
                dalAssembly = Assembly.LoadFile(path + $"\\{dalPkg}\\bin\\Debug\\net5.0\\" + $"{dalPkg}.dll");
            }
            catch (Exception)
            {
                throw new DalConfigException("Failed to load the correct dll file"); 
            }

            Type type = dalAssembly.GetType($"DAL.{dalPkg}");

            if (type == null) 
                throw new DalConfigException($"Class {dalPkg} was not found in the {dalPkg}.dll");

            var prop = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            IDAL dal = (IDAL)prop.GetValue(null);

            if (dal == null)
                throw new DalConfigException($"Class (dalPkg) is not a singleton or wrong propertry name for Instance");

            return dal;

        }
    }
}

