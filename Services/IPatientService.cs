using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IPatientService
    {
        void AddPatient(string name, string email, string password, string phoneNumber);
        void UpdatePatient(int id, string name, string email, string password, string phoneNumber);
        void DeletePatient(int id);
        //List<Patient> GetAllPatients();
       // Patient GetPatientById(int id);
    }


}
