using System;
using System.Collections.Generic;
using HealthCenterSystem.Models;
using HealthCenterSystem.Services;

namespace HealthCenterSystem
{
    internal class Program
    {
        public static List<User> users = new List<User>();
        static SuperAdmin superAdmin = new SuperAdmin(users); // static SuperAdmin instance
        static List<Branch> branches = new List<Branch>();
        static void Main(string[] args)
        {
                 List<Branch> branches = new List<Branch>
                 {
                      new Branch { BranchId = 1, BranchName = "Muscat Branch" },
                      new Branch { BranchId = 2, BranchName = "Dhofar Branch" }
                 };

            List<Clinic> clinics = new List<Clinic>
            {
                     new Clinic(1, "Cardiology Clinic", "Muscat", 100.00m, "12345678", "cardio@clinic.com"),
                     new Clinic(2, "Pediatrics Clinic", "Muscat", 80.00m, "87654321", "pediatrics@clinic.com")
            };

            DoctorService doctorService = new DoctorService();

            Console.WriteLine("Welcome to Codeline Health System");

            int choice = 0;
            while (choice != 4)
            {
                Console.Clear();
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
                        Console.WriteLine("Super Admin Loging");
                        Console.Write("Enter Admin ID: ");
                        string ID = Console.ReadLine();
                        Console.Write("Enter Admin Password: ");
                        string adminPassword = Console.ReadLine();
                        if (ID != "1" || adminPassword != "123")
                        {
                            Console.WriteLine("Invalid ID or password.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Login successful.");
                            SuperAdminMenu(branches, clinics);
                        }
                        break;

                    case 2:
                        Console.WriteLine("You are logged in as Admin.");
                        Console.Write("Enter Doctor Name: ");
                        string doctorName = Console.ReadLine();
                        Console.Write("Enter Doctor Email: ");
                        string doctorEmail = Console.ReadLine();
                        Console.Write("Enter Doctor Password: ");
                        string doctorPassword = Console.ReadLine();
                        Console.Write("Enter Doctor Specialization: ");
                        string doctorSpecialization = Console.ReadLine();
                        //superAdmin.AddDoctor(doctorName, doctorSpecialization);
                      //  Console.WriteLine("Doctor added successfully.");
                       
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

            //SuperAdmin menu
            static void SuperAdminMenu(List<Branch> branches, List<Clinic> clinics)
            {
                Console.Clear();
                Console.WriteLine("SuperAdmin Menu:");
                Console.WriteLine("1. Add Admin");
                Console.WriteLine("2. Add Doctor");
                Console.WriteLine("3. Add Branch");
                Console.WriteLine("4. View Users");
                Console.WriteLine("0. Exit");
                int superAdminChoice = 0;
                while (superAdminChoice != 5)
                {

                    Console.Write("Select an option: ");
                    if (!int.TryParse(Console.ReadLine(), out superAdminChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                        continue;
                    }
                    switch (superAdminChoice)
                    {
                        case 1:
                            Console.Write("Enter Admin Name: ");
                            string adminName = Console.ReadLine();
                            Console.Write("Enter Admin Password: ");
                            string adminPassword = Console.ReadLine();
                            string adminEmail = superAdmin.AddAdmin(adminName, adminPassword);
                            Console.WriteLine($"Admin added successfully. Email: {adminEmail}");
                            break;

                        case 2:
                            DoctorService doctorService = new DoctorService();

                            Console.Write("Enter Doctor Name: ");
                            string doctorName = Console.ReadLine();

                            Console.Write("Enter Doctor Password: ");
                            string doctorPassword = Console.ReadLine();

                            Console.Write("Enter Doctor Specialization: ");
                            string doctorSpecialization = Console.ReadLine();

                            // Select Branch with validation
                           

                            // Add doctor and generate email
                            string doctorEmail = doctorService.AddDoctorAndGenerateEmail(doctorName, doctorPassword, doctorSpecialization, selectedClinic, selectedDepartment);

                            Console.WriteLine($"Doctor added successfully. Email: {doctorEmail}");
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.WriteLine("Add Branch");
                            Console.Write("Enter Branch Name: ");
                            string branchName = Console.ReadLine();
                            Console.Write("Enter Branch Location: ");
                            string branchLocation = Console.ReadLine();
                            Console.Write("Enter Number of Floors: ");
                            int noOfFloors;
                            while (!int.TryParse(Console.ReadLine(), out noOfFloors) || noOfFloors <= 0)
                            {
                                Console.Write("Invalid input. Please enter a valid number of floors: ");
                            }
                            Console.Write("Enter Number of Rooms: ");
                            int noOfRooms;

                            while (!int.TryParse(Console.ReadLine(), out noOfRooms) || noOfRooms <= 0)
                            {
                                Console.Write("Invalid input. Please enter a valid number of rooms: ");
                            }
                            //Console.Write("Enter Departments: ");
                            //string departments = Console.ReadLine();

                            //superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms, departments, clinics);
                            //Console.WriteLine("Branch added successfully.");
                            Console.ReadLine();
                            break;
                        case 4:
                            superAdmin.ViewUsers();
                            Console.WriteLine("Users viewed successfully.");
                            break;
                        case 5:
                            Console.WriteLine("Exiting Super Admin menu.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }
    }
}
