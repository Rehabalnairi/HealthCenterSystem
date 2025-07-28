using System;
using System.Collections.Generic;
using HealthCenterSystem.Models;

namespace HealthCenterSystem
{
    internal class Program
    {
        static List<User> users = new List<User>();
        static SuperAdmin superAdmin = new SuperAdmin(users); // static SuperAdmin instance

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Codeline Health System");

            int choice = 0;
            while (choice != 4)
            {
                Console.WriteLine("\nPlease select your role:");
                Console.WriteLine("1. Super Admin");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Doctor");
                Console.WriteLine("4. Patient");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You are logged in as Super Admin.");
                        Console.Write("Enter Admin Name: ");
                        string adminName = Console.ReadLine();
                        Console.Write("Enter Admin Email: ");
                        string adminEmail = Console.ReadLine();
                        Console.Write("Enter Admin Password: ");
                        string adminPassword = Console.ReadLine();
                        superAdmin.AddAdmin(adminName, adminEmail, adminPassword);
                        Console.WriteLine(" Admin added successfully.");
                        break;

                    case 2:
                        Console.WriteLine("You are logged in as Admin.");
                        Console.Write("Enter Doctor Name: ");
                        string doctorName = Console.ReadLine();
                        Console.Write("Enter Doctor Email: ");
                        string doctorEmail = Console.ReadLine();
                        Console.Write("Enter Doctor Password: ");
                        string doctorPassword = Console.ReadLine();
                        superAdmin.AddDoctor(doctorName, doctorEmail, doctorPassword);
                        Console.WriteLine("Doctor added successfully.");
                        break;

                    case 3:
                        Console.WriteLine("You are logged in as Doctor.");
                        Console.WriteLine("Doctor functionality coming soon...");
                        break;

                    case 4:
                        Console.WriteLine("You are logged in as Patient.");
                        Console.WriteLine("Patient functionality coming soon...");
                        break;

                    case 0:
                        Console.WriteLine("Exiting the system. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
