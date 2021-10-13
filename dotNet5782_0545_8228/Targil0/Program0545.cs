using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0545();
            Welcome8228();
        }

        private static void Welcome0545()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"{name}, welcome to my first console application!\n");
        }
        private static void Welcome8228()
        {
            Welcome0545();
        }
    }
}
