using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class Department
    {
        public int DepId { get; set; }
        public string DepName { get; set; }
        public List<Doctor> Doctors { get; set; } // list to hold doctors in the department
        public List<Clinic> clinics { get; set; } // list to hold clinics in the department

        public Department(int depId, string depName) // constructor to initialize department properties
        {
            this.DepId = depId;
            this.DepName = depName;
            this.Doctors = new List<Doctor>(); // initialize the list of doctors
            this.clinics = new List<Clinic>(); // initialize the list of clinics
        }

        public void AddDoctor(Doctor doctor) // method to add a doctor to the department
        {
            this.Doctors.Add(doctor); // add the doctor to the list of doctors
        }

        //public void AddClinic(Clinic clinic) // method to add a clinic to the department
        //{
        //    this.clinics.Add(clinic); // add the clinic to the list of clinics
        //}
    }
}
