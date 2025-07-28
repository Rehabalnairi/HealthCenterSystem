using HealthCenterSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCenterSystem.Models;

namespace HealthCenterSystem.Models
{
    public class DoctorService: IDoctorService
    {
        private List<Doctor> doctors = new List<Doctor>(); // List to hold all doctors in the system

        /// Adds a new doctor to the system with the provided details
        public void AddDoctor(string name, string email, string password, string phoneNumber, string specialization)
        {
            int newId = doctors.Count + 1;
            Doctor newDoctor = new Doctor(newId, name, email, password, specialization);
            newDoctor.PhoneNumber = phoneNumber; // Set the phone number for the doctor
            doctors.Add(newDoctor);
        }

        /// Updates an existing doctor's information based on their ID
        public void UpdateDoctor(int id, string name, string email, string password, string phoneNumber, string specialization)
        {
            var doctor = doctors.FirstOrDefault(d => d.UserId == id);
            if (doctor != null)
            {
                doctor.Name = name;
                doctor.Email = email;
                doctor.Password = password;
                doctor.PhoneNumber = phoneNumber;
                doctor.Spelcialization = specialization;
            }
        }

        /// Deletes a doctor from the system based on their ID
        public void DeleteDoctor(int id)
        {
            var doctor = doctors.FirstOrDefault(d => d.UserId == id);
            if (doctor != null)
            {
                doctors.Remove(doctor);
            }
        }

        /// Retrieves a list of all doctors in the system
        public List<Doctor> GetAllDoctors()
        {
            return doctors;
        }

        /// Retrieves a doctor by their ID
        public Doctor GetDoctorById(int id)
        {
            return doctors.FirstOrDefault(d => d.UserId == id);
        }

        /// Retrieves a list of doctors based on their specialization
        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return doctors.Where(d => d.Spelcialization ==specialization).ToList();
        }

        /// Retrieves a list of doctors associated with a specific department
        public List<Doctor> GetDoctorsByDepartment(int DepId)
        {
            return doctors.Where(d => d.Departments.Any(dep => dep.DepId == DepId)).ToList();
        }

        /// Retrieves a list of doctors associated with a specific clinic
        public List<Doctor> GetDoctorsByClinic(int clinicId)
        {
            return doctors.Where(d => d.Clinics.Any(c => c.ClinicId == clinicId)).ToList();
        }
    }
}
