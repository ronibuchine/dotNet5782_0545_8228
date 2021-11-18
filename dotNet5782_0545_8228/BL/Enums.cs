using System;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Enum types for different weight categories 
        /// </summary>
        public enum WeightCategories { heavy, medium, light };
        /// <summary>
        /// Enum types for potential drone statuses   
        /// </summary>
        public enum DroneStatuses { free, maintenance, delivery };
        /// <summary>
        /// Enum types for different priority levels
        /// </summary>
        public enum Priorities { regular, fast, emergency };
        public enum PackageStatuses { created, assigned, collected, delivered };
    }
}
