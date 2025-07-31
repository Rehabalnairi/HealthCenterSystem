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
        public string ToFileString()
        {
            return $"{UserId}|{Name}|{Email}|{Password}|{PhoneNumber}|{Gender}|{DateOfBirth:yyyy-MM-dd}|{Address}";
        }

        public static Patient FromFileString(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length != 8) return null;

            return new Patient(
                int.Parse(parts[0]),   // UserId
                parts[1],              // Name
                parts[2],              // Email
                parts[3],              // Password
                parts[4],              // PhoneNumber
                parts[5],              // Gender
                DateTime.Parse(parts[6]), // DateOfBirth
                parts[7]               // Address
            );
        }


    }


}
