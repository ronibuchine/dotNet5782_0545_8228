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
            public override string ToString()
            {
                return String.Format("ID = {0}, Sender = {1}, Receiver = {2}, Weight Category = {3}, Priority = {4}, Drone = {5}, Creation Time = {6}, Assignment Time = {7}, Collection Time = {8}, Delivering Time = {9}",
                    ID, sender.ToString(), receiver.ToString(), weightCategory, priority, drone.ToString(), creationTime.ToString(), assigningTime.ToString(), collectionTime.ToString(), deliveringTime.ToString());
            }
        }
    }
}
