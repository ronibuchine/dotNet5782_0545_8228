using System;

namespace IBL
{
    namespace BO
    {
        public class Package
        {
            public int ID { get; set; }
            public Customer sender { get; set; }
            public Customer receiver { get; set; } // TODO set this somewhere
            public WeightCategories weightCategory { get; set; }
            public Priorities priority { get; set; }
            public Drone drone { get; set; }
            public DateTime requested { get; set; }
            public DateTime scheduled { get; set; }
            public DateTime pickedUp { get; set; }
            public DateTime delivered { get; set; }

            public Package(int ID, int senderID, int receiverID, WeightCategories weight, Priorities priority)
            {
                this.ID = ID;
                this.weightCategory = weight;
                this.priority = priority;
                this.drone = null;

                
                /* foreach (IDAL.DO.Customer customer in DalObject.DalObject.GetAllCustomers()) */
                /* { */
                /*     if (customer.ID == senderID) */ 
                /*         this.sender = new Customer(customer); */
                /*     if (customer.ID == receiverID) */ 
                /*         this.receiver = new Customer(customer); */
                /* } */
            }
            public Package(IDAL.DO.Package parcel)
            {
                ID = parcel.ID;
                weightCategory = (IBL.BO.WeightCategories)parcel.weight;
                priority = (IBL.BO.Priorities)parcel.priority;
                requested = parcel.requested;
                scheduled = parcel.scheduled;
                pickedUp = parcel.pickedUp;
                delivered = parcel.delivered;
                
                
                /* foreach (IDAL.DO.Drone drone in DalObject.DataSource.drones) */
                /* { */
                /*     if (drone.ID == parcel.droneId) */ 
                /*         this.drone = new IBL.BO.Drone(drone); */
                /* } */

                /* foreach (IDAL.DO.Customer customer in DalObject.DataSource.customers) */
                /* { */
                /*     if (customer.ID == parcel.senderId) */
                /*         this.sender = new IBL.BO.Customer(customer); */
                /* } */
            }
            

            public override string ToString()
            {
                return String.Format("ID = {0}, Sender = {1}, Receiver = {2}, Weight Category = {3}, Priority = {4}, Drone = {5}, Creation Time = {6}, Assignment Time = {7}, Collection Time = {8}, Delivering Time = {9}",
                    ID, sender.ToString(), receiver.ToString(), weightCategory, priority, drone.ToString(), requested.ToString(), scheduled.ToString(), pickedUp.ToString(), delivered.ToString());
            }
        }
    }
}
