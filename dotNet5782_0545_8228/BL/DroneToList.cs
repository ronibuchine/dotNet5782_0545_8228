using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int ID { get; set; }
            public string model { get; set; }
            public WeightCategories weightCategory { get; set; }
            public double battery { get; set; }
            public DroneStatuses status { get; set; }
            public Location location { get; set; }
            public int packageNumber { get; set; }

            public override string ToString()
            {
                return String.Format("ID = {0}, Model = {1}, WeightCategory = {2}, Battery = {3}, Drone Status = {4}, Location = {5}, Package Number = {6}", 
                    ID, model, weightCategory, battery, status, location.ToString(), packageNumber);
            }
        }
    }
    
}
