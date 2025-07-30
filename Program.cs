using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using HealthCenterSystem.Models;
using HealthCenterSystem.Services;

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
                            Console.WriteLine($"Welcome Dr. {foundDoctor.Name}");
                            Console.ReadKey();
                            DoctorMenu(foundDoctor);

                        }

                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Patient Menu:");
                        Console.WriteLine("1. Register");
                        Console.WriteLine("2. Login");
                        Console.WriteLine("0. Back to main menu");

                        Console.Write("Enter your choice: ");
                        int patientChoice = int.Parse(Console.ReadLine());

                        switch (patientChoice)
                        {
                            case 1: // Registration
                                Console.Write("Enter your name: ");
                                string pname = Console.ReadLine();

                                Console.Write("Enter your email: ");
                                string pemail = Console.ReadLine();

                                Console.Write("Enter password: ");
                                string ppassword = Console.ReadLine();

                                Console.Write("Enter your phone number: ");
                                string pphone = Console.ReadLine();

                                PatientService patientService = new PatientService();
                                int newPatientId = users.Count + 1;

                                //patientService.AddPatient(newPatientId, pname, pemail, ppassword, pphone);
                                users.Add(new Patient(newPatientId, pname, pemail, ppassword, pphone, "Unknown", DateTime.Now, "Unknown"));
                                Console.WriteLine("Registration successful.");
                                Console.ReadKey();
                                break;

                            case 2: // Login
                                Console.Write("Enter your email: ");
                                string loginEmail = Console.ReadLine();

                                Console.Write("Enter your password: ");
                                string loginPassword = Console.ReadLine();

                                var loggedPatient = users.FirstOrDefault(u => u.Email == loginEmail && u.Password == loginPassword && u.Role == "Patient") as Patient;

                                if (loggedPatient != null)
                                {
                                    Console.WriteLine($"Welcome {loggedPatient.Name}!");
                                    // Display patient dashboard
                                    int pMenu = -1;
                                    while (pMenu != 0)
                                    {
                                        Console.WriteLine("\nPatient Dashboard:");
                                        Console.WriteLine("1. View Appointments");
                                        Console.WriteLine("2. View Medical Reports");
                                        Console.WriteLine("0. Logout");

                                        Console.Write("Choice: ");
                                        int.TryParse(Console.ReadLine(), out pMenu);

                                        switch (pMenu)
                                        {
                                            case 1:

                                            //AppointmentService appService = new AppointmentService();
                                            //var appointments = appService.GetAppointmentsByPatient(loggedPatient.UserId);

                                            //foreach (var app in appointments)
                                            //{
                                            //    Console.WriteLine(app.ToString());
                                            //}
                                            //break;

                                            case 2:
                                                //PatientRecordService recordService = new PatientRecordService();
                                                //var records = recordService.GetRecordsByPatient(loggedPatient);

                                                //foreach (var record in records)
                                                //{
                                                //    Console.WriteLine(record.ToString());
                                                //}
                                                break;

                                            case 0:
                                                Console.WriteLine("Logging out...");
                                                break;

                                            default:
                                                Console.WriteLine("Invalid option.");
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Login failed. Invalid credentials.");
                                }

                                Console.ReadKey();
                                break;

                            case 0:
                                break;

                            default:
                                Console.WriteLine("Invalid option.");
                                break;
                        }
                        break;




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

                             bool idExists = superAdmin.UsersList
                            .OfType<Doctor>()
                            .Any(d => d.UserId == doctorId);

                            if (idExists)
                            {
                                Console.WriteLine("This doctor ID is already in use.");
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

                            Console.Write("Enter specialization: ");
                            string specialization = Console.ReadLine();
                            

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
                                Console.WriteLine("0. Exit Branch Management");

                                if (!int.TryParse(Console.ReadLine(), out branchOption))
                                {
                                    Console.WriteLine("Invalid input. Please enter a number between 0 and 5.");
                                    continue;
                                }
                                switch (branchOption)
                                {
                                    case 1:
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

                                        superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms, "", "");
                                        Console.WriteLine("Branch added successfully.");
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

                                        Console.Write("Enter New Branch Name: ");
                                        string newName = Console.ReadLine();

                                        Console.Write("Enter New Location: ");
                                        string newLocation = Console.ReadLine();

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
                                        Console.Write("Enter Branch ID to delete: ");
                                        if (!int.TryParse(Console.ReadLine(), out int deleteId))
                                        {
                                            Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                                        }
                                        else
                                        {
                                            if (superAdmin.RemoveBranch(deleteId))
                                            {
                                                Console.WriteLine("Branch deleted successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Branch not found.");
                                            }
                                        }
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;

                                    case 4:
                                        int branchId;
                                        while (true)
                                        {
                                            Console.Write("Enter Branch ID to add department(s): ");
                                            string inputBranchId = Console.ReadLine();

                                            if (!int.TryParse(inputBranchId, out branchId))
                                            {
                                                Console.WriteLine("Invalid ID. Please enter a numeric Branch ID");
                                                continue;
                                            }

                                            // Find the branch by ID
                                            var branch = superAdmin.BranchesList.FirstOrDefault(b => b.BranchId == branchId);
                                            if (branch == null)
                                            {
                                                Console.WriteLine("Branch not found.");
                                                Console.Write("Do you want to enter another Branch ID? (Y/N): ");
                                                string choice = Console.ReadLine().Trim().ToUpper();
                                                if (choice == "Y")
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            {

                                                int deptCount;
                                                while (true)
                                                {
                                                    Console.Write("How many departments do you want to add? ");
                                                    string inputDeptCount = Console.ReadLine();

                                                    if (!int.TryParse(inputDeptCount, out deptCount) || deptCount <= 0)
                                                    {
                                                        Console.WriteLine("Invalid number of departments. Must be a positive integer");
                                                        continue;
                                                    }
                                                    break;
                                                }

                                                for (int i = 0; i < deptCount; i++)
                                                {
                                                    Console.Write($"Enter name of department #{i + 1}: ");
                                                    string deptName = Console.ReadLine()?.Trim();
                                                    if (string.IsNullOrWhiteSpace(deptName) || deptName.Any(char.IsDigit))
                                                    {
                                                        Console.WriteLine("Invalid department name. It must contain letters only. Skipping this department.");
                                                        continue;
                                                    }

                                                    Department newDept = new Department
                                                    {
                                                        DepName = deptName,
                                                        Clinics = new List<Clinic>()
                                                    };

                                                    int clinicCount;
                                                    while (true)
                                                    {
                                                        Console.Write($"How many clinics in department '{deptName}'? ");
                                                        string clinicName = Console.ReadLine();

                                                        if (string.IsNullOrWhiteSpace(clinicName) || clinicName.Any(char.IsDigit))
                                                        {
                                                            Console.WriteLine("Invalid clinic name. It must contain letters only. Skipping this clinic.");
                                                            continue;
                                                        }
                                                    }

                                                    for (int j = 0; j < clinicCount; j++)
                                                    {
                                                        Console.Write($"\tEnter name of clinic #{j + 1} in department '{deptName}': ");
                                                        string clinicName = Console.ReadLine()?.Trim();

                                                        if (string.IsNullOrWhiteSpace(clinicName))
                                                        {
                                                            Console.WriteLine("Clinic name cannot be empty. Skipping this clinic.");
                                                            continue;
                                                        }

                                                        Clinic newClinic = new Clinic
                                                        {
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
                                            }
                                        }
                                        break;

                                    case 5:
                                        superAdmin.ViewBranches();
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;

                                    case 0:
                                        return; // Exit Branch Management menu

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
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
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


            void AdminMenu()
            {
                Console.Clear();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Assign Exisiting Doctoer to Department and clinic");

                Console.WriteLine("2. Update Or Delete Doctor");
                Console.WriteLine("3. Add Appointment");
                Console.WriteLine("4. Book Appointments For Patient");
                Console.WriteLine("5.Views");
                Console.WriteLine("0. Exit Admin Menu");

                int adminChoice = -1;
                while (adminChoice != 0)
                {
                    Console.Write("Select an option: ");
                    if (!int.TryParse(Console.ReadLine(), out adminChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
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
                                Console.WriteLine($"{clinic.Name}");
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
                            Console.WriteLine("Enter Doctor ID to update/delete:");
                            if (!int.TryParse(Console.ReadLine(), out int updateDocId))
                            {
                                Console.WriteLine("Invaild input.");
                                break;
                            }
                            var selectedDoctor = doctorsList.FirstOrDefault(d => d.UserId == updateDocId);
                            if (selectedDoctor == null)
                            {
                                Console.WriteLine("Doctor Not Found");
                                break;
                            }
                            Console.WriteLine("Select an option:");
                            Console.WriteLine("\n1. Update Doctor\n2. Delete Doctor");
                            if (!int.TryParse(Console.ReadLine(), out int updateChoice))
                            {
                                Console.WriteLine("Invaild input");
                                break;
                            }
                            if (updateChoice == 1)
                            {
                                Console.WriteLine("Enter new name (or press Enter to skip): ");
                                string newName = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newName))
                                    selectedDoctor.Name = newName;
                                Console.WriteLine("Enter new specialization (or press Enter to skip): ");
                                string newSpec = Console.ReadLine();
                                if (!string.IsNullOrEmpty(newSpec))
                                    selectedDoctor.Specialization = newSpec;
                                Console.WriteLine("Enter new Phone Number (or press Enter to skip):");
                                string newNumber = Console.ReadLine();
                                if (string.IsNullOrEmpty(newNumber))
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
                        case 3:
                            var doctors = users.OfType<Doctor>().ToList();

                            if (doctors.Count() == 0)
                            {
                                Console.WriteLine("No doctors available.");
                                return;
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
                                return;
                            }

                            Doctor selDoctor = doctors[docIndex - 1];

                            Console.Write("How many appointments do you want to add? ");
                            if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                            {
                                Console.WriteLine("Invalid number.");
                                return;
                            }

                            for (int i = 0; i < count; i++)
                            {
                                Console.Write($"Enter appointment date and time (e.g., 2025-08-01 10:00): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime appointment))
                                {
                                    selDoctor.AvailableAppointments.Add(appointment);
                                    Console.WriteLine("Appointment added.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format.");
                                }
                            }
                            Console.WriteLine("Appointments added successfully.");
                            Console.ReadKey();


                            break;
                        case 4:
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
                                            Console.WriteLine("All Departments and Clinics:");
                                            var branches = Branch.GetAllBranches(); // لازم تخزن الفرع هنا
                                            if (branches.Count == 0)
                                            {
                                                Console.WriteLine("NO Branches Available");
                                                break;
                                            }
                                            foreach (var br in branches)
                                            {
                                                Console.WriteLine($"Branch: {br.BranchName} - Location: {br.BranchLocation}");
                                                if (br.Departments == null || br.Departments.Count == 0)
                                                {
                                                    Console.WriteLine("  No Departments Available");
                                                    continue;
                                                }
                                                foreach (var dep in br.Departments)
                                                {
                                                    Console.WriteLine($"  Department: {dep.DepName}");
                                                    if (dep.Clinics == null || dep.Clinics.Count == 0)
                                                    {
                                                        Console.WriteLine("No Clinics Available");
                                                        continue;
                                                    }
                                                    foreach (var clinic in dep.Clinics)
                                                    {
                                                        Console.WriteLine($"    Clinic: {clinic.Name}");
                                                    }
                                                }
                                            }
                                            break;

                                        case "2":
                                            Console.Clear();
                                            Console.WriteLine("All Doctors:");
                                            var allDoctors = users.OfType<Doctor>().ToList();
                                            if (allDoctors.Count == 0)
                                            {
                                                Console.WriteLine("No Doctors Available");
                                                break;
                                            }
                                            foreach (var Doctor in allDoctors)
                                            {
                                                Console.WriteLine($"Doctor ID: {Doctor.UserId}, Name: {Doctor.Name}, Email: {Doctor.Email}, Specialization: {Doctor.Specialization}, Phone: {Doctor.PhoneNumber}");
                                            }
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
                                            break;

                                        case "4":
                                            Console.Clear();
                                            Console.WriteLine("All Patients:");
                                            var allPatients = users.OfType<Patient>().ToList();
                                            if (allPatients.Count == 0)
                                            {
                                                Console.WriteLine("No Patients Available");
                                                break;
                                            }
                                            foreach (var patient in allPatients)
                                            {
                                                Console.WriteLine($"Patient ID: {patient.UserId}, Name: {patient.Name}, Email: {patient.Email}, Phone: {patient.PhoneNumber}");
                                            }
                                            break;

                                        case "0":
                                            return;

                                        default:
                                            Console.WriteLine("Invalid choice. Please try again.");
                                            break;
                                            Console.WriteLine("Press any key to continue...");
                                            Console.ReadKey();
                                    }
                                }
                            }
                                        case 0:
                                        return; // Exit Admin menu
                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        break;
                                    }
                                }

                            }
                    }
                
            
        
                
            
        

                


        public static void DoctorMenu(Doctor loggedInDoctor)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"== Doctor Menu: Dr. {loggedInDoctor.Name} ==");
                Console.WriteLine("1. View My Appointments");
                Console.WriteLine("2. Add Medical Report");
                Console.WriteLine("3. View My Patients Reports");
                Console.WriteLine("4. Update Medical Report");
                Console.WriteLine("5. Logout");
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
                                Console.WriteLine($"- {app}");
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
                        Console.Write("Enter Record ID to update: ");
                        if (!int.TryParse(Console.ReadLine(), out int recordId))
                        {
                            Console.WriteLine("Invalid Record ID");
                            Console.ReadKey();
                            break;
                        }

                        var recordToUpdate = recordService.GetAllRecords().FirstOrDefault(r => r.RecordId == recordId);
                        if (recordToUpdate == null)
                        {
                            Console.WriteLine("Record not found.");
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
                            Console.WriteLine("Record updated successfully.");
                        else
                            Console.WriteLine("Failed to update record.");

                        Console.ReadKey();
                        break;


                    case "5":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
     
    }

}




