using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
     public class Patient : User
    {
        // add pationtRecords property to hold a list of patient records
        public List<PatientRecord> Records { get; set; } = new List<PatientRecord>();

        public string PatientNI { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public Patient ( int userId,string name, string email, string password, string phoneNumber, string gender, DateTime dateOfBirth, string address) 
            : base (userId,name, email, password, phoneNumber, "Patient") // constructor to initialize patient properties

        {
            this.Gender = gender;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.IsActive = true; // default active status is true
        }
    }


}
