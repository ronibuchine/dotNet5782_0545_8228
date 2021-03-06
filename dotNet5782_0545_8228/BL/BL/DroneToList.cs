using System;
using UTIL;


namespace BL
{
    /// <summary>
    /// A drone entity which is used for list representation
    /// </summary>
    public class DroneToList
    {
        public int ID { get; set; }
        public string model { get; set; }
        public WeightCategories weightCategory { get; set; }
        public double? battery { get; set; }
        public DroneStatuses? status { get; set; }
        public Location location { get; set; }
        public int? packageNumber { get; set; }

        public DroneToList(Drone drone)
        {
            ID = drone.ID;
            model = drone.model;
            weightCategory = drone.weightCategory;
            battery = drone.battery;
            status = drone.status;
            location = drone.currentLocation;
            packageNumber = drone.packageInTransfer != null ?  drone.packageInTransfer.ID : null;
        }
        public override string ToString()
        {
            return String.Format("ID = {0}, Model = {1}, WeightCategory = {2}, Battery = {3}, Drone Status = {4}, Location = {5}, Package Number = {6}", 
                ID, model, weightCategory, PrintDebug.ToStringOrNull(battery), PrintDebug.ToStringOrNull(status), PrintDebug.ToStringOrNull(location), PrintDebug.ToStringOrNull(packageNumber));
        }
    }
}
    

