using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Reflection;
using HealthCenterSystem.Models;
using HealthCenterSystem.Services;
using static HealthCenterSystem.Models.HelperClass;

namespace HealthCenterSystem
{
    internal class Program
    {
        public static List<User> users = new List<User>();
        static SuperAdmin superAdmin = new SuperAdmin(users); // static SuperAdmin instance
        public static List<Admins> admins = new List<Admins>();
        static List<Branch> branches = new List<Branch>();
        static PatientRecordService recordService = new PatientRecordService();

        static void Main(string[] args)
        {
            List<Branch> branches = new List<Branch>();
            List<Clinic> clinics = new List<Clinic>();
            DoctorService doctorService = new DoctorService();
            Console.WriteLine("Welcome to Codeline Health System");
            int choice = -1;
            while (choice != 0)
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
                        Console.WriteLine("==Super Admin Loging==");
                        Console.Write("Enter Super Admin ID: ");
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
                        Console.WriteLine("==Admin Login==");
                        Console.WriteLine("Enter your ID: ");
                        string adminID = Console.ReadLine();
                        Console.WriteLine("Enter You Passowerd");
                        string AdPassowerd = Console.ReadLine();
                        if (!int.TryParse(adminID, out int userId))
                        {
                            Console.WriteLine("Invaild ID.The Id must be numric");
                            Console.ReadKey();
                        }
                        Admins addminFound = admins.FirstOrDefault(a => a.UserId == userId && a.Password == AdPassowerd);
                        if (addminFound == null)
                        {
                            Console.WriteLine("Invalid ID or password.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Login successful.");
                            AdminMenu();
                        }
                        break;

                    case 3:

                        Console.WriteLine("== Doctor Login ==");
                        Console.WriteLine("Enter your ID: ");
                        string doctorID = Console.ReadLine();
                        Console.WriteLine("Enter your Password: ");
                        string doctorPassword = Console.ReadLine();
                        if (!int.TryParse(doctorID, out int doctorUserId))
                        {
                            Console.WriteLine("Invalid ID. The ID must be numeric.");
                            Console.ReadKey();
                            break;
                        }
                        Doctor foundDoctor = superAdmin.UsersList
                            .OfType<Doctor>()
                            .FirstOrDefault(d => d.UserId == doctorUserId && d.Password == doctorPassword);

                        if (foundDoctor == null)
                        {
                            Console.WriteLine("Invalid doctor ID or password.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine($"Login successful. Welcome Dr. {foundDoctor.Name}");
                            Console.ReadKey();
                            DoctorMenu(foundDoctor);
                        }
                        break;
                    case 4: PatientMenu();break;
                    case 0:
                        return; // Exit the application
                        Console.WriteLine("Exiting the system. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
            //SuperAdmin menu
            void SuperAdminMenu(List<Branch> branches, List<Clinic> clinics)
            {
                int superAdminChoice = -1;

                while (superAdminChoice != 0)
                {
                    Console.Clear();
                    Console.WriteLine("SuperAdmin Menu: Select an option");

                    Console.WriteLine("1. Add Admin");
                    Console.WriteLine("2. Add Doctor");
                    Console.WriteLine("3. Add Branch");
                    Console.WriteLine("4. View Users");
                    Console.WriteLine("0. Exit");
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
                                Console.WriteLine("Enter Admin ID: (The Id must be more than 6 digits) :");
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
                                if (superAdmin.UsersList.Any(u => u.UserId == adminId))
                                {
                                    Console.WriteLine("This ID is already used by another user. Please try a different one.");
                                    continue;
                                }

                                break;
                            }
                            string adminName;
                            while (true)
                            {
                                Console.WriteLine("Enter Admin Name: (name must contain letters only)");
                                adminName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(adminName) || !adminName.All(char.IsLetter))
                                {
                                    Console.WriteLine(" Admin name must contain letters only (no digits or symbols).");
                                    continue;
                                }
                                break;
                            }

                            string adminPassword;
                            while (true)
                            {
                                Console.WriteLine("Enter Admin Password: (Password must contain letters, numbers, and at least one symbol)");
                                adminPassword = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(adminPassword) ||
                                !adminPassword.Any(char.IsLetter) ||
                                !adminPassword.Any(char.IsDigit) ||
                                !adminPassword.Any(ch => !char.IsLetterOrDigit(ch)))
                                {
                                    Console.WriteLine(" Password must contain letters, numbers, and at least one symbol.");
                                    continue;
                                }
                                break;
                            }

                            string adminPhoneNumber;
                            while (true)
                            {

                                Console.WriteLine("Enter Admin Phone Number: ( must be at least 8 numbers) ");
                                adminPhoneNumber = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(adminPhoneNumber) ||
                                adminPhoneNumber.Length < 8 ||
                               !adminPhoneNumber.All(char.IsDigit))
                                {
                                    Console.WriteLine(" Phone number must be at least 8 digits and contain digits only.");
                                    continue;
                                }
                                break;
                            }

                            string adminEmail = superAdmin.AddAdmin(adminId, adminName, adminPassword, adminPhoneNumber);
                            Console.WriteLine($"Admin added successfully. Email: {adminEmail}");
                            Console.ReadKey();
                            break; // Exit the loop if a valid ID is entered
                        case 2:
                            if (superAdmin.BranchesList.Count == 0)
                            {
                                Console.WriteLine("No branches available. Please add a branch first.");
                                Console.ReadKey();
                                break;
                            }
                            int doctorId;
                            while (true)
                            {
                                Console.Write("Enter doctor ID: (The Id must be more than 6 digits) ");
                                string inputDoctorId = Console.ReadLine();
                                if (!int.TryParse(inputDoctorId, out doctorId))
                                {
                                    Console.WriteLine("Invalid input. Please enter a numeric ID");
                                    continue;
                                }
                                if (inputDoctorId.Length < 6)
                                {
                                    Console.WriteLine("ID must be at least 6 digits long. Please try again.");
                                    continue;
                                }

                                if (superAdmin.UsersList.Any(u => u.UserId == doctorId))
                                {
                                    Console.WriteLine("This ID is already used by another user. Please try a different one.");
                                    continue;
                                }

                                break;
                            }

                            string doctorName;
                            while (true)
                            {
                                Console.Write("Enter doctor name: (name must contain letters only ) ");
                                doctorName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(doctorName) || !doctorName.All(char.IsLetter))
                                {
                                    Console.WriteLine("Doctor name must contain letters only (no digits or symbols).");
                                    continue;
                                }
                                break;
                            }

