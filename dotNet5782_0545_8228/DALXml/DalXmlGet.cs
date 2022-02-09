using System;
using DO;
using DALAPI;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{
    public partial class DALXml : IDAL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetAllCharges()
        {
            IEnumerable<DroneCharge> charges;
            try
            {
                charges = (from c in LoadXml("droneCharges").Elements()
                           select new DroneCharge
                           {
                               droneId = Int32.Parse(c.Element("DroneId").Value),
                               stationId = Int32.Parse(c.Element("StationId").Value),
                               beganCharge = DateTime.Parse(c.Element("beganCharge").Value),
                           });
                if (charges == null)
                    throw new InvalidDalObjectException();
                return charges;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomers()
        {
            IEnumerable<Customer> customers;
            try
            {
                customers = (from c in LoadXml("customers").Elements()
                             where c.Element("IsActive").Value == "true"
                             select new Customer
                             {
                                 ID = Int32.Parse(c.Element("ID").Value),
                                 IsActive = c.Element("IsActive").Value == "true" ? true : false,
                                 name = c.Element("name").Value,
                                 phone = c.Element("phone").Value,
                                 longitude = Double.Parse(c.Element("longitude").Value),
                                 latitude = Double.Parse(c.Element("latitude").Value),
                                 password = c.Element("password").Value,
                             });
                if (customers == null)
                    throw new InvalidDalObjectException();
                return customers;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetAllDrones()
        {
            IEnumerable<Drone> drones;
            try
            {
                drones = (from d in LoadXml("drones").Elements()
                          where d.Element("IsActive").Value == "true"
                          select new Drone
                          {
                              ID = Int32.Parse(d.Element("ID").Value),
                              IsActive = d.Element("IsActive").Value == "true" ? true : false,
                              model = d.Element("model").Value,
                              maxWeight = ParseWeightCategory(d.Element("maxWeight").Value)
                          });
                if (drones == null)
                {
                    throw new InvalidDalObjectException();
                }
                return drones;
            }
            catch (NullReferenceException)
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetAllPackages()
        {
            IEnumerable<Package> packages;
            try
            {
                packages = (from p in LoadXml("packages").Elements()
                            where p.Element("IsActive").Value == "true"
                            select new Package
                            {
                                ID = Int32.Parse(p.Element("ID").Value),
                                IsActive = p.Element("IsActive").Value == "true" ? true : false,
                                senderId = Int32.Parse(p.Element("senderId").Value),
                                recieverId = Int32.Parse(p.Element("recieverId").Value),
                                droneId = Int32.Parse(p.Element("droneId").Value),
                                weight = ParseWeightCategory(p.Element("weight").Value),
                                priority = ParsePriorityCategory(p.Element("priority").Value),
                                requested = p.Element("requested").Value == "null" ? null : DateTime.Parse(p.Element("requested").Value),
                                scheduled = p.Element("scheduled").Value == "null" ? null : DateTime.Parse(p.Element("scheduled").Value),
                                pickedUp = p.Element("pickedUp").Value == "null" ? null : DateTime.Parse(p.Element("pickedUp").Value),
                                delivered = p.Element("delivered").Value == "null" ? null : DateTime.Parse(p.Element("delivered").Value),
                            });
                if (packages == null)
                    throw new InvalidDalObjectException();
                return packages;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAllStations()
        {
            IEnumerable<Station> stations;
            try
            {
                stations = (from s in LoadXml("stations").Elements()
                            where s.Element("IsActive").Value == "true"
                            select new Station
                            {
                                ID = Int32.Parse(s.Element("ID").Value),
                                IsActive = s.Element("IsActive").Value == "true" ? true : false,
                                name = s.Element("name").Value,
                                chargeSlots = Int32.Parse(s.Element("chargeSlots").Value),
                                longitude = Double.Parse(s.Element("longitude").Value),
                                latitude = Double.Parse(s.Element("latitude").Value)
                            });
                if (stations == null)
                    throw new InvalidDalObjectException();
                return stations;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Package> GetAllUnassignedPackages()
        {
            IEnumerable<Package> packages;
            try
            {
                packages = (from p in LoadXml("packages").Elements()
                            where p.Element("IsActive").Value == "true" && p.Element("scheduled").Value == "null"
                            select new Package
                            {
                                ID = Int32.Parse(p.Element("ID").Value),
                                IsActive = p.Element("IsActive").Value == "true" ? true : false,
                                senderId = Int32.Parse(p.Element("senderId").Value),
                                recieverId = Int32.Parse(p.Element("recieverId").Value),
                                droneId = Int32.Parse(p.Element("droneId").Value),
                                weight = ParseWeightCategory(p.Element("weight").Value),
                                priority = ParsePriorityCategory(p.Element("priority").Value),
                                requested = p.Element("requested").Value == "null" ? null : DateTime.Parse(p.Element("requested").Value),
                                scheduled = p.Element("scheduled").Value == "null" ? null : DateTime.Parse(p.Element("scheduled").Value),
                                pickedUp = p.Element("pickedUp").Value == "null" ? null : DateTime.Parse(p.Element("pickedUp").Value),
                                delivered = p.Element("delivered").Value == "null" ? null : DateTime.Parse(p.Element("delivered").Value),
                            });
                if (packages == null)
                    throw new InvalidDalObjectException();
                return packages;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetAllUnoccupiedStations()
        {
            IEnumerable<Station> stations;
            try
            {
                stations = (from s in LoadXml("stations").Elements()
                            where s.Element("IsActive").Value == "true" && Int32.Parse(s.Element("chargeSlots").Value) > 0
                            select new Station
                            {
                                ID = Int32.Parse(s.Element("ID").Value),
                                IsActive = s.Element("IsActive").Value == "true" ? true : false,
                                name = s.Element("name").Value,
                                chargeSlots = Int32.Parse(s.Element("chargeSlots").Value),
                                longitude = Double.Parse(s.Element("longitude").Value),
                                latitude = Double.Parse(s.Element("latitude").Value)
                            });
                if (stations == null)
                    throw new InvalidDalObjectException();
                return stations;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int ID)
        {
            try
            {
                var drone = (from c in LoadXml("customers").Elements()
                             where Int32.Parse(c.Element("ID").Value) == ID && c.Element("IsActive").Value == "true"
                             select new Customer
                             {
                                 ID = Int32.Parse(c.Element("ID").Value),
                                 IsActive = c.Element("IsActive").Value == "true" ? true : false,
                                 name = c.Element("name").Value,
                                 phone = c.Element("phone").Value,
                                 longitude = Int32.Parse(c.Element("longitude").Value),
                                 latitude = Int32.Parse(c.Element("latitude").Value),
                                 password = c.Element("password").Value,
                             }
                        ).First();
                if (drone == null)
                    throw new InvalidDalObjectException("There was an issue retrieving the entity.");
                return drone;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int ID)
        {
            try
            {
                LoadXml("drones");
                var drone = (from d in LoadXml("drones").Elements()
                             where Int32.Parse(d.Element("ID").Value) == ID && d.Element("IsActive").Value == "true"
                             select new Drone
                             {
                                 ID = Int32.Parse(d.Element("ID").Value),
                                 IsActive = d.Element("IsActive").Value == "true" ? true : false,
                                 model = d.Element("model").Value,
                                 maxWeight = ParseWeightCategory(d.Element("maxWeight").Value)
                             }
                        ).First();
                if (drone == null)
                    throw new InvalidDalObjectException("There was an issue retrieving the entity.");
                return drone;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Employee GetEmployee(int ID)
        {
            try
            {
                var employee = (from d in LoadXml("employees").Elements()
                                where Int32.Parse(d.Element("ID").Value) == ID && d.Element("IsActive").Value == "true"
                                select new Employee
                                {
                                    ID = Int32.Parse(d.Element("ID").Value),
                                    IsActive = d.Element("IsActive").Value == "true" ? true : false,
                                    password = d.Element("password").Value,
                                }
                        ).First();
                if (employee == null)
                    throw new InvalidDalObjectException("There was an issue retrieving the entity.");
                return employee;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package GetPackage(int ID)
        {
            try
            {
                var package = (from p in LoadXml("packages").Elements()
                               where Int32.Parse(p.Element("ID").Value) == ID && p.Element("IsActive").Value == "true"
                               select new Package
                               {
                                   ID = Int32.Parse(p.Element("ID").Value),
                                   IsActive = p.Element("IsActive").Value == "true" ? true : false,
                                   senderId = Int32.Parse(p.Element("senderId").Value),
                                   recieverId = Int32.Parse(p.Element("recieverId").Value),
                                   droneId = Int32.Parse(p.Element("droneId").Value),
                                   weight = ParseWeightCategory(p.Element("weight").Value),
                                   priority = ParsePriorityCategory(p.Element("priority").Value),
                                   requested = p.Element("requested").Value != "null" ? DateTime.Parse(p.Element("requested").Value) : null,
                                   scheduled = p.Element("scheduled").Value != "null" ? DateTime.Parse(p.Element("scheduled").Value) : null,
                                   pickedUp = p.Element("pickedUp").Value != "null" ? DateTime.Parse(p.Element("pickedUp").Value) : null,
                                   delivered = p.Element("delivered").Value != "null" ? DateTime.Parse(p.Element("delivered").Value) : null,
                               }
                        ).First();
                if (package == null)
                    throw new InvalidDalObjectException("There was an issue retrieving the entity.");
                return package;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int ID)
        {
            try
            {
                var station = (from s in LoadXml("stations").Elements()
                               where Int32.Parse(s.Element("ID").Value) == ID && s.Element("IsActive").Value == "true"
                               select new Station
                               {
                                   ID = Int32.Parse(s.Element("ID").Value),
                                   IsActive = s.Element("IsActive").Value == "true" ? true : false,
                                   name = s.Element("name").Value,
                                   chargeSlots = Int32.Parse(s.Element("chargeSlots").Value),
                                   longitude = Int32.Parse(s.Element("longitude").Value),
                                   latitude = Int32.Parse(s.Element("latitude").Value),
                               }
                        ).First();
                if (station == null)
                    throw new InvalidDalObjectException("There was an issue retrieving the entity.");
                return station;
            }
            catch
            {
                throw new InvalidDalObjectException("There was an issue retrieving the entity.");
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        static private WeightCategories ParseWeightCategory(string weight) =>
            weight switch
            {
                "heavy" => WeightCategories.heavy,
                "medium" => WeightCategories.medium,
                "light" => WeightCategories.light,
                _ => throw new Exception("not valid weight")
            };

        [MethodImpl(MethodImplOptions.Synchronized)]
        static private Priorities ParsePriorityCategory(string priority) =>
            priority switch
            {
                "regular" => Priorities.regular,
                "fast" => Priorities.fast,
                "emergency" => Priorities.emergency,
                _ => throw new Exception("not valid priority")
            };

    }
}
