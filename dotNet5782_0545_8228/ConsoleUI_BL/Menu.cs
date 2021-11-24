using System;
using System.Collections.Generic;
using System.Diagnostics;
using IBL.BO;

namespace ConsoleUI_BL
{
    class Menu
    {
        static void Main(string[] args)
        {
            BLOBject.BLOBject bl = new();
            bool running = true;
            //while (running)
            //{
            //    DisplayMenuNoOutput(new string[] { "Add", "Update", "Display", "List Display", "Exit" },
            //            new Action[]{
            //                () => DisplayAddMenu(data),
            //                () => DisplayUpdateMenu(data),
            //                () => DisplayAllMenu(data),
            //                () => DisplayOneMenu(data),
            //                () => running = false
            //            });
            //}

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
        //static void DisplayMenuWithOutput<T>(string[] choices, Func<List<T>>[] funcs) where T : DalEntity
        //{
        //    try
        //    {
        //        int choice = GetUserInput(choices, funcs.Length);
        //        funcs[choice]().ForEach((dalObj) => Console.WriteLine(dalObj.ToString()));
        //    }
        //    catch (IndexOutOfRangeException e) 
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        static void DisplayAddMenu(BLOBject.BLOBject bl)
        {
            //DisplayMenuNoOutput(new string[] { "Add Base Station", "Add Drone", "Add Customer", "Add Package", "Cancel" },
            //        new Action[]{
            //            () => bl.AddBaseStation(),
            //            () => bl.AddDrone(),
            //            () => bl.AddCustomer(),
            //            () => bl.AddParcel(),
            //            () => {}
            //        });
        }
    }
}
