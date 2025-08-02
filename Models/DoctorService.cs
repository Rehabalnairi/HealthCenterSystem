using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthCenterSystem.Services
{
    public class DoctorService : IDoctorService
    {
       
        private List<Doctor> doctors = new List<Doctor>();

        
        public void AddDoctor(int userId, string name, string email, string password, string phoneNumber, string specialization)
        {
            Doctor newDoctor = new Doctor(userId, name, email, password, specialization);
            newDoctor.PhoneNumber = phoneNumber;
            doctors.Add(newDoctor);
        }
        public void AddDoctor(Doctor doctor)
        {
            doctors.Add(doctor);
        }


        public void UpdateDoctor(int id, string name, string email, string password, string phoneNumber, string specialization)
        {
            var doctor = doctors.FirstOrDefault(d => d.UserId == id);
            if (doctor != null)
            {
                doctor.Name = name;
                doctor.Email = email;
                doctor.Password = password;
                doctor.PhoneNumber = phoneNumber;
                doctor.Specialization = specialization;
            }
        }

       
        public void DeleteDoctor(int id)
        {
            var doctor = doctors.FirstOrDefault(d => d.UserId == id);
            if (doctor != null)
            {
                doctors.Remove(doctor);
            }
        }

      
        public List<Doctor> GetAllDoctors()
        {
            return doctors;
        }

       
        public Doctor GetDoctorById(int id)
        {
            return doctors.FirstOrDefault(d => d.UserId == id);
        }

       
        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return doctors.Where(d => d.Specialization == specialization).ToList();
        }

       
        public List<Doctor> GetDoctorsByDepartment(int depId)
        {
            return doctors.Where(d => d.Departments.Any(dep => dep.DepId == depId)).ToList();
        }
        public List<Doctor> GetDoctorsByClinic(int clinicId)
        {
            return doctors.Where(d => d.Clinics.Any(c => c.ClinicId == clinicId)).ToList();
        }

        public string AddDoctorAndGenerateEmail(string name, string password, string specialization, Clinic clinic, Department department)
        {
            string email = GenerateEmail(name, "doctor");
            int newId = doctors.Count + 1;
            Doctor newDoctor = new Doctor(newId, name, email, password, specialization);
            newDoctor.PhoneNumber = "00000000";

            if (clinic != null)
                newDoctor.Clinics.Add(clinic);

            if (department != null)
                newDoctor.Departments.Add(department);

            doctors.Add(newDoctor);
            return email;
        }

      
        private string GenerateEmail(string name, string role)
        {
            return $"{name.ToLower()}.{role}@gmail.com";
        }

        public void SaveToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var doctor in doctors)
                {
                    
                    writer.WriteLine(doctor.ToFileString());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            doctors.Clear();

            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
               
                Doctor doctor = Doctor.FromFileString(line);
                if (doctor != null)
                {
                    doctors.Add(doctor);
                }
            }
        }
    }
}
