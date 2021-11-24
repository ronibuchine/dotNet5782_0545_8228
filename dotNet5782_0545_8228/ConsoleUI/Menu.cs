using System;
using System.Diagnostics;
using System.Collections.Generic;
using IDAL.DO;

namespace ConsoleUI
{
    /// <summary>
    /// Menu class which encapsulates the Main function for our program
    /// </summary>
    class Menu
    {
        /// <summary>
        /// The Main function which displays the menu to the user in the console interface
        /// </summary>
        /// <param name="args">arguments passed to the CLI</param>
        static void Main(string[] args)
        {
            IDAL.IdalInterface data = new DalObject.DalObject();
            bool running = true;
            while (running)
            {
                DisplayMenuNoOutput(new string[] { "Add", "Update", "Display All", "Display One", "Exit" },
                        new Action[]{
                            () => DisplayAddMenu(data),
                            () => DisplayUpdateMenu(data),
                            () => DisplayAllMenu(data),
                            () => DisplayOneMenu(data),
                            () => running = false
                        });
            }
        }

        static int GetUserInput(string[] choices, int numActions)
        {
            Trace.Assert(choices.Length == numActions); // ensure that there is an action for each choice
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, choices[i]));
            }
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int choice))
            {
                if (choice >= 0 && choice < numActions)
                {
                    return choice;
                }
                else
                {
                    throw new IndexOutOfRangeException("Choice out of bounds");
                }
            }
            throw new IndexOutOfRangeException("Invalid input");
        }

        /// <summary>
        /// The Main function which displays the menu to the user in the console interface
        /// </summary>
        /// <param name="strings">An array of strings, each one corresponding to an option in the menu</param>
        /// <param name="actions">An array of Actions, each one corresponding to the appropiate string. Note: The order strings and actions matters and must match up</param>
        static void DisplayMenuNoOutput(string[] choices, Action[] actions)
        {
            try
            {
                int choice = GetUserInput(choices, actions.Length);
                actions[choice]();
            }
            catch (IndexOutOfRangeException e) 
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// The Main function which displays the menu to the user in the console interface
        /// </summary>
        /// <param name="strings">An array of strings, each one corresponding to an option in the menu</param>
        /// <param name="funcs">An array of Actions, each one corresponding to the appropiate string. Note: The order strings and actions matters and must match up</param>
        static void DisplayMenuWithOutput<T>(string[] choices, Func<List<T>>[] funcs) where T : DalEntity
        {
            try
            {
                int choice = GetUserInput(choices, funcs.Length);
                funcs[choice]().ForEach((dalObj) => Console.WriteLine(dalObj.ToString()));
            }
            catch (IndexOutOfRangeException e) 
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void DisplayAddMenu(IDAL.IdalInterface data)
        {
            DisplayMenuNoOutput(new string[] { "Add Base Station", "Add Drone", "Add Customer", "Add Package", "Cancel" },
                    new Action[]{
                        () => data.AddStation(),
                        () => data.AddDrone(),
                        () => data.AddCustomer(),
                        () => data.AddPackage(),
                        () => {}
                    });
        }

        internal static int GetChoice(string prompt = "Please enter a choice: ")
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int choice))
            {
                return choice;
            }
            else
            {
                return -1;
            }
        }

        static void DisplayUpdateMenu(IDAL.IdalInterface data)
        {
            DisplayMenuNoOutput(new string[] { "Assign package to drone", "Collect package from drone", "Provide package to customer",
                     "Send a drone to charge", "Release a drone from charging", "Cancel"},
                     new Action[]{
                         () => data.AssignPackageToDrone(GetChoice("Please choose which package to assign:")),
                         () => data.CollectPackageFromDrone(GetChoice("Please choose which package to collect:")),
                         () => data.ProvidePackageToCustomer(GetChoice("Please choose which package to provide:")),
                         () => data.SendDroneToCharge(GetChoice("Please enter a drone station number:"), GetChoice("Please enter a drone number:")),
                         () => data.ReleaseDroneFromCharge(GetChoice("Please enter a drone station number:"), GetChoice("Please enter a drone number")),
                         () => {}
                     });
        }

        static void DisplayOneMenu(IDAL.IdalInterface data)
        {
            //DisplayMenuWithOutput(new string[] { "Display a base station", "Display a drone", "Display a Customer", "Display a package", "Cancel" },
            //        new Func<List<DalEntity>>[]{
            //            () => data.GetDroneStation(GetChoice()).ConvertAll(d => (DalEntity)d),
            //            () => data.GetDrone(GetChoice()).ConvertAll(d => (DalEntity)d),
            //            () => data.GetCustomer(GetChoice()).ConvertAll(d => (DalEntity)d),
            //            () => data.GetParcel(GetChoice()).ConvertAll(d => (DalEntity)d),
            //            () => new List<DalEntity>()
            //        });
            throw new Exception();
        }

        static void DisplayAllMenu(IDAL.IdalInterface data)
        {
            // TODO: add the rest of the things
            DisplayMenuWithOutput(new string[] { "Display all base stations", "Display all drones", "Display all Customers", "Display all packages", "Display all packages not assigned to a drone", "Display all base stations with unoccupied charging stations", "Cancel" },
                    new Func<List<DalEntity>>[]{
                        () => data.GetAllStations().ConvertAll(d => (DalEntity)d),
                        () => data.GetAllDrones().ConvertAll(d => (DalEntity)d),
                        () => data.GetAllCustomers().ConvertAll(d => (DalEntity)d),
                        () => data.GetAllParcels().ConvertAll(d => (DalEntity)d),
                        () => data.GetAllNotAssignedParcels().ConvertAll(d => (DalEntity)d),
                        () => data.GetAllUnoccupiedStations().ConvertAll(d => (DalEntity)d),
                        () => new List<DalEntity>()
                    });
        }
    }
}
