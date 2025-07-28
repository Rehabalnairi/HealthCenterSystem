using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class PatientService
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(string name, string email, string password, string phoneNumber)
        {
            string gender = "Unknow";
            DateTime dateOfBirth = new DateTime(2000, 01, 15); // Default to current date
            string address = "Unknown"; // Default address

            Patient newpatient = new Patient(name, email, password, phoneNumber, gender, dateOfBirth, address);
            patients.Add(newpatient);
        }

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

        public void DeletePatient(int id)
        {
            var patient = GetPatientById(id);
            if (patient != null)
            {
                patients.Remove(patient);
            }
        }

        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        public Patient GetPatientById(int id)
        {
            return patients.FirstOrDefault(p => p.UserId == id);

        }
    }
}
