using System;
using DalObject;

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
            // TODO: write console UI with switch statement for options and outputs
            DataSource data = new();
            bool running = true;
            while (running)
            {
                DisplayMenu(new string[]{"Add", "Update", "Display", "List Display", "Exit"},
                        new Action[]{ 
                            () => DisplayAddMenu(data),
                            () => DisplayUpdateMenu(data),
                            () => DisplayDisplayMenu(data),
                            () => DisplayListDisplayMenu(data),
                            () => running = false 
                        });
            }

        }
        
        static void DisplayMenu(string[] choices, Action[] actions)
        {
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
            DisplayMenu(new string[] { "Add Base Station", "Add Drone", "Add Customer", "Add Package", "Cancel"},
                    new Action[]{ 
                        () => data.AddDroneStation(),
                        () => data.AddDrone(),
                        () => data.AddCustomer(),
                        () => data.AddParcel(),
                        () => {}
                    });
        }

        static void DisplayUpdateMenu(DataSource data)
        {
            /* DisplayMenu(new string[] { "Assign package to drone", "Collect package from drone", "Provide package to customer", */
            /*         "Send a drone to charge", "Release a drone from charging", "Cancel"}, */
            /*         new Action[]{ */ 
            /*             () => data.AssignPackageToDrone(), */
            /*             () => data.CollectPackageFromDrone(), */
            /*             () => data.ProvidePackageToCustomer(), */
            /*             () => data.SendDroneToCharge(), */
            /*             () => data.ReleaseDroneFromCharge(), */
            /*             () => {} */
            /*         }); */
        }

        static void DisplayDisplayMenu(DataSource data)
        {
            /* DisplayMenu(new string[] {"Display a base station", "Display a base drone", "Display a Customer", "Display a package", "Cancel"}, */
            /*         new Action[]{ */ 
            /*             () => data.DisplayDroneStation(), */
            /*             () => data.DisplayDrone(), */
            /*             () => data.DisplayCustomer(), */
            /*             () => data.DisplayParcel(), */
            /*             () => {} */
            /*         }); */
        }

        static void DisplayListDisplayMenu(DataSource data)
        {
            /* DisplayMenu(new string[] {"Display all base stations", "Display all drones", "Display all Customers", "Display all packages", "Cancel"}, */
            /*         new Action[]{ */ 
            /*             () => data.DisplayDroneStationList(), */
            /*             () => data.DisplayDroneList(), */
            /*             () => data.DisplayCustomerList(), */
            /*             () => data.DisplayParcelList(), */
            /*             () => data.DisplayNonAssignedPackages(), */
            /*             () => data.DisplayAvailableDroneStations(), */
            /*             () => {} */
            /*         }); */
        }
    }
}
