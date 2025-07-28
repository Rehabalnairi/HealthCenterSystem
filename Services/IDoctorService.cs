using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    /// This interface defines the methods for managing doctors in a health center system.
    interface IDoctorService
    {
        void AddDoctor(string name, string email, string password, string phoneNumber, string specialization); // Adds a new doctor to the system
        void UpdateDoctor(int id, string name, string email, string password, string phoneNumber, string specialization); /// Updates an existing doctor's information by ID
        void DeleteDoctor(int id); // Deletes a doctor from the system by ID
        List<Doctor> GetAllDoctors(); // Retrieves a list of all doctors in the system
        Doctor GetDoctorById(int id); // Retrieves a doctor by their ID

        List<Doctor> GetDoctorsBySpecialization(string specialization); // Retrieves a list of doctors by their specialization
        List<Doctor> GetDoctorsByDepartment(int departmentId); // Retrieves a list of doctors by their department ID
        List<Doctor> GetDoctorsByClinic(int clinicId); // Retrieves a list of doctors by their clinic ID

    }
}
