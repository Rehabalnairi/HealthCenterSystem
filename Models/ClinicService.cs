using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
      public class ClinicService
      {
            private List<Clinic> clinics = new List<Clinic>();

            public void AddClinic(Clinic clinic) // method to add a new clinic
            {
                clinics.Add(clinic);
                Console.WriteLine($"Clinic {clinic.Name} added successfully.");
            }

            public Clinic GetClinicById(int id) // method to get a clinic by its ID
            {
                return clinics.FirstOrDefault(c => c.ClinicId == id);
            }

            public List<Clinic> GetAllClinics() // method to get all clinics
            {
                return clinics;
            }

            public void UpdateClinic(Clinic updateClinic) // method to update clinic details
            {
                var existingClinic = clinics.FirstOrDefault(c => c.ClinicId == updateClinic.ClinicId);
                if (existingClinic != null)
                {
                    existingClinic.Name = updateClinic.Name;
                    existingClinic.Address = updateClinic.Address;
                    existingClinic.Price = updateClinic.Price;
                    existingClinic.PhoneNumber = updateClinic.PhoneNumber;
                    existingClinic.Email = updateClinic.Email; // update email
                    existingClinic.IsActive = updateClinic.IsActive; // update active status
                    Console.WriteLine($"Clinic {existingClinic.Name} updated successfully.");
                }
                else
                {
                    Console.WriteLine("Clinic not found.");
                }
            }

            public void DeactivateClinic(int id)
            {
                var clinic = clinics.FirstOrDefault(c => c.ClinicId == id);
                if (clinic != null)
                {
                    clinic.IsActive = false; // Mark clinic as inactive instead of deleting
                    Console.WriteLine($"Clinic {clinic.Name} deleted Deactivate.");
                }
                else
                {
                    Console.WriteLine("Clinic not found.");
                }
            }
            public void ActivateClinic(int id)
            {
                var clinic = clinics.FirstOrDefault(c => c.ClinicId == id);
                if (clinic != null)
                {
                    clinic.IsActive = true; // Mark clinic as active
                    Console.WriteLine($"Clinic {clinic.Name} activated.");
                }
                else
                {
                    Console.WriteLine("Clinic not found.");
                }
            }
      }
}
