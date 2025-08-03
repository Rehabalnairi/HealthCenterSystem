using HealthCenterSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthCenterSystem.Models
{
    class BookingService : IBookingService
    {
        private List<Booking> bookings = new List<Booking>();
        private readonly string bookingFilePath = "bookings.txt";

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
            SaveBookingsToFile(); // حفظ تلقائي بعد الإضافة
        }

        public void CancelBooking(int patientId, DateTime bookingDate)
        {
            var booking = bookings.FirstOrDefault(b => b.PatientId == patientId && b.BookingDate == bookingDate);
            if (booking != null)
            {
                bookings.Remove(booking);
                Console.WriteLine("Booking cancelled successfully.");
                SaveBookingsToFile(); 
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

        public void SaveBookingsToFile()
        {
            var lines = bookings.Select(b =>
                $"{b.PatientId}|{b.DoctorId}|{b.BookingDate:yyyy-MM-dd HH:mm}");
            File.WriteAllLines(bookingFilePath, lines);
        }

       
        public void LoadBookingsFromFile()
        {
            if (!File.Exists(bookingFilePath))
                return;

            var loadedBookings = new List<Booking>();

            foreach (var line in File.ReadAllLines(bookingFilePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int patientId) &&
                    int.TryParse(parts[1], out int doctorId) &&
                    DateTime.TryParse(parts[2], out DateTime bookingDate))
                {
                    loadedBookings.Add(new Booking
                    {
                        PatientId = patientId,
                        DoctorId = doctorId,
                        BookingDate = bookingDate
                    });
                }
            }

            bookings = loadedBookings;
        }
    }
}
