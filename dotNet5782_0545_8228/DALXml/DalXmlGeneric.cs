using System;
using DO;
using DALAPI;
using System.Collections.Generic;

namespace DAL
{

    public partial class DALXml : IDAL
    {
        public DALXml()
        {
        }
        /* static void Main() */
        /* { */
        /*     var drone = new Drone(1, "model", WeightCategories.heavy); */
        /*     System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(drone.GetType()); */
        /*     x.Serialize(Console.Out, drone); */
        /*     Console.WriteLine(); */
        /* } */

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int GetNextID()
        {
            throw new NotImplementedException();
        }

        public double[] PowerConsumptionRequest()
        {
            throw new NotImplementedException();
        }

        public bool VerifyCustomerCredentials(int ID, string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyEmployeeCredentials(int ID, string password)
        {
            throw new NotImplementedException();
        }
    }
}
