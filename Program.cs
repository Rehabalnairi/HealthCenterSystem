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
                        Console.WriteLine("You are logged in as Doctor.");
                        Console.WriteLine("Doctor functionality coming soon...");
                        break;

                    case 4:
                        Console.Clear();
                        PatientService patientService = new PatientService();
                        bool patientMenuActive = true;

                        while (patientMenuActive)
                        {
                            Console.WriteLine("Patient Menu:");
                            Console.WriteLine("1. Register");
                            Console.WriteLine("2. Login");
                            Console.WriteLine("0. Exit to Main Menu");
                            Console.Write("Enter your choice: ");
                            int patientChoice = Convert.ToInt32(Console.ReadLine());

                            Patient LoginPatient(string email, string password, List<User> users)
                            {
                                return users.OfType<Patient>().FirstOrDefault(u =>
                                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                                    u.Password == password);
                            }

                            switch (patientChoice)
                            {
                                case 1: // Registration
                                    Console.Write("Enter your name: ");
                                    string name = Console.ReadLine();

                                    Console.Write("Enter your email: ");
                                    string email = Console.ReadLine();

                                    Console.Write("Enter your password: ");
                                    string password = Console.ReadLine();

                                    Console.Write("Enter your phone number: ");
                                    string phone = Console.ReadLine();

                                    Console.Write("Enter your gender: ");
                                    string gender = Console.ReadLine();

                                    Console.Write("Enter your date of birth (yyyy-mm-dd): ");
                                    DateTime dateOfBirth;
                                    while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                                    {
                                        Console.Write("Invalid date format. Please enter again (yyyy-mm-dd): ");
                                    }

                                    Console.Write("Enter your address: ");
                                    string address = Console.ReadLine();

                                    int newId = users.Count + 1;

                                    Patient newPatient = new Patient(newId, name, email, password, phone, gender, dateOfBirth, address);
                                    users.Add(newPatient);

                                    patientService.AddPatient(newId, name, email, password, phone, gender, dateOfBirth, address);

                                    Console.WriteLine("Patient registered successfully.");

                                    switch (pMenu)
                                    {
                                        case 1:

                                        AppointmentService appService = new AppointmentService();
                                        var appointments = appService.GetAppointmentsByPatient(loggedPatient.UserId);

                                            foreach (var app in appointments)

                                                Console.WriteLine(app.ToString());
                                    }
                                    break;


                                    case 2:
                                    PatientRecordService recordService = new PatientRecordService();
                                    var records = recordService.GetRecordsByPatient(loggedPatient);

                                    foreach (var record in records)
                                    {
                                        Console.WriteLine(record.ToString());
                                    }
                                    break;
                                    }
                                        case 2: // Login
                                            Console.Write("Enter email: ");
                                            string pemail = Console.ReadLine();
                                            Console.Write("Enter password: ");
                                            string ppassword = Console.ReadLine();

                                            var loggedPatient = LoginPatient(pemail, ppassword, users);
                                            if (loggedPatient != null)
                                            {
                                                Console.WriteLine("Login successful!");
                                                ShowPatientActions(); // Display patient actions after login
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid email or password.");
                                            }
                                            break;

                                        case 0:
                                            patientMenuActive = false; // Exit patient menu
                                            break;

                                        default:
                                            Console.WriteLine("Invalid choice. Please try again.");
                                            break;
                                    }
                            }
                            break;
                        }

                        //SuperAdmin menu
                        void SuperAdminMenu(List<Branch> branches, List<Clinic> clinics)
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
                                            if (string.IsNullOrWhiteSpace(adminName) || string.IsNullOrWhiteSpace(adminPassword) || string.IsNullOrWhiteSpace(adminPhoneNumber))
                                            {
                                                Console.WriteLine("Admin Name, Password, and Phone Number cannot be empty. Please try again.");
                                                continue;
                                            }
                                            string adminEmail = superAdmin.AddAdmin(adminId, adminName, adminPassword, adminPhoneNumber);
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

                                                    superAdmin.AddBranch(branchName, branchLocation, noOfFloors, noOfRooms, "", "");
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
                            //Console.WriteLine("5. View All Departments and Clinic");
                            // Console.WriteLine("2. View All Doctors");
                            // Console.WriteLine("7. View All Appointments");
                            //Console.WriteLine("9. View Patients");
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

                                        break;
                                    case 3:

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

                                        break;
                                    case 0:
                                        return; // Exit Admin menu
                                    default:
                                        Console.WriteLine("Invalid choice. Please try again.");
                                        break;
                                }
                            }
                        }
                }



             public static void DoctorMenu(Doctor doctor)
              {
               while (true)
               {
                Console.Clear();
                Console.WriteLine($"== Doctor Menu: Dr. {doctor.Name} ==");
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

                        recordService.AddRecord(patient, doctor, diagnosis, treatment, notes);
                        Console.ReadKey();
                        break;
                    case "3":
                        var records = recordService.GetAllRecords()
                            .Where(r => r.Doctor.UserId == doctor.UserId).ToList();

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