                            string doctorPassword;
                            while (true)
                            {
                                Console.Write("Enter password: ( must contain letters, numbers, and at least one symbol )");
                                doctorPassword = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(doctorPassword) ||
                                 !doctorPassword.Any(char.IsLetter) ||
                                 !doctorPassword.Any(char.IsDigit) ||
                                 !doctorPassword.Any(ch => !char.IsLetterOrDigit(ch)))
                                {
                                    Console.WriteLine("Password must contain letters, numbers, and at least one symbol.");
                                    continue;
                                }
                                break;
                            }

                            string specialization;
                            while (true)
                            {
                                Console.Write("Enter specialization (letters only): ");
                                specialization = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(specialization) || !specialization.All(char.IsLetter))
                                {
                                    Console.WriteLine("Specialization must contain letters only. No numbers or symbols allowed.");
                                    continue;
                                }

                                break;
                            }

                            Console.WriteLine("Available Branches:");
                            for (int i = 0; i < superAdmin.BranchesList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {superAdmin.BranchesList[i].BranchName}");
                            }

                            int branchChoice;
                            while (true)
                            {
                                Console.Write("Select branch number to assign this doctor to: ");
                                if (!int.TryParse(Console.ReadLine(), out branchChoice) ||
                                branchChoice < 1 || branchChoice > superAdmin.BranchesList.Count)
                                {
                                    Console.WriteLine("Invalid branch selection.");
                                    continue;
                                }
                                break;
                            }

                            Branch selectedBranch = superAdmin.BranchesList[branchChoice - 1];
                            string doctorEmail = superAdmin.GenerateEmail(doctorName, "doctor");

                            Doctor newDoctor = new Doctor(doctorId, doctorName, doctorEmail, doctorPassword, specialization);
                            newDoctor.BranchId = selectedBranch.BranchId;
                            superAdmin.UsersList.Add(newDoctor);

                            Console.WriteLine($"Doctor added successfully with email: {doctorEmail}");
                            Console.ReadKey();
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
                                Console.WriteLine("0. Exit menu");

                                if (!int.TryParse(Console.ReadLine(), out branchOption))
                                {
                                    Console.WriteLine("Invalid input. Please enter a number between 0 and 5.");
                                    Console.ReadKey();
                                    continue;
                                }
                                switch (branchOption)
                                {
                                    case 1:
                                        do 
                                        { 
                                        string branchName;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch Name: ");
                                            branchName = Console.ReadLine()?.Trim();

                                            if (string.IsNullOrWhiteSpace(branchName) || branchName.Any(char.IsDigit))
                                            {
                                                Console.WriteLine("Invalid branch name. It must contain letters only and cannot be empty.");
                                                continue;
                                            }
                                            break;
                                        }

                                        string branchLocation;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch Location: ");
                                            branchLocation = Console.ReadLine()?.Trim();

                                            if (string.IsNullOrWhiteSpace(branchLocation) || branchLocation.Any(char.IsDigit))
                                            {
                                                Console.WriteLine("Invalid branch location. It must contain letters only and cannot be empty.");
                                                continue;
                                            }
                                            break;
                                        }

                                        int noOfFloors;
                                        while (true)
                                        {
                                            Console.Write("Enter Number of Floors: ");
                                            string floorsInput = Console.ReadLine();
                                            if (!int.TryParse(floorsInput, out noOfFloors))
                                            {
                                                Console.WriteLine("Invalid input. Number of floors must be a numeric value.");
                                                continue;
                                            }
                                            break;
                                        }
                                            

                                        int noOfRooms;
                                        while (true)
                                        {
                                            Console.Write("Enter Number of Rooms: ");
                                            string roomsInput = Console.ReadLine();
                                            if (!int.TryParse(roomsInput, out noOfRooms))
                                            {
                                                Console.WriteLine("Invalid input. Number of rooms must be a numeric value.");
                                                continue;
                                            }
                                            break;
                                        }

                                        Branch newBranch = superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms, "", "");
                                        branches.Add(newBranch); //add new branch to the list
                                        Console.WriteLine("Branch added successfully.");

                                            Console.Write("Do you want to add another branch? (Y/N): ");
                                        }
                                        while (Console.ReadLine().Trim().ToUpper() == "Y");

                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;

                                    case 2:
                                        int updateId;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch ID to update: ");
                                            string inputId = Console.ReadLine();
                                            if (!int.TryParse(inputId, out updateId))
                                            {
                                                Console.WriteLine("Invalid ID format. Please enter a numeric ID");
                                                continue;
                                            }

                                            var selectbranch = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == updateId);
                                            if (selectbranch == null)
                                            {
                                                Console.WriteLine("Branch with this ID does not exist.");
                                                Console.Write("Do you want to return to continue? (Y/N): ");
                                                string choice = Console.ReadLine().Trim().ToUpper();

                                                if (choice == "Y")
                                                {
                                                    continue;
                                                }
                                                else if (choice == "N")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid choice. Please enter Y or N.");
                                                    continue;
                                                }
                                            }
                                            break;
                                        }

                                        var branchToUpdate = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == updateId);
                                        if (branchToUpdate == null)
                                        {
                                            break;
                                        }

                                        string newName;
                                        while (true)
                                        {
                                            Console.Write("Enter New Branch Name: ");
                                            newName = Console.ReadLine()?.Trim();
                                            if (string.IsNullOrWhiteSpace(newName) || newName.Any(char.IsDigit))
                                            {
                                                Console.WriteLine("Invalid branch name. Only letters allowed.");
                                                continue;
                                            }
                                            break;
                                        }

                                        string newLocation;
                                        while (true)
                                        {

                                            Console.Write("Enter New Location: ");
                                            newLocation = Console.ReadLine()?.Trim();
                                            if (string.IsNullOrWhiteSpace(newLocation) || newLocation.Any(char.IsDigit))
                                            {
                                                Console.WriteLine("❌Invalid location. Only letters allowed.");
                                                continue;
                                            }
                                            break;
                                        }

                                        int newFloors;
                                        while (true)
                                        {
                                            Console.Write("Enter New Number of Floors: ");
                                            string floorsInput = Console.ReadLine();
                                            if (!int.TryParse(floorsInput, out newFloors))
                                            {
                                                Console.WriteLine("Invalid number of floors. Number of floors must be a numeric value");
                                                continue;
                                            }
                                            break;
                                        }

                                        int newRooms;
                                        while (true)
                                        {
                                            Console.Write("Enter New Number of Rooms: ");
                                            string roomsInput = Console.ReadLine();
                                            if (!int.TryParse(roomsInput, out newRooms))
                                            {
                                                Console.WriteLine("Invalid number of rooms.");
                                                continue;
                                            }
                                            break;
                                        }

                                        bool updated = superAdmin.UpdateBranch(updateId, newName, newLocation, newFloors, newRooms);

                                        if (updated)
                                        {
                                            Console.WriteLine("Branch updated successfully.");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                        }

                                        else
                                        {
                                            Console.WriteLine("Failed to update branch.");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                        }
                                        break;


                                    case 3:
                                        int deleteId;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch ID to delete: ");
                                            string deleteInput = Console.ReadLine();
                                            if (!int.TryParse(deleteInput, out deleteId))
                                            {
                                                Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                                                continue;
                                            }
                                            break;
                                        }

                                        if (superAdmin.RemoveBranch(deleteId))
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
                                        Console.Clear();
                                        int branchId;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch ID to add department(s): ");
                                            string inputBranchId = Console.ReadLine();

                                            if (!int.TryParse(inputBranchId, out branchId))
                                            {
                                                Console.WriteLine("Invalid ID. Please enter a numeric Branch ID.");
                                                continue;
                                            }

                                            // Find the branch by ID
                                            var branch = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == branchId);
                                            if (branch == null)
                                            {
                                                Console.WriteLine("Branch not found.");
                                                Console.Write("Would you like to try again? (Y/N): ");
                                                string choice = Console.ReadLine().Trim().ToUpper();

                                                if (choice == "Y") continue;
                                                else break;
                                            }

                                            // Input number of departments
                                            int deptCount;
                                            while (true)
                                            {
                                                Console.Write("How many departments do you want to add? ");
                                                string inputDeptCount = Console.ReadLine();

                                                if (!int.TryParse(inputDeptCount, out deptCount) || deptCount <= 0)
                                                {
                                                    Console.WriteLine("Invalid number. Please enter a positive integer.");
                                                    continue;
                                                }
                                                break;
                                            }

                                            for (int i = 0; i < deptCount; i++)
                                            {
                                                string deptName;
                                            while (true)
                                            {
                                                Console.Write($"\nEnter name of department #{i + 1}: ");
                                                deptName = Console.ReadLine()?.Trim();

                                                if (string.IsNullOrWhiteSpace(deptName) || deptName.Any(char.IsDigit))
                                                {
                                                    Console.WriteLine("Invalid department name. It must contain letters only.");
                                                    continue;
                                                }
                                                break;
                                            }

                                                Department newDept = new Department
                                                {
                                                    DepName = deptName,
                                                    Clinics = new List<Clinic>()
                                                };

                                                // Input number of clinics
                                                int clinicCount;
                                                while (true)
                                                {
                                                    Console.Write($"How many clinics in department '{deptName}'? ");
                                                    string inputClinicCount = Console.ReadLine();

                                                    if (!int.TryParse(inputClinicCount, out clinicCount) || clinicCount <= 0)
                                                    {
                                                        Console.WriteLine("Invalid number. Please enter a positive integer.");
                                                        continue;
                                                    }
                                                    break;
                                                }

                                                // Add clinics
                                                for (int j = 0; j < clinicCount; j++)
                                                {
                                                    string clinicName;
                                            while (true)
                                            {
                                                Console.Write($"\tEnter name of clinic #{j + 1} in '{deptName}': ");
                                                clinicName = Console.ReadLine()?.Trim();

                                                if (string.IsNullOrWhiteSpace(clinicName) || clinicName.Any(char.IsDigit))
                                                {
                                                    Console.WriteLine("Invalid clinic name. It must contain letters only");
                                                    continue;
                                                }
                                                break;
                                            }

                                                    Clinic newClinic = new Clinic
                                                    {
                                                        Name = clinicName,
                                                        IsActive = true
                                                    };

                                                    newDept.Clinics.Add(newClinic);
                                                }

                                                branch.Departments.Add(newDept);
                                                Console.WriteLine($"Department '{deptName}' with {newDept.Clinics.Count} clinic(s) added successfully.");
                                            }

                                            Console.WriteLine($"\n{deptCount} department(s) added successfully to branch '{branch.BranchName}'.");
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;
                                        }
                                        break;


                                    case 5:
                                        superAdmin.ViewBranches();
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;


                                    case 0:
                                        Console.WriteLine("Returning to SuperAdmin menu...");
                                        Console.ReadKey();
                                        break;


                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        Console.ReadKey();
                                        break;
                                }

                            }
                            //Console.ReadLine();
                            break;
                        case 4:
                            superAdmin.ViewUsers();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case 0:
                            Console.WriteLine("Exiting Super Admin menu.");
                            return; // Exit Super Admin menu
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadKey();
                            break;
                    }
                }
            }


             void  AdminMenu()
            {
                int adminChoice = -1;

                while (adminChoice != 0)
                {
                    Console.Clear();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Assign Exisiting Doctoer to Department and clinic");
                Console.WriteLine("2. Add Patient");
                Console.WriteLine("3. Update Or Delete Doctor");
                Console.WriteLine("4. Add Appointment");
                Console.WriteLine("5. Book Appointments For Patient");
                Console.WriteLine("6. Views");
                Console.WriteLine("0. Exit Admin Menu");

                    Console.Write("Select an option: ");
                    if (!int.TryParse(Console.ReadLine(), out adminChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
                        Console.ReadKey();
                        continue;
                    }
                    switch (adminChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("==Assign Exisiting Doctoer to Department and clinic==");
                            // Check if there are any branches available
                            if (branches.Count == 0)
                            {
                                Console.WriteLine("No branch available.");
                                break;
                            }
                            //view available branches
                            Console.WriteLine("\nAvailable Branches");

                            foreach (var b in branches)
                                Console.WriteLine($"{b.BranchId}.\n{b.BranchName}");
                            //select branch
                            Console.WriteLine("Enter Branch ID:");
                            if (!int.TryParse(Console.ReadLine(), out int branchId))
                            {
                                Console.WriteLine("Invaild Input!");
                                break;
                            }
                            var selectBranch = branches.FirstOrDefault(b => b.BranchId == branchId);
                            if (selectBranch == null)
                            {
                                Console.WriteLine("Branch not found");
                                break;
                            }
                            if (selectBranch.Departments.Count == 0)
                            {
                                Console.WriteLine("This Branch has no department");
                                break;
                            }
                            Console.WriteLine("\nAvailable Departments:");
                            foreach (var dep in selectBranch.Departments)
                                Console.WriteLine($"{dep.DepId}.{dep.DepName}");
                            //select department
                            Console.WriteLine("Enter Department ID:");
                            if (!int.TryParse(Console.ReadLine(), out int depId))
                            {
                                Console.WriteLine("Invalid Input!");
                                break;
                            }

                            var selectedDepartment = selectBranch.Departments.FirstOrDefault(d => d.DepId == depId);
                            if (selectedDepartment == null)
                            {
                                Console.WriteLine("Department not found.");
                                break;
                            }
                            if (selectedDepartment.Clinics == null || selectedDepartment.Clinics.Count == 0)
                            {
                                Console.WriteLine("This department has no clinics.");
                                break;
                            }
                            Console.WriteLine("Available Clinics in this Department:");
                            foreach (var clinic in selectedDepartment.Clinics)
                                Console.WriteLine($"{clinic.ClinicId}{clinic.Name}");
                            //select clinic
                            Console.WriteLine("Enter Clinic ID to assign doctor:");
                            if (!int.TryParse(Console.ReadLine(), out int clinicId))
                            {
                                Console.WriteLine("Invalid Input!");
                                break;
                            }
                            var selectedClinic = selectedDepartment.Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
                            if (selectedClinic == null)
                            {
                                Console.WriteLine("Clinic not found.");
                                break;
                            }
                            Console.WriteLine("Enter Doctoer ID to assign:");
                            if (!int.TryParse(Console.ReadLine(), out int doctorId))
                            {
                                Console.WriteLine("Invalid Input!");
                                break;
                            }
                            Doctor doctor = users.OfType<Doctor>().FirstOrDefault(d => d.UserId == doctorId);
                            if (doctor == null)
                            {
                                Console.WriteLine("Doctor not found.");
                                break;
                            }
                            selectedDepartment.Doctors.Add(doctor);
                            Console.WriteLine($"Doctor {doctor.Name} has been assigned to Department {selectedDepartment.DepName} and Clinic {selectedClinic.Name}.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case 2:
                            while (true) { 
                            Console.Clear();
                            Console.WriteLine("==Add Patient==");
                            // Patient ID
                            int patientId;
                            while (true)
                            {
                                Console.Write("Enter Patient ID (at least 6 digits): ");
                                if (!int.TryParse(Console.ReadLine(), out patientId) || patientId.ToString().Length < 6)
                                {
                                    Console.WriteLine("Invalid Patient ID. It must be a number with at least 6 digits.");
                                    continue;
                                }

                                // Check if ID already exists
                                bool idExists = users.Any(u => u is Patient && u.UserId == patientId);
                                if (idExists)
                                {
                                    Console.WriteLine("This Patient ID already exists. Please enter a unique ID.");
                                    continue;
                                }
                                
                                break;
                            }

                            // Patient Name
                            string patientName;
                            while (true)
                            {
                                Console.Write("Enter Patient Name (letters only): ");
                                patientName = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrWhiteSpace(patientName) && patientName.All(char.IsLetter))
                                    break;
                                Console.WriteLine("Invalid Patient Name. It must contain letters only.");
                            }

                            // Patient Email
                            string patientEmail;
                            while (true)
                            {
                                Console.Write("Enter Patient Email(e.g. example@domain.com): ");
                                patientEmail = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrWhiteSpace(patientEmail) && patientEmail.Contains("@") && patientEmail.Contains("."))
                                    break;
                                Console.WriteLine("Invalid Email format.");
                            }

                            // Patient Phone
                            string patientPhone;
                            while (true)
                            {
                                Console.Write("Enter Patient Phone Number: ");
                                patientPhone = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrWhiteSpace(patientPhone) && patientPhone.All(char.IsDigit) && patientPhone.Length >= 8)
                                    break;
                                Console.WriteLine("Invalid Phone Number. It must contain at least 8 digits.");
                            }

                            // Password
                            string patientPassword;
                            while (true)
                            {
                                Console.Write("Enter Patient Password (at least 8 characters, with a letter, number, and symbol): ");
                                patientPassword = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(patientPassword) &&
                                    patientPassword.Length >= 8 &&
                                    patientPassword.Any(char.IsLetter) &&
                                    patientPassword.Any(char.IsDigit) &&
                                    patientPassword.Any(ch => !char.IsLetterOrDigit(ch)))
                                {
                                    break;
                                }
                                Console.WriteLine("Invalid Password. It must be at least 8 characters and contain a letter, number, and symbol.");
                            }

                            // Gender
                            string gender = " ";
                            while (true)
                            {
                                Console.WriteLine("Select Gender:");
                                Console.WriteLine("1. Male");
                                Console.WriteLine("2. Female");
                                Console.Write("Enter choice (1 or 2): ");
                                string genderChoice = Console.ReadLine()?.Trim();

                                if (genderChoice == "1")
                                {
                                    gender = "Male";
                                    break;
                                }
                                if (genderChoice == "2")
                                {
                                    gender = "Female";
                                    break;
                                }
                                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                            }

                            // Date of Birth
                            DateTime dob;
                            while (true)
                            {
                                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out dob))
                                    break;
                                Console.WriteLine("Invalid date format.");
                            }

                            // Address
                            string address;
                            while (true)
                            {
                                Console.Write("Enter Address (letters only): ");
                                address = Console.ReadLine()?.Trim();
                                if (!string.IsNullOrWhiteSpace(address) && address.All(ch => char.IsLetter(ch) || ch == ' '))
                                    break;
                                Console.WriteLine("Invalid Address. It must contain only letters and no numbers.");
                            }


                            Patient newPatient = new Patient(
                             patientId,
                             patientName,
                             patientEmail,
                             patientPassword,  
                             patientPhone,
                             gender,
                             dob,
                             address
                              );

                            users.Add(newPatient);
                            Console.WriteLine("Patient Add successfully");
                            Console.WriteLine("\nDo you want to:");
                            Console.WriteLine("1. Add another patient");
                            Console.WriteLine("2. Return to Admin Menu");
                            Console.Write("Enter your choice: ");
                            string choice = Console.ReadLine();

                            if (choice == "2")
                                break;
                    }
                    break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("==Update Or Delete Doctor");
                            var doctorsList = users.OfType<Doctor>().ToList();
                            if (doctorsList.Count == 0)
                            {
                                Console.WriteLine("No Docter Found.");
                                break;
                            }
                            Console.WriteLine("\nAvailable Doctors:");
                            foreach (var d in doctorsList)
                                Console.WriteLine($"ID:{d.UserId},Name{d.Name},Specialization: {d.Specialization}");

                            Doctor selectedDoctor = null;

                            while (true)
                            {
                                Console.Write("Enter Doctor ID to update/delete: ");
                                if (!int.TryParse(Console.ReadLine(), out int updateDocId))
                                {
                                    Console.WriteLine(" Invalid input. ID must be a number.");
                                    continue;
                                }

                                selectedDoctor = doctorsList.FirstOrDefault(d => d.UserId == updateDocId);
                                if (selectedDoctor == null)
                                {
                                    Console.WriteLine($" No doctor found with ID {updateDocId}. Try again.");
                                    continue;
                                }

                                break;
                            }

                            int updateChoice;
                            while (true)
                            {
                                Console.WriteLine("Select an option:");
                                Console.WriteLine("\n1. Update Doctor\n2. Delete Doctor");
                                if (!int.TryParse(Console.ReadLine(), out updateChoice) || (updateChoice != 1 && updateChoice != 2))
                                {
                                    Console.WriteLine(" Invalid input. Please enter 1 to update or 2 to delete.");
                                    continue;
                                }
                                break;
                            }

                                if (updateChoice == 1)
                                {
                                    string newName;
                                    while (true)
                                    {
                                        Console.Write("Enter new name: ");
                                        newName = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(newName))
                                            break;
                                        Console.WriteLine(" Name is required. Please enter a valid name.");
                                    }
                                    selectedDoctor.Name = newName;

                                    string newSpec;
                                    while (true)
                                    {
                                        Console.Write("Enter new specialization: ");
                                        newSpec = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(newSpec))
                                            break;
                                        Console.WriteLine(" Specialization is required. Please enter a valid specialization.");
                                    }
                                    selectedDoctor.Specialization = newSpec;

                                    string newNumber;
                                    while (true)
                                    {
                                        Console.Write("Enter new Phone Number (8 digits): ");
                                        newNumber = Console.ReadLine();

                                        if (!string.IsNullOrWhiteSpace(newNumber) &&
                                            newNumber.Length == 8 &&
                                            newNumber.All(char.IsDigit))
                                        {
                                            break;
                                        }

                                        Console.WriteLine(" Invalid phone number. It must be exactly 8 digits.");
                                    }

                                    selectedDoctor.PhoneNumber = newNumber;
                                    Console.WriteLine("Doctor information updated successfully.");
                                }
                                else if (updateChoice == 2)
                                {
                                    Console.WriteLine("Are you sure you want to delete this doctor? (yes/no)");
                                    string confirm = Console.ReadLine().ToLower();
                                    if (confirm == "yes")
                                    {
                                        users.Remove(selectedDoctor);
                                        Console.WriteLine("Doctor deleted successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Deletion cancelled.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice.");
                                }
                            
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;


                        case 4:
                            var doctors = users.OfType<Doctor>().ToList();

                            if (doctors.Count() == 0)
                            {
                                Console.WriteLine("No doctors available.");
                                Console.WriteLine("Press any key to return to menu.");
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine("Select a doctor to add appointments:");
                            for (int i = 0; i < doctors.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1}. {doctors[i].Name} - {doctors[i].Specialization}");
                            }

                            Console.Write("Enter doctor number: ");
                            if (!int.TryParse(Console.ReadLine(), out int docIndex) || docIndex < 1 || docIndex > doctors.Count())
                            {
                                Console.WriteLine("Invalid selection.");
                                Console.ReadKey();
                                return;
                            }

                            Doctor selDoctor = doctors[docIndex - 1];

                            Console.Write("How many appointments do you want to add? ");
                            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                            {
                                Console.WriteLine("Invalid number.");
                                Console.ReadKey();
                                return;
                            }

                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"Enter appointment date and time (e.g., 2025-08-01 10:00): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime appointment))
                                {
                                    selDoctor.AvailableAppointments.Add(appointment);
                                    Console.WriteLine("Appointment added.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format.");
                                    Console.ReadKey();
                                }
                            }
                            Console.WriteLine("Appointments added successfully.");
                            Console.ReadKey();


                            break;

                        case 5:
                            Console.Clear();
                            Console.WriteLine("==Book Appointments For Patient==");
                            var doctorList = users.OfType<Doctor>().ToList();
                            if (doctorList.Count == 0)
                            {
                                Console.WriteLine("No doctors available.");
                                break;
                            }
                            Console.WriteLine("Available Doctors:");
                            for (int i = 0; i < doctorList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {doctorList[i].Name} - {doctorList[i].Specialization}");
                            }

                            Doctor selectedDoctorForAppointment;
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out int doctorChoice) &&
                                doctorChoice >= 1 && doctorChoice <= doctorList.Count)
                                {
                                    selectedDoctorForAppointment = doctorList[doctorChoice - 1];
                                    break;
                                }
                                Console.WriteLine("Invalid doctor selection. Please try again.");
                            }

                            DateTime appointmentDateTime;
                            while (true)
                            {
                                Console.Write("Enter appointment date and time (e.g. 2025-08-05 14:30): ");
                                string input = Console.ReadLine();

                                if (DateTime.TryParse(input, out appointmentDateTime))
                                {
                                    if (appointmentDateTime < DateTime.Now)
                                    {
                                        Console.WriteLine("Appointment cannot be in the past.");
                                        continue;
                                    }
                                    break;
                                }
                                Console.WriteLine("Invalid date/time format. Please use yyyy-MM-dd HH:mm.");
                            }

                            Console.Write("Enter Patient ID to book this appointment: ");
                            if (!int.TryParse(Console.ReadLine(), out int PatientId))
                            {
                                Console.WriteLine("Invalid Patient ID.");
                                break;
                            }

                            Patient patient = users.OfType<Patient>().FirstOrDefault(p => p.UserId == PatientId);
                            if (patient == null)
                            {
                                Console.WriteLine("Patient not found.");
                                break;
                            }

                            if (patient.BookedAppointments != null && patient.BookedAppointments.Contains(appointmentDateTime))
                            {
                                Console.WriteLine("This patient has already booked this appointment.");
                                break;
                            }
    
                            if (patient.BookedAppointments == null)
                                patient.BookedAppointments = new List<DateTime>();

                            if (selectedDoctorForAppointment.AvailableAppointments == null)
                                selectedDoctorForAppointment.AvailableAppointments = new List<DateTime>();

                            if (!selectedDoctorForAppointment.AvailableAppointments.Contains(appointmentDateTime))
                            {
                                selectedDoctorForAppointment.AvailableAppointments.Add(appointmentDateTime);
                            }

                            patient.BookedAppointments.Add(appointmentDateTime);
                            selectedDoctorForAppointment.AvailableAppointments.Remove(appointmentDateTime);

                            Console.WriteLine($"Appointment on {appointmentDateTime} booked for Patient {patient.Name}.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;

                        case 6:
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("==View Menu==");
                                    Console.WriteLine("1. View All Departments and Clinics");
                                    Console.WriteLine("2. View All Doctors");
                                    Console.WriteLine("3. View All Appointments");
                                    Console.WriteLine("4. View Patients");
                                    Console.WriteLine("0. Back to Admin Menu");
                                    Console.Write("Select an option: ");
                                    string viewChoice = Console.ReadLine();

                                    switch (viewChoice)
                                    {
                                        case "1":
                                            Console.Clear();
                                            Console.WriteLine("All Branches,Departments and Clinics:");
                                            //var branches = Branch.GetAllBranches();
                                            if (branches == null || branches.Count == 0)
                                            {
                                                Console.WriteLine("NO Branches Available");

                                            }
                                            else
                                            {
                                                foreach (var br in branches)
                                                {
                                                    Console.WriteLine($"Branch:{br.BranchName}");
                                                    foreach (var dep in br.Departments)
                                                    {
                                                        Console.WriteLine($"\tDepartment: {dep.DepName}");
                                                        foreach (var clinic in dep.Clinics)
                                                        {
                                                            Console.WriteLine($"\t\tClinic: {clinic.Name}, Active: {clinic.IsActive}");
                                                        }
                                                    }

                                                }
                                            }
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;

                                        case "2":
                                            Console.Clear();
                                            Console.WriteLine("All Doctors:");

                                            var allDoctors = users.OfType<Doctor>().ToList();

                                            if (allDoctors == null || allDoctors.Count == 0)
                                            {
                                                Console.WriteLine("No Doctors Available");
                                                break;
                                            }
                                            else
                                            {
                                                foreach (var Doctor in allDoctors)
                                                {
                                                    Console.WriteLine($"Doctor ID: {Doctor.UserId}, Name: {Doctor.Name}, Email: {Doctor.Email}, Specialization: {Doctor.Specialization}, Phone: {Doctor.PhoneNumber}");
                                                }
                                            }
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;

                                        case "3":
                                            Console.Clear();
                                            Console.WriteLine("All Appointments:");
                                            var Doctors = users.OfType<Doctor>().ToList();
                                            foreach (var doc in Doctors)
                                            {
                                                Console.WriteLine($"\nDr. {doc.Name}'s Appointments:");
                                                if (doc.AvailableAppointments == null || doc.AvailableAppointments.Count == 0)
                                                {
                                                    Console.WriteLine("\tNo appointments.");
                                                    continue;
                                                }
                                                foreach (var app in doc.AvailableAppointments)
                                                {
                                                    Console.WriteLine($"\t- {app}");
                                                }
                                            }
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;

                                        case "4":
                                            Console.Clear();
                                            Console.WriteLine("All Patients:");

                                            var allPatients = users.OfType<Patient>().ToList();

                                            if (allPatients == null || allPatients.Count == 0)
                                            {
                                                Console.WriteLine("No Patients Available");
                                                break;
                                            }
                                            else
                                            {
                                                foreach (var Patient in allPatients)
                                                {
                                                    Console.WriteLine($"Patient ID: {Patient.UserId}, Name: {Patient.Name}, Email: {Patient.Email}, Phone: {Patient.PhoneNumber}");
                                                }
                                            }
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                            break;

                                        case "0":
                                            return;

                                        default:
                                            Console.WriteLine("Invalid choice. Please try again.");
                                            Console.ReadKey();
                                            break;


                                    }

                                }
                            }

                                
                            

                            break;
                        case 0:
                            return; // Exit Admin menu

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            Console.ReadKey();
                            break;
                    }
                }
                

            }
        }
        public static void PatientMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Patient Menu ==");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterPatient();
                        break;

                    case "2":
                        Console.Write("Enter your email: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();

                        var patient = users.OfType<Patient>()
                            .FirstOrDefault(p => p.Email == email && p.Password == password);

                        if (patient != null)
                        {
                            Console.WriteLine("Login successful! Press any key to continue...");
                            Console.ReadKey();
                            PatientLoginMenu(patient);  // Enter patient sub-menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid Email or password");
                            Console.ReadKey();
                        }
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void PatientLoginMenu(Patient patient)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"== Welcome, {patient.Name} ==");
                Console.WriteLine("1. View My Appointments");
                Console.WriteLine("2. View My Medical Reports");
                Console.WriteLine("3. Logout"); // logout to patient login screen
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("== Your Booked Appointments ==");

                        if (patient.BookedAppointments.Any())
                        {
                            foreach (var appointment in patient.BookedAppointments)
                            {
                                Console.WriteLine($"- {appointment}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no booked appointments.");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;


                    case "2":
                        Console.Clear();
                        Console.WriteLine("== Your Medical Reports ==");
                        var reports = recordService.GetAllRecords()
                            .Where(r => r.Patient.UserId == patient.UserId).ToList();

                        if (!reports.Any())
                        {
                            Console.WriteLine("No reports found.");
                        }
                        else
                        {
                            foreach (var r in reports)
                            {
                                Console.WriteLine(r.ToString());
                            }
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("Logging out...");
                        return; // returns to login

                    default:
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void RegisterPatient()
        {
            Console.Clear();
            Console.WriteLine("== Register New Patient ==");

            int userId;
            while (true)
            {
                Console.Write("Enter Desired Patient ID: (number, at least 6 digits): ");
                string inputId = Console.ReadLine();
                if (!int.TryParse(inputId, out userId))
                {
                Console.WriteLine("Invalid ID format.Please enter numbers only.");
                    continue;
            }
                if (inputId.Length < 6)
                {
                    Console.WriteLine("ID must be at least 6 digits long.");
                    continue;
                }
                if (users.Any(u => u.UserId == userId))
                {
                    Console.WriteLine("This Patient ID is already taken. Please try a different one.");
                    continue;
                }
                break;
            }

            string name;
            while (true)
            {
                Console.Write("Enter your Name (letters only): ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || !name.All(char.IsLetter))
                {
                    Console.WriteLine("Name must contain letters only.");
                    continue;
                }
                break;
            }
            string email;
            while (true)
            {
                Console.Write("Enter Email: ");
                email = Console.ReadLine();
                if (!ValidationHelper.IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email format.");
                    continue;
                }
                break;
            }
            string password;
            while (true)
            {
                Console.Write("Enter Password (must contain letters, numbers, and at least one symbol): ");
                password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password) ||
                    !password.Any(char.IsLetter) ||
                    !password.Any(char.IsDigit) ||
                    !password.Any(ch => !char.IsLetterOrDigit(ch)))
                {
                    Console.WriteLine("Password must contain letters, numbers, and at least one symbol.");
                    continue;
                }
                break;
            }
            string phone;
            while (true)
            {
                Console.Write("Enter Phone Number (at least 8 digits): ");
                phone = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phone) || phone.Length < 8 || !phone.All(char.IsDigit))
                {
                    Console.WriteLine("Phone number must be at least 8 digits and contain digits only.");
                    continue;
                }
                break;
            }
            string gender;
            while (true)
            {
                Console.Write("Enter Gender (Male/Female): ");
                gender = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(gender))
                {
                    Console.WriteLine("Gender cannot be empty.");
                    continue;
                }

                string genderLower = gender.ToLower();

                if (genderLower != "male" && genderLower != "female" && genderLower != "other")
                {
                    Console.WriteLine("Invalid gender. Please enter Male, Female, or Other.");
                    continue;
                }
                break;
            }
            string address;
            while (true)
            {
                Console.Write("Enter Address (letters and spaces only): ");
                address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address) || address.Any(ch => !char.IsLetter(ch) && !char.IsWhiteSpace(ch)))
                {
                    Console.WriteLine("Address must contain letters and spaces only.");
                    continue;
                }
                break;
            }

            DateTime dob;
            while (true)
            {
                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                string inputDob = Console.ReadLine();
                if (!DateTime.TryParse(inputDob, out dob))
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    continue;
                }
                break;
            }

            Patient newPatient = new Patient(userId, name, email, password, phone, gender, dob, address);
            users.Add(newPatient);

            Console.WriteLine($"Registration successful! Your Patient ID is: {newPatient.UserId}");
            Console.ReadKey();
        }
        public static void DoctorMenu(Doctor loggedInDoctor)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"== Doctor Menu: == ");
                Console.WriteLine("1. View My Appointments");
                Console.WriteLine("2. Add Medical Report");
                Console.WriteLine("3. View My Patients Reports");
                Console.WriteLine("4. Update Medical Report");
                // back to main menu
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("== Your Available Appointments ==");
                        if (loggedInDoctor.AvailableAppointments.Count == 0)
                        {
                            Console.WriteLine("No appointments available.");
                        }
                        else
                        {
                            foreach (var app in loggedInDoctor.AvailableAppointments)
                            {
                                var matchedPatient = users
                               .OfType<Patient>()
                               .FirstOrDefault(p => p.BookedAppointments != null && p.BookedAppointments.Contains(app));

                                if (matchedPatient != null)
                                {
                                    Console.WriteLine($"- {app} (Booked by: {matchedPatient.Name}, ID: {matchedPatient.UserId})");
                                }
                                else
                                {
                                    Console.WriteLine($"- {app} (Available)");
                                }
                            }
                        
                        }
                         
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();

                        break;

                    case "2":
                        Console.Write("Enter Patient ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int patientId))
                        {
                            Console.WriteLine("Invalid Patient ID");
                            Console.ReadKey();
                            break;
                        }

                        Patient patient = superAdmin.UsersList.OfType<Patient>().FirstOrDefault(p => p.UserId == patientId);
                        if (patient == null)
                        {
                            Console.WriteLine("Patient not found.");
                            Console.ReadKey();
                            break;
                        }

                        Console.Write("Enter Diagnosis: ");
                        string diagnosis = Console.ReadLine();

                        Console.Write("Enter Treatment: ");
                        string treatment = Console.ReadLine();

                        Console.Write("Enter Notes: ");
                        string notes = Console.ReadLine();

                        recordService.AddRecord(patient, loggedInDoctor, diagnosis, treatment, notes);
                        Console.ReadKey();
                        break;

                    case "3":
                        var records = recordService.GetAllRecords()
                            .Where(r => r.Doctor.UserId == loggedInDoctor.UserId)
;

                        if (!records.Any())
                        {
                            Console.WriteLine("No reports found.");
                        }
                        else
                        {
                            foreach (var record in records)
                            {
                                Console.WriteLine(record.ToString());
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Write("Enter Patient ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int userId))
                        {
                            Console.WriteLine("Invalid Patient ID");
                            Console.ReadKey();
                            break;
                        }

                        var patientRecords = recordService.GetAllRecords()
                       .Where(r => r.Patient.UserId == userId)
                       .ToList();

                        if (patientRecords.Count == 0)
                        {
                            Console.WriteLine("No records found for this patient.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("== Patient Medical Records ==");
                        foreach (var rec in patientRecords)
                        {
                            Console.WriteLine($"Record ID: {rec.RecordId} | Diagnosis: {rec.Diagnosis} | Treatment: {rec.Treatment} | Notes: {rec.Notes}");
                        }

                        Console.Write("Enter Record ID to update: ");
                        if (!int.TryParse(Console.ReadLine(), out int recordId))
                        {
                            Console.WriteLine("Invalid Record ID");
                            Console.ReadKey();
                            break;
                        }

                        var recordToUpdate = patientRecords.FirstOrDefault(r => r.RecordId == recordId);
                        if (recordToUpdate == null)
                        {
                            Console.WriteLine("Record not found for this patient.");
                            Console.ReadKey();
                            break;
                        }


                        Console.WriteLine($"Current Diagnosis: {recordToUpdate.Diagnosis}");
                        Console.WriteLine($"Current Treatment: {recordToUpdate.Treatment}");
                        Console.WriteLine($"Current Notes: {recordToUpdate.Notes}");

                        Console.Write("Enter new Diagnosis: ");
                        string newDiagnosis = Console.ReadLine();

                        Console.Write("Enter new Treatment: ");
                        string newTreatment = Console.ReadLine();

                        Console.Write("Enter new Notes: ");
                        string newNotes = Console.ReadLine();

                        if (recordService.UpdateRecord(recordId, newDiagnosis, newTreatment, newNotes))
                        {
                            Console.WriteLine("Record updated successfully.");
                            Console.WriteLine("== Updated Record ==");
                            Console.WriteLine($"Record ID: {recordId}");
                            Console.WriteLine($"Diagnosis: {newDiagnosis}");
                            Console.WriteLine($"Treatment: {newTreatment}");
                            Console.WriteLine($"Notes: {newNotes}");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update record.");
                        }
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Console.ReadKey();
                        break;

                    case "0":
                        return; // Back to main menu
                }
            }
        }
    }
}




