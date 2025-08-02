using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCenterSystem.Models
{
    public class Doctor : User
    {
        public string Specialization { get; set; }
        public List<DateTime> AvailableAppointments { get; set; } = new List<DateTime>();

        public List<PatientRecord> PatientRecords { get; set; } = new List<PatientRecord>();
        public List<Clinic> Clinics { get; set; } = new List<Clinic>();
        public List<Department> Departments { get; set; } = new List<Department>();
        public int DepartmentId { get; set; }
        public int? BranchId { get; set; }

        public Doctor(int userId, string name, string email, string password, string specialization)
            : base(userId, name, email, password, "98376256", "Doctor")
        {
            this.Specialization = specialization;
        }

        public void AddPatientRecord(PatientRecord record)
        {
            this.PatientRecords.Add(record);
        }

        public void AddClinic(Clinic clinic)
        {
            this.Clinics.Add(clinic);
        }

        public override string ToString()
        {
            return $"Doctor ID: {UserId}, Name: {Name}, Email: {Email}, Specialization: {Specialization}, Phone: {PhoneNumber}, Role: {Role}";
        }

        public string ToFileString()
        {
            string departmentsIds = Departments != null && Departments.Count > 0
                ? string.Join("|", Departments.Select(d => d.DepId))
                : "";

            string clinicsIds = Clinics != null && Clinics.Count > 0
                ? string.Join("|", Clinics.Select(c => c.ClinicId))
                : "";

            string appointments = AvailableAppointments != null && AvailableAppointments.Count > 0
                ? string.Join("|", AvailableAppointments.Select(a => a.ToString("o"))) // ISO 8601 format
                : "";

            return $"{UserId},{Name},{Email},{Password},{PhoneNumber},{Specialization},{departmentsIds},{clinicsIds},{appointments}";
        }

        public static Doctor FromFileString(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            var parts = line.Split(',');

            if (parts.Length < 9)  
                return null;

            if (!int.TryParse(parts[0], out int userId))
                return null;

            string name = parts[1];
            string email = parts[2];
            string password = parts[3];
            string phone = parts[4];
            string specialization = parts[5];
            string departmentsIds = parts[6];
            string clinicsIds = parts[7];
            string appointmentsStr = parts[8];

            var doctor = new Doctor(userId, name, email, password, specialization)
            {
                PhoneNumber = phone
            };

            doctor.Departments = departmentsIds.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(idStr =>
                {
                    if (int.TryParse(idStr, out int id))
                        return new Department { DepId = id };
                    return null;
                })
                .Where(d => d != null)
                .ToList();

            doctor.Clinics = clinicsIds.Split('|', StringSplitOptions.RemoveEmptyEntries)
                .Select(idStr =>
                {
                    if (int.TryParse(idStr, out int id))
                        return new Clinic { ClinicId = id };
                    return null;
                })
                .Where(c => c != null)
                .ToList();

            if (!string.IsNullOrEmpty(appointmentsStr))
            {
                doctor.AvailableAppointments = appointmentsStr.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(apptStr =>
                    {
                        if (DateTime.TryParse(apptStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dt))
                            return dt;
                        return (DateTime?)null;
                    })
                    .Where(dt => dt.HasValue)
                    .Select(dt => dt.Value)
                    .ToList();
            }
            else
            {
                doctor.AvailableAppointments = new List<DateTime>();
            }

            return doctor;
        }
    }
}
