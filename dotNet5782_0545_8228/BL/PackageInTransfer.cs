using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    { 
        class PackageInTransfer
        {
            public int ID { get; set; }
            public IBL.BO.WeightCategories weightCategory { get; set; }
            public IBL.BO.Priorities priority { get; set; }
            public Boolean deliveryStatus { get; set; }
            public CustomerInPackage sender { get; set; }
            public CustomerInPackage receiver { get; set; }
            public Location collectionLocation { get; set; }
            public Location deliveringLocation { get; set; }
            public double deliveryDistance { get; set; }


        }
    }
}
