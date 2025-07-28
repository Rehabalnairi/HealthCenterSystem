using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCenterSystem.Models; // Add this using directive

namespace HealthCenterSystem.Services
{
    interface IPatientService
    {
        void AddPatient(string name, string email, string password, string phoneNumber); // Adds a new patient to the system

        void UpdatePatient(int id, string name, string email, string password, string phoneNumber); // Updates an existing patient's information by ID
        void DeletePatient(int id); // Deletes a patient from the system by ID
        List<Patient> GetAllPatients(); // Retrieves a list of all patients in the system
        Patient GetPatientById(int id);  // Retrieves a patient by their ID     
    }
}
