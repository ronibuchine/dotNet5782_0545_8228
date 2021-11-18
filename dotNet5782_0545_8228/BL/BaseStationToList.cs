using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStationToList
        {
            public int ID { get; set; }
            public string name { get; set; }
            public int availableChargeSlots { get; set; }
            public int occupiedSlots { get; set; }
        }
    }

}
