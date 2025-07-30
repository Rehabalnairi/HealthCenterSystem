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
                            Console.WriteLine("\nPatient Menu:");
                            Console.WriteLine("1. Register");
                            Console.WriteLine("2. Login");
                            Console.WriteLine("0. Back to Main Menu");
                            Console.Write("Enter your choice: ");
                            int patientChoice;
                            if (!int.TryParse(Console.ReadLine(), out patientChoice))
                            {
                                Console.WriteLine("Invalid input.");
                                continue;
                            }

                            Patient LoginPatient(string email, string password, List<User> users)
                            {
                                return users.OfType<Patient>().FirstOrDefault(u =>
                                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                                    u.Password == password);
                            }

                            switch (patientChoice)
                            {
                                case 1: // Register
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
                                        Console.Write("Invalid date format. Please try again (yyyy-mm-dd): ");
                                    }

                                    Console.Write("Enter your address: ");
                                    string address = Console.ReadLine();

                                    int newId = users.Count + 1;
                                    Patient newPatient = new Patient(newId, name, email, password, phone, gender, dateOfBirth, address);
                                    users.Add(newPatient);
                                    patientService.AddPatient(newId, name, email, password, phone, gender, dateOfBirth, address);

                                    Console.WriteLine("Patient registered successfully!");

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
                                    
                                case 2: // Login
                                    Console.Write("Enter email: ");
                                    string pemail = Console.ReadLine();
                                    Console.Write("Enter password: ");
                                    string ppassword = Console.ReadLine();

                                    var loggedPatient = LoginPatient(pemail, ppassword, users);
                                    if (loggedPatient != null)
                                    {
                                        Console.WriteLine($" Login successful. Welcome, {loggedPatient.Name}!");
                                        ShowPatientActions(loggedPatient);
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Invalid email or password.");
                                    }
                                    break;

                                case 0:
                                    patientMenuActive = false;
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice. Try again.");
                                    break;
                            }
                        }
                        break;

                }
            }
            }
               
        




        ///
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

        static void ShowPatientActions(Patient patient)
        {
            bool actionsActive = true;

            while (actionsActive)
            {
                Console.WriteLine($"\nPatient Dashboard for {patient.Name}:");
                Console.WriteLine("1. View Appointments");
                Console.WriteLine("2. View Reports");
                Console.WriteLine("0. Logout");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine(" Appointments feature coming soon...");
                        break;

                    case "2":
                        Console.WriteLine("Reports feature coming soon...");
                        break;

                    case "0":
                        Console.WriteLine(" Logging out...");
                        actionsActive = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

    }
}

