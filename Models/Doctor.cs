using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    public class Doctor:User
    {
       // public string DoctorNI { get; set; }
        public string Specialization { get; set; } // property to hold doctor's specialization
        public List<PatientRecord> PatientRecords { get; set; } // list to hold patient records associated with the doctor
        public List<Clinic> Clinics { get; set; } // list to hold clinics associated with the doctor
        public List<Department> Departments { get; set; } // list to hold departments associated with the doctor
        public int DepartmentId { get; set; } // property to hold department ID
        public int? BranchId { get; set; }
        public Doctor(int userId, string name, string email, string password,string Specialization)
            :   base(userId, name, email, password, "98376256", "Doctor") // constructor to initialize doctor properties
        {
            UserId = userId; // set the user ID
            this.Specialization = Specialization; // initialize specialization
            this.PatientRecords = new List<PatientRecord>(); // initialize the list of patient records
            this.Clinics = new List<Clinic>(); // initialize the list of clinics
            this.Departments = new List<Department>(); // initialize the list of departments
        }

        public void AddPatientRecord(PatientRecord record) // method to add a patient record
        {
            this.PatientRecords.Add(record); // add the record to the list of patient records
        }

        public void AddClinic(Clinic clinic) // method to add a clinic
        {       
                this.Clinics.Add(clinic); // add the clinic to the list of clinics    
        }

        public override string ToString() // override ToString method to return doctor's information
        {
            return $"Doctor ID: {UserId}, Name: {Name}, Email: {Email}, Specialization: {Specialization}, Phone: {PhoneNumber}, Role: {Role}";
        }
    }
}
