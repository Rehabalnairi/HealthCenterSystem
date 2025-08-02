using System;
using System.Collections.Generic;
using System.Linq;
using HealthCenterSystem.Models;

namespace HealthCenterSystem.Models
{
    public class Admins : User
    {
        public List<Doctor> doctors { get; set; } = new List<Doctor>();

        public Admins(int userId, string name, string email, string password, string phoneNumber, string role)
            : base(userId, name, email, password, phoneNumber, role)
        {
            this.IsActive = true;
        }

       
        private string DoctorsToFileString()
        {
            if (doctors == null || doctors.Count == 0) return "";
            return string.Join(";", doctors.Select(d => d.ToFileString()));
        }

        
        private void DoctorsFromFileString(string doctorsString)
        {
            doctors = new List<Doctor>();

            if (string.IsNullOrWhiteSpace(doctorsString))
                return;

            var doctorStrings = doctorsString.Split(';');

            foreach (var docStr in doctorStrings)
            {
                if (!string.IsNullOrWhiteSpace(docStr))
                {
                    Doctor doctor = Doctor.FromFileString(docStr);
                    if (doctor != null)
                        doctors.Add(doctor);
                }
            }
        }

        public string ToFileString()
        {
            string doctorsString = DoctorsToFileString();
            return $"{UserId},{Name},{Email},{Password},{PhoneNumber},{Role},{IsActive},{doctorsString}";
        }

        public static Admins FromFileString(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            string[] parts = line.Split(new char[] { ',' }, 8);

            if (parts.Length < 7)
                return null;

            if (!int.TryParse(parts[0], out int id)) return null;
            if (!bool.TryParse(parts[6], out bool isActive)) return null;

            string name = parts[1];
            string email = parts[2];
            string password = parts[3];
            string phone = parts[4];
            string role = parts[5];

            Admins admin = new Admins(id, name, email, password, phone, role)
            {
                IsActive = isActive
            };

            
            if (parts.Length == 8)
            {
                admin.DoctorsFromFileString(parts[7]);
            }

            return admin;
        }
    }
}
