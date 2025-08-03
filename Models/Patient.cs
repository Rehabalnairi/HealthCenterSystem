using System;
using System.Collections.Generic;
using System.Linq;

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
            string bookedApps = BookedAppointments != null && BookedAppointments.Count > 0
                ? string.Join(";", BookedAppointments.Select(d => d.ToString("o")))  // صيغة ISO 8601 دقيقة
                : "";

            return $"{UserId}|{Name}|{Email}|{Password}|{PhoneNumber}|{Gender}|{DateOfBirth:yyyy-MM-dd}|{Address}|{bookedApps}";
        }

      
        public static Patient FromFileString(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length < 8) return null;

            var patient = new Patient(
                int.Parse(parts[0]),       // UserId
                parts[1],                  // Name
                parts[2],                  // Email
                parts[3],                  // Password
                parts[4],                  // PhoneNumber
                parts[5],                  // Gender
                DateTime.Parse(parts[6]),  // DateOfBirth
                parts[7]                   // Address
            );

            if (parts.Length >= 9 && !string.IsNullOrEmpty(parts[8]))
            {
                patient.BookedAppointments = parts[8].Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(str =>
                    {
                        if (DateTime.TryParse(str, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt))
                            return dt;
                        return (DateTime?)null;
                    })
                    .Where(dt => dt.HasValue)
                    .Select(dt => dt.Value)
                    .ToList();
            }
            else
            {
                patient.BookedAppointments = new List<DateTime>();
            }

            return patient;
        }
    }
}
