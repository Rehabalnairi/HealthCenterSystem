using System;
using System.Collections.Generic;

namespace HealthCenterSystem.Models
{
    public class Booking
    {
        public int PatientId { get; set; } // property to hold patient ID
        public int DepId { get; set; }     // property to hold department ID
        public int DoctorId { get; set; }  // property to hold doctor ID
        public int BranchId { get; set; }  // property to hold branch ID
        public int ClinicId { get; set; }  // property to hold clinic ID
        public DateTime BookingDate { get; set; } // property to hold booking date
    }

    public static class BookingData
    {
        public static List<Department> Departments = new List<Department>
        {
            new Department { Id = 1, Name = "Cardiology" },
            new Department { Id = 2, Name = "Neurology" }
        };

        public static List<Doctor> Doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Dr. Smith", DepartmentId = 1 },
            new Doctor { Id = 2, Name = "Dr. Jones", DepartmentId = 2 }
        };

        public static List<DateTime> AvailableTimes = new List<DateTime>
        {
            DateTime.Today.AddHours(9),
            DateTime.Today.AddHours(10),
            DateTime.Today.AddHours(11)
        };
    }
}
