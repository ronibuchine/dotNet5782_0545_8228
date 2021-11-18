using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class PackageToList
        {
            public int ID { get; set; }
            public string senderName { get; set; }
            public string receiverName { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public PackageStatuses status { get; set; }
        }
    }
   
}
