using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IDoctorService
    {
        IDoctorService Clone();
        void AddDoctor(string name, string email, string password, string phoneNumber, string specialization);
        void UpdateDoctor(int id, string name, string email, string password, string phoneNumber, string specialization);
        void DeleteDoctor(int id);
        // List<Doctor> GetAllDoctors();

        // Doctor GetDoctorById(int id);
        // List<Doctor> GetDoctorsBySpecialization(string specialization);
        // List<Doctor> GetDoctorsByBranch(int branchId);
        // List<Doctor> GetDoctorsByDepartment(int departmentId);
        // List<Doctor> GetDoctorsByClinic(int clinicId);
        // List<Doctor> GetDoctorsByPatient(int patientId);



    }
}
