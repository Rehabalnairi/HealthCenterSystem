using System;

namespace HealthCenterSystem.Models
{
    public class Clinic
    {
        private static int IndexClinicID = 1; // Static index to keep track of clinic IDs
        public int ClinicId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        // 
        public Clinic() 
        {
            ClinicId = IndexClinicID++;
        }

        // 
        public Clinic(int clinicId, string name, string address, decimal price, string phoneNumber, string email, bool isActive = true)
        {
            ClinicId = clinicId;
            Name = name;
            Address = address;
            Price = price;
            PhoneNumber = phoneNumber;
            Email = email;
            IsActive = isActive;
        }

        public void DisplayClinicInfo()
        {
            Console.WriteLine($"Clinic ID: {ClinicId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Address: {Address}");
            Console.WriteLine($"Price: {Price:C}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Status: {(IsActive ? "Active" : "Inactive")}");
            Console.WriteLine($"Email: {Email}");
        }
    }
}
