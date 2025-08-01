using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{

    class PatientService
    {
        private List<Patient> patients = new List<Patient>(); // List to hold all patients in the system

        /// Adds a new patient to the system with the provided details
        public void AddPatient(int userId, string name, string email, string password, string phoneNumber, string gender, DateTime dateOfBirth, string address)
        {
            if (patients.Any(p => p.UserId == userId))
            {
                Console.WriteLine("A patient with this ID already exists.");
                return;
            }

            Patient newPatient = new Patient(userId, name, email, password, phoneNumber, gender, dateOfBirth, address);
            patients.Add(newPatient);
        }


        /// Updates an existing patient's information based on their ID
        public void UpdatePatient(int id, string name, string email, string password, string phoneNumber)
        {
            var patient = GetPatientById(id);
            if (patient != null)
            {
                patient.Name = name;
                patient.Email = email;
                patient.Password = password;
                patient.PhoneNumber = phoneNumber;
            }
        }

        /// Deletes a patient from the system based on their ID
        public void DeletePatient(int id)
        {
            var patient = GetPatientById(id);
            if (patient != null)
            {
                patients.Remove(patient);
            }
        }

        /// Retrieves a list of all patients in the system
        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        /// Retrieves a patient by their ID
        public Patient GetPatientById(int id)
        {
            return patients.FirstOrDefault(p => p.UserId == id);

        }
        public bool PatientIdExists(int id)
        {
            return patients.Any(p => p.UserId == id);
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var patient in patients)
                {
                    writer.WriteLine(patient.ToFileString());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                patients = new List<Patient>();
                return;
            }

            patients = new List<Patient>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Patient p = Patient.FromFileString(line);
                if (p != null)
                {
                    patients.Add(p);
                }
            }
        }
        public void AddPatient(Patient patient)
        {
            if (patients.Any(p => p.UserId == patient.UserId))
            {
                Console.WriteLine("A patient with this ID already exists.");
                return;
            }

            patients.Add(patient);
        }


    }
}
