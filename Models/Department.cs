﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCenterSystem.Models
{
    public class Department
    {
        private static int IndexDepartmentID = 1; // Static index to keep track of department IDs
        public int DepId { get; set; }
        public string DepName { get; set; }
        public  string Head { get; set; } // Property to hold the head of the department
        public List<Doctor> Doctors { get; set; }
        public List<Clinic> Clinics { get; set; }

        //public Department(string depName)
        //{
        //    this.DepId = 0; 
        //    this.DepName = depName;
        //    this.Clinics = new List<Clinic>();
        //    this.Doctors = new List<Doctor>();
        //}
        public Department()
        {
            this.DepId = 0;
            this.DepName = "";
            this.Head = "";
            this.Clinics = new List<Clinic>();
            this.Doctors = new List<Doctor>();
        }


        // Constructor
        public Department(int depId, string depName, string head)
        {
            DepId = depId;
            DepName = depName ?? throw new ArgumentNullException(nameof(depName));
            Clinics = new List<Clinic>();
            Head = head;
            Clinics = new List<Clinic>();
            Doctors = new List<Doctor>();
        }

        // Add a clinic to the department
        public void AddClinic(Clinic clinic)
        {
            if (clinic == null)
                throw new ArgumentNullException(nameof(clinic));

            // Prevent adding duplicates by ID or object reference
            if (!Clinics.Any(c => c.ClinicId == clinic.ClinicId))
            {
                Clinics.Add(clinic);
            }
        }

        // Remove a clinic from the department
        public bool RemoveClinic(Clinic clinic)
        {
            if (clinic == null) return false;
            return Clinics.Remove(clinic);
        }

        // Optional: Remove clinic by ID
        public bool RemoveClinicById(int clinicId)
        {
            var clinic = Clinics.FirstOrDefault(c => c.ClinicId == clinicId);
            if (clinic != null)
            {
                Clinics.Remove(clinic);
                return true;
            }
            return false;
        }

        // Static data holder (consider moving this out in production)
        public static class DepartmentData
        {
            public static List<Department> Departments = new List<Department>();
            
        }

        public static List<Department> GetAllDepartments()
        {
            return DepartmentData.Departments;
        }

    }
}
