using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DALAPI
{
    /// <summary>
    /// This class is a configuration class for the data layer of our system
    /// </summary>
    class DalConfig
    {
        internal static string DalName;

        internal static Dictionary<string, string> DalPackages;

        /// <summary>
        /// The static constructor here loads the xml config file from the solution folder and laods the appropriate implementatino of the DALObject
        /// </summary>
        static DalConfig()
        {
            var path = Directory.GetCurrentDirectory();
            var containing = Directory.GetFiles(path, "*.sln");
            while (containing.Length == 0)
            {
                path = Directory.GetParent(path).FullName;
                containing = Directory.GetFiles(path, "*.sln");
            }

            XElement dalConfig = XElement.Load(path + @"/xml/dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages =
                (from pkg in dalConfig.Element("dal-packages").Elements() select pkg
            ).ToDictionary(p => "" + p.Name, p => p.Value);

        }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}

