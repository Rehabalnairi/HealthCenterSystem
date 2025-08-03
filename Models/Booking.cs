using System;

namespace HealthCenterSystem.Models
{
    public class Booking
    {
        public int PatientId { get; set; }
        public int DepId { get; set; }
        public int DoctorId { get; set; }
        public int BranchId { get; set; }
        public int ClinicId { get; set; }
        public DateTime BookingDate { get; set; }

        public string ToFileString()
        {
            return $"{PatientId},{DepId},{DoctorId},{BranchId},{ClinicId},{BookingDate:yyyy-MM-dd HH:mm}";
        }

        public static Booking FromFileString(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 6) return null;

            return new Booking
            {
                PatientId = int.Parse(parts[0]),
                DepId = int.Parse(parts[1]),
                DoctorId = int.Parse(parts[2]),
                BranchId = int.Parse(parts[3]),
                ClinicId = int.Parse(parts[4]),
                BookingDate = DateTime.Parse(parts[5])
            };
        }
    }

    public static class BookingData
    {
        public static List<Department> Departments = new List<Department>();
        public static List<Doctor> Doctors = new List<Doctor>();

        public static List<DateTime> AvailableTimes = new List<DateTime>
        {
            DateTime.Today.AddHours(9),
            DateTime.Today.AddHours(10),
            DateTime.Today.AddHours(11)
        };
    }
}
