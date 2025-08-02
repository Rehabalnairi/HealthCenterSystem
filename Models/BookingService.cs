using HealthCenterSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class BookingService : IBookingService
    {
        private List<Booking> bookings = new List<Booking>();
        public void AddBooking(Booking booking)
        {
            bool isTimeTaken = bookings.Any(b => b.DoctorId == booking.DoctorId && b.BookingDate == booking.BookingDate);
            if (isTimeTaken)
            {
                Console.WriteLine("This time slot is already booked for this doctor.");
                return;
            }
            bookings.Add(booking);
            Console.WriteLine("Booking added successfully.");
        }

        public void CancelBooking(int patientId, DateTime bookingDate)
        {
            var booking = bookings.FirstOrDefault(b => b.PatientId == patientId && b.BookingDate == bookingDate);
            if (booking != null)
            {
                bookings.Remove(booking);
                Console.WriteLine("Booking cancelled successfully.");
            }
            else
            {
                Console.WriteLine("No booking found for the specified patient and date.");
            }
        }

        public List<Booking> GetAllBookings()
        {
            return bookings;
        }

        public List<Department> GetDepartments()
        {
            return BookingData.Departments;
        }

        public List<Doctor> GetDoctorByDepartment(int departmentId)
        {
            var department = BookingData.Departments.FirstOrDefault(d => d.DepId == departmentId);
            if (department != null)
            {
                return BookingData.Doctors.Where(d => d.DepartmentId == departmentId).ToList();
            }
            else
            {
                Console.WriteLine("Department not found.");
                return new List<Doctor>();
            }
        }

        public List<DateTime> GetAvailableTimes()
        {
            return BookingData.AvailableTimes;
        }


    }
}

