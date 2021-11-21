using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Package
        {
            public Package(int ID, int senderID, int receiverID, WeightCategories weight, Priorities priority)
            {
                this.ID = ID;
                this.weightCategory = weight;
                this.priority = priority;
                this.drone = null;
                
                foreach (IDAL.DO.Customer customer in DalObject.DataSource.customers)
                {
                    if (customer.ID == senderID) this.sender = new Customer(customer);
                    if (customer.ID == receiverID) this.receiver = new Customer(customer);
                }
            }
            public Package(IDAL.DO.Parcel parcel)
            {
                ID = parcel.ID;
                weightCategory = (IBL.BO.WeightCategories)parcel.weight;
                priority = (IBL.BO.Priorities)parcel.priority;
                
                requestedTime = parcel.requested;
                scheduledTime = parcel.scheduled;
                pickedUpTime = parcel.pickedUp;
                deliveryTime = parcel.delivered;
                
                
                foreach (IDAL.DO.Drone drone in DalObject.DataSource.drones)
                {
                    if (drone.ID == parcel.ID) this.drone = new IBL.BO.Drone(drone);
                }

                foreach (IDAL.DO.Customer customer in DalObject.DataSource.customers)
                {
                    if (customer.ID == parcel.senderId) this.sender = new IBL.BO.Customer(customer);
                }
            }
            
            public int ID { get; set; }
            public Customer sender { get; set; }
            public Customer receiver { get; set; } // TODO set this somewhere
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Drone drone { get; set; }
            public DateTime requestedTime { get; set; }
            public DateTime scheduledTime { get; set; }
            public DateTime pickedUpTime { get; set; }
            public DateTime deliveryTime { get; set; }
            public override string ToString()
            {
                return String.Format("ID = {0}, Sender = {1}, Receiver = {2}, Weight Category = {3}, Priority = {4}, Drone = {5}, Creation Time = {6}, Assignment Time = {7}, Collection Time = {8}, Delivering Time = {9}",
                    ID, sender.ToString(), receiver.ToString(), weightCategory, priority, drone.ToString(), requestedTime.ToString(), scheduledTime.ToString(), pickedUpTime.ToString(), deliveryTime.ToString());
            }
        }
    }
}
