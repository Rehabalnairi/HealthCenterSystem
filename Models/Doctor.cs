using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    public class Doctor:User
    {
        public string Spelcialization { get; set; } // property to hold doctor's specialization
        public List<PatientRecord> PatientRecords { get; set; } // list to hold patient records associated with the doctor
        //public List<Appointment> Appointments { get; set; } // list to hold appointments associated with the doctor
        public List<Clinic> Clinics { get; set; } // list to hold clinics associated with the doctor
        public List<Doctor> Doctors { get; set; } // list to hold doctors associated with the doctor
        public List<Department> Departments { get; set; } // list to hold departments associated with the doctor
        public Doctor(int id, string name, string email, string password,string Spelcialization)
            :   base(name, email, password, "98376256", "Doctor") // constructor to initialize doctor properties
        {
            this.Spelcialization = Spelcialization; // initialize specialization
            this.PatientRecords = new List<PatientRecord>(); // initialize the list of patient records
            this.Clinics = new List<Clinic>(); // initialize the list of clinics
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
            return $"Doctor ID: {UserId}, Name: {Name}, Email: {Email}, Specialization: {Spelcialization}, Phone: {PhoneNumber}, Role: {Role}";
        }
    }
}
