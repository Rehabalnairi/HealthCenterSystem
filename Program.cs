using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Numerics;
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

            int choice =0;
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
                         


                        break;

                    case 0:
                        return; // Exit the application
                        //Console.WriteLine("Exiting the system. Goodbye!");
                        //break;

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
                int superAdminChoice = -1;
                while (superAdminChoice != 0)
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
                            int adminId;
                            while (true)
                            {
                                Console.WriteLine("Enter Admin ID 'The Id must be morr than 6 digits' :");
                                string inputId = Console.ReadLine();
                                if (!int.TryParse(inputId, out adminId))
                                {
                                    Console.WriteLine("Invalid input. Please enter a numeric ID.");
                                    continue;
                                }
                                if (inputId.Length < 6)
                                {
                                    Console.WriteLine("ID must be at least 6 digits long. Please try again.");
                                    continue;
                                }
                                Console.WriteLine("Enter Admin Name: ");
                                string adminName = Console.ReadLine();

                                Console.WriteLine("Enter Admin Password: ");
                                string adminPassword = Console.ReadLine();
                                Console.WriteLine("Enter Admin Phone Number: ");
                                string adminPhoneNumber = Console.ReadLine();
                                if(string.IsNullOrWhiteSpace(adminName) || string.IsNullOrWhiteSpace(adminPassword) || string.IsNullOrWhiteSpace(adminPhoneNumber))
                                {
                                    Console.WriteLine("Admin Name, Password, and Phone Number cannot be empty. Please try again.");
                                    continue;
                                }
                                string adminEmail = superAdmin.AddAdmin(adminName, adminPassword);
                                Console.WriteLine($"Admin added successfully. Email: {adminEmail}");
                                Console.ReadKey();
                                break; // Exit the loop if a valid ID is entered

                            }
                            break;
                        case 2:
                            Console.Write("Enter doctor name: ");
                            string doctorName = Console.ReadLine();

                            Console.Write("Enter password: ");
                            string doctorPassword = Console.ReadLine();

                            Console.Write("Enter specialization: ");
                            string specialization = Console.ReadLine();

                          
                            if (superAdmin.BranchesList.Count == 0)
                            {
                                Console.WriteLine("No branches available. Please add a branch first.");
                                break;
                            }

                            Console.WriteLine("Available Branches:");
                            for (int i = 0; i < superAdmin.BranchesList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {superAdmin.BranchesList[i].BranchName}");
                            }

                            Console.Write("Select branch number to assign this doctor to: ");
                            int branchChoice = int.Parse(Console.ReadLine());

                            if (branchChoice < 1 || branchChoice > superAdmin.BranchesList.Count)
                            {
                                Console.WriteLine("Invalid branch selection.");
                                break;
                            }

                            Branch selectedBranch = superAdmin.BranchesList[branchChoice - 1];

                            string doctorEmail = superAdmin.AddDoctor(doctorName, doctorPassword, specialization);

                            Console.WriteLine($"Doctor added successfully with email: {doctorEmail}");

                            break;



                        case 3:
                            Console.Clear();
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
                                       
                                        superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms,"","");
                                        Console.WriteLine("Branch added successfully.");
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        Console.Write("Enter Branch Name to update: ");
                                        string updateName = Console.ReadLine(); 

                                        Console.Write("Enter New Branch Name: ");
                                        string newName = Console.ReadLine(); 
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
                                        if (!int.TryParse(Console.ReadLine(), out int BranchId))
                                        {
                                            Console.WriteLine("Invalid ID.");
                                            break;
                                        }

                                        // Find the branch by ID
                                        var branch = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == BranchId);
                                        if (branch == null)
                                        {
                                            Console.WriteLine("Branch not found.");
                                            break;
                                        }

                                        Console.Write("How many departments do you want to add? ");
                                        if (!int.TryParse(Console.ReadLine(), out int deptCount) || deptCount <= 0)
                                        {
                                            Console.WriteLine("Invalid number of departments.");
                                            break;
                                        }

                                        for (int i = 0; i < deptCount; i++)
                                        {
                                            Console.Write($"Enter name of department #{i + 1}: ");
                                            string deptName = Console.ReadLine();

                                            Department newDept = new Department();
                                            newDept.DepName = deptName;
                                            newDept.Clinics = new List<Clinic>();

                                            Console.Write($"How many clinics in department '{deptName}'? ");
                                            if (!int.TryParse(Console.ReadLine(), out int clinicCount) || clinicCount < 0)
                                            {
                                                Console.WriteLine("Invalid clinic count. Skipping this department.");
                                                continue;
                                            }

                                            for (int j = 0; j < clinicCount; j++)
                                            {
                                                Console.Write($"\tEnter name of clinic #{j + 1} in department '{deptName}': ");
                                                string clinicName = Console.ReadLine();
                                                Clinic newClinic = new Clinic {
                                                    Name = clinicName,
                                                    IsActive = true
                                                };
                                                newDept.Clinics.Add(newClinic);
                                            }

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
                                        return; // Exit Branch Management menu

                                        //break;
                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        break;
                                }

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
                            return; // Exit Super Admin menu
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }

          static void PatientMenu()
            {
                Console.WriteLine("")


            }
        }
    }

