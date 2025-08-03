using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public Clinic()
        {
            ClinicId = IndexClinicID++;
        }

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

        public string ToFileString()
        {
         
            return $"{ClinicId}|{Name}|{Address}|{Price}|{PhoneNumber}|{Email}|{IsActive}";
        }

        public static Clinic FromFileString(string line)
        {
            var parts = line.Split('|');
            if (parts.Length != 7)
                return null;

            return new Clinic
            {
                ClinicId = int.Parse(parts[0]),
                Name = parts[1],
                Address = parts[2],
                Price = decimal.Parse(parts[3]),
                PhoneNumber = parts[4],
                Email = parts[5],
                IsActive = bool.Parse(parts[6])
            };
        }
    }

    public static class ClinicFileService
    {
        private const string filePath = "clinics.txt";

        public static void SaveToFile(List<Clinic> clinics)
        {
            var lines = clinics.Select(c => c.ToFileString());
            File.WriteAllLines(filePath, lines);
        }

        public static List<Clinic> LoadFromFile()
        {
            var clinics = new List<Clinic>();

            if (!File.Exists(filePath))
                return clinics;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var clinic = Clinic.FromFileString(line);
                if (clinic != null)
                    clinics.Add(clinic);
            }

            return clinics;
        }
    }
}
