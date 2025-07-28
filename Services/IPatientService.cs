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
        void AddPatient(string name, string email, string password, string phoneNumber);
        void UpdatePatient(int userid, string name, string email, string password, string phoneNumber);
        void DeletePatient(int userid);
        List<Patient> GetAllPatients();
        Patient GetPatientById(int userid);
    }
}
