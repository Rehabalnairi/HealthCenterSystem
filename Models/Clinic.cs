using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
   public class Clinic
   {
        public int ClinicId { get; set; }
        public string Name { get; set; } // property to hold clinic name
        public string Address { get; set; } // property to hold clinic address
        public decimal Price { get; set; } // property to hold clinic price
        public string PhoneNumber { get; set; } // property to hold clinic phone number
        public string Email { get; set; } // property to hold clinic email address
        public bool IsActive { get; set; } // property to hold clinic active status
        
        //add
        public Clinic(int clinicId, string name, string address, decimal price, string phoneNumber,string email, bool isActive = true) // constructor to initialize clinic properties
        {
            ClinicId = clinicId;
            Name = name;
            Address = address;
            Price = price;
            PhoneNumber = phoneNumber;
            Email = email; // initialize email
            IsActive = isActive; // default active status is true
            
        }

        public void DisplayClinicInfo() // method to display clinic information
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
