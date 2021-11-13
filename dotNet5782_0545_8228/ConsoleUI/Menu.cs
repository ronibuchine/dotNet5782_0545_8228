using System;
using DalObject;
using System.Diagnostics;
using System.Collections.Generic;

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
           
            DataSource data = new();
            bool running = true;
            while (running)
            {
                DisplayMenu(new string[] { "Add", "Update", "Display All", "Display One", "Exit" },
                        new Action[]{
                            () => DisplayAddMenu(data),
                            () => DisplayUpdateMenu(data),
                            () => DisplayAllMenu(data),
                            () => DisplayOneMenu(data),
                            () => running = false
                        }) ;
            }
        }

        /// <summary>
        /// The Main function which displays the menu to the user in the console interface
        /// </summary>
        /// <param name="strings">An array of strings, each one corresponding to an option in the menu</param>
        /// <param name="actions">An array of Actions, each one corresponding to the appropiate string. Note: The order strings and actions matters and must match up</param>
        static void DisplayMenu(string[] choices, Action[] actions)
        {
            Trace.Assert(choices.Length == actions.Length); // ensure that there is an action for each choice
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, choices[i]));
            }
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out int choice) &&
                choice < actions.Length &&
                choice >= 0)
            {
                actions[choice]();
            }
        }

        static void DisplayAddMenu(DataSource data)
        {
            DisplayMenu(new string[] { "Add Base Station", "Add Drone", "Add Customer", "Add Package", "Cancel" },
                    new Action[]{
                        () => data.AddDroneStation(),
                        () => data.AddDrone(),
                        () => data.AddCustomer(),
                        () => data.AddParcel(),
                        () => {}
                    });
        }

        static int GetChoice(string prompt = "Please enter a choice: ")
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

        static void DisplayUpdateMenu(DataSource data)
        {
            DisplayMenu(new string[] { "Assign package to drone", "Collect package from drone", "Provide package to customer",
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

        


        static void DisplayOneMenu(DataSource data)
        {
            

            DisplayMenu(new string[] {"Display a base station", "Display a base drone", "Display a Customer", "Display a package", "Cancel"},
                    new Action[]{
                        () => data.DisplayDroneStation(GetChoice()),
                        () => data.DisplayDrone(GetChoice()),
                        () => data.DisplayCustomer(GetChoice()),
                        () => data.DisplayParcel(GetChoice()),
                        () => {}
                    });
        }

        static void DisplayAllMenu(DataSource data)
        {
            // TODO: add the rest of the things
            DisplayMenu(new string[] {"Display all base stations", "Display all drones", "Display all Customers", "Display all packages", "Display all packages not assigned to a drone", "Display all base stations with unoccupied charging stations", "Cancel"},
                    new Action[]{
                        () => data.DisplayAllDroneStations(),
                        () => data.DisplayAllDrones(),
                        () => data.DisplayAllCustomers(),
                        () => data.DisplayAllParcels(),
                        () => data.DisplayAllNotAssignedParcels(),
                        () => data.DisplayAllUnoccupiedStations(),
                        () => {}
                    });
        }
    }
}
