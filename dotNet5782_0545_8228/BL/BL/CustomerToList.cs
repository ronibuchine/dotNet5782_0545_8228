using System;
using System.Linq;
using DALAPI;


namespace BL
{
    /// <summary>
    /// Customer entity which is used for list representation
    /// </summary>
    public class CustomerToList
    {
        public CustomerToList(Customer customer)
        {
            ID = customer.ID;
            name = customer.name;
            phoneNumber = customer.phone;
            IDAL dal = DalFactory.GetDal();
            numberExpectedPackages = dal.GetAllPackages().Where(p => p.recieverId == ID && p.delivered == null).Count();
            numberPackagesDelivered = dal.GetAllPackages().Where(p => p.senderId == ID && p.delivered != null).Count();
            numberPackagesUndelivered = dal.GetAllPackages().Where(p => p.senderId == ID && p.delivered == null).Count();
            numberReceivedPackages = dal.GetAllPackages().Where(p => p.recieverId == ID && p.delivered != null).Count();
        }

        public int ID {get; set;}
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public int numberPackagesDelivered { get; set; }
        public int numberPackagesUndelivered { get; set; }
        public int numberReceivedPackages { get; set; }
        public int numberExpectedPackages { get; set; }

        public override string ToString()
        {
            return String.Format("ID = {0}, Name = {1}, Phone Number = {2}, Packages Delivered = {3}, Packages Not Delivered = {4}, Received Packages = {5}, Expected Packages = {6}",
                ID, name, phoneNumber, numberPackagesDelivered, numberPackagesUndelivered, numberReceivedPackages, numberExpectedPackages);
        }
    }
}


