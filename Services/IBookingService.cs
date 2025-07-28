using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IBookingService
    {
        void AddBooking(Booking booking); // method to add a new booking
        List <Booking> GetAllBookings(); // method to get all bookings
        List <DateTime> GetAvailableTimes();
        List <Department> GetDepartments(); // method to get all departments
        List <Doctor> GetDoctorByDepartment(int departmentId); // method to get doctors by department ID
        void CancelBooking(int patientId, DateTime bookingDate); // method to cancel a booking
    }
}
