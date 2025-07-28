using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IClinicService
    {
        void AddClinic(Clinic clinic); // method to add a new clinic
        Clinic GetClinicById(int id); // method to get a clinic by its ID
        List<Clinic> GetAllClinics(); // method to get all clinics
        void UpdateClinic(Clinic updateClinic); // method to update clinic details
        void DeactivateClinic(int id); // method to deactivate a clinic
        void ActivateClinic(int id); // method to activate a clinic
    }
}
