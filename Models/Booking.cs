using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class Booking
    {
       public int PationtId { get; set; } // property to hold patient ID
        public int DepId { get; set; }
        public int DoctorId { get; set; } // property to hold doctor ID
        public int BranchId { get; set; } // property to hold branch ID
        public int ClinicId { get; set; } // property to hold clinic ID
        public DateTime BookingDate { get; set; } // property to hold booking date
        
        public class Dooking
        {
            public static List<Department> Departments = new List<Department>(); // static list to hold departments
            {
                Departments.Add(new Department(1, "Cardiology")); // add a department to the list
                Departments.Add(new Department(2, "Neurology")); // add another department to the list
                };
        public static List<Doctor> doctors = new List<Doctor>()
        {


        new Doctor { Id = 1, Name = "Dr. Smith", DepartmentId = 1 },
        new Doctor { Id = 2, Name = "Dr. Jones", DepartmentId = 2 }

        };
        public static List<DateTime> AvailableTimes = new List<DateTime>();


    }
}
