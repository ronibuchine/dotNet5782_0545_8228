using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class Package
        {
            public int ID { get; set; }
            public Customer sender { get; set; }
            public Customer receiver { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Drone drone { get; set; }
            public DateTime creationTime { get; set; }
            public DateTime assigningTime { get; set; }
            public DateTime collectionTime { get; set; }
            public DateTime deliveringTime { get; set; }
        }
    }
}
