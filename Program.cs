using System;
using System.Collections.Generic;
using HealthCenterSystem.Models;

namespace HealthCenterSystem
{
    internal class Program
    {
        public static List<User> users = new List<User>();
        static SuperAdmin superAdmin = new SuperAdmin(users); // static SuperAdmin instance

        static void Main(string[] args)
        {
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
                            SuperAdminMenu();
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
            static void SuperAdminMenu()
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
                            Console.Write("Enter Doctor Name: ");
                            string doctorName = Console.ReadLine();
                            Console.Write("Enter Doctor Password: ");
                            string doctorPassword = Console.ReadLine();
                            Console.Write("Enter Doctor Specialization: ");
                            string doctorSpecialization = Console.ReadLine();
                            string doctorEmail = superAdmin.AddDoctor(doctorName, doctorPassword, doctorSpecialization);
                            Console.WriteLine($"Doctor added successfully. Email: {doctorEmail}");
                            break;

                        case 3:
                            int branchOption = -1;
                            while (branchOption != 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Branch managment Menu:");
                                Console.WriteLine("1. Add Branch");
                                Console.WriteLine("2. Update Branch");
                                Console.WriteLine("3. Delete Branch");
                                Console.WriteLine("4. Add Department To Branch");
                                Console.WriteLine("5. View Branches");
                                Console.WriteLine("0. Exit Branch Management");

                                if (!int.TryParse(Console.ReadLine(), out branchOption))
                                {
                                    Console.WriteLine("Invalid input. Please enter a number between 0 and 5.");
                                    continue;
                                }
                                switch (branchOption)
                                {
                                    case 1:
                                        Console.Write("Enter Branch Name: ");
                                        string branchName = Console.ReadLine();
                                        Console.Write("Enter Branch Location: ");
                                        string branchLocation = Console.ReadLine();
                                        Console.Write("Enter Number of Floors: ");
                                        int noOfFloors = int.Parse(Console.ReadLine());
                                        Console.Write("Enter Number of Rooms: ");
                                        int noOfRooms = int.Parse(Console.ReadLine());
                                        Console.WriteLine("add Department");
                                        string departmentName = Console.ReadLine();
                                        Console.WriteLine("add Clinic");
                                        string clinicName = Console.ReadLine();
                                        superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms, departmentName, clinicName);
                                        Console.WriteLine("Branch added successfully.");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.Write("Enter Branch Name to update: ");
                                        string updateName = Console.ReadLine(); // 

                                        Console.Write("Enter New Branch Name: ");
                                        string newName = Console.ReadLine(); // 
                                        Console.Write("Enter New Location: ");
                                        string newLoc = Console.ReadLine();

                                        Console.Write("Enter New Number of Floors: ");
                                        int newFloors = int.Parse(Console.ReadLine());

                                        Console.Write("Enter New Number of Rooms: ");
                                        int newRooms = int.Parse(Console.ReadLine());

                                        // Call method with 5 parameters
                                        if (superAdmin.UpdateBranchByName(updateName, newName, newLoc, newFloors, newRooms))
                                        {
                                            Console.WriteLine("Branch updated successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Branch not found.");
                                        }

                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        Console.Write("Enter Branch Name to delete: ");
                                        string deleteName = Console.ReadLine();
                                        if (superAdmin.RemoveBranchByName(deleteName))
                                        {
                                            Console.WriteLine("Branch deleted successfully.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Branch not found.");
                                        }
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;

                                    case 4:
                                        Console.Write("Enter Branch ID to add department(s): ");
                                        int branchId;
                                        if (!int.TryParse(Console.ReadLine(), out branchId))
                                        {
                                            Console.WriteLine("Invalid ID.");
                                            break;
                                        }

                                        // Find the branch by ID
                                        var branch = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == branchId);
                                        if (branch == null)
                                        {
                                            Console.WriteLine("Branch not found.");
                                            break;
                                        }

                                        Console.Write("How many departments do you want to add? ");
                                        int deptCount;
                                        if (!int.TryParse(Console.ReadLine(), out deptCount) || deptCount <= 0)
                                        {
                                            Console.WriteLine("Invalid number of departments.");
                                            break;
                                        }

                                        for (int i = 0; i < deptCount; i++)
                                        {
                                            Console.Write($"Enter name of department #{i + 1}: ");
                                            string deptName = Console.ReadLine();

                                            // You can create a new Department object, assuming constructor is Department(string name)
                                            // Or set the DepName property after instantiating.

                                            Department newDept = new Department();
                                            newDept.DepName = deptName;

                                            // Add to the branch's Departments list
                                            branch.Departments.Add(newDept);
                                        }

                                        Console.WriteLine($"{deptCount} department(s) added successfully to branch '{branch.BranchName}'.");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;

                                    case 5:
                                        Console.WriteLine("List of Branches:");
                                        superAdmin.ViewBranches();
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    case 0:
                                        Console.WriteLine("Exiting Branch Management.");

                                        break;
                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        break;
                                }

                            }
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
