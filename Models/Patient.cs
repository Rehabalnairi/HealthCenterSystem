using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
     public class Patient : User
    {
        public List<DateTime> BookedAppointments { get; set; } = new List<DateTime>();
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public Patient(int userId, string name, string email, string password, string phoneNumber, string gender, DateTime dateOfBirth, string address)
       : base(userId, name, email, password, phoneNumber, "Patient")
        {
            this.Gender = gender;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.IsActive = true;
        }

    }


}
