using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace DALAPI
{
    class DalConfig
    {
        internal static string DalName;

        internal static Dictionary<string, string> DalPackages;

        static DalConfig()
        {
            var path = Directory.GetCurrentDirectory();
            var containing = Directory.GetFiles(path, "*.sln");
            while (containing.Length == 0)
            {
                path = Directory.GetParent(path).FullName;
                containing = Directory.GetFiles(path, "*.sln");
            }

            XElement dalConfig = XElement.Load(path + @"\xml\dal-config.xml");
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

