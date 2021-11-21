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
            public PackageToList(Package package)
            {
                ID = package.ID;
                senderName = package.sender.name;
                receiverName = package.receiver.name;
                weightCategory = package.weightCategory;
                priority = package.priority;
            }
            public int ID { get; set; }
            public string senderName { get; set; }
            public string receiverName { get; set; }
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public PackageStatuses status { get; set; }

            public override string ToString()
            {
                return $"ID = {ID}, Sender = {senderName}, Recevier = {receiverName}, WeightCategory = {weightCategory}, Priority = {priority}, Package Status = {status}";
            }
        }
    }
   
}
