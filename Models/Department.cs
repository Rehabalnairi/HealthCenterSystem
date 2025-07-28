using System;
using System.Collections.Generic;

namespace HealthCenterSystem.Models
{
    public class Department
    {
        public int DepId { get; set; }
        public string DepName { get; set; }
        // public List<Doctor> Doctors { get; set; }
        // public List<Clinic> Clinics { get; set; }

        public Department(int depId, string depName)
        {
            this.DepId = depId;
            this.DepName = depName;
            // this.Doctors = new List<Doctor>();
            // this.Clinics = new List<Clinic>();
        }

        //  public void AddDoctor(Doctor doctor)
        // {
        //  this.Doctors.Add(doctor);
        //}

        //    public void AddClinic(Clinic clinic)
        //    {
        //        this.Clinics.Add(clinic);
        //    }
        //}

        public static class DepartmentData
        {
            public static List<Department> Departments = new List<Department>
        {
            new Department(1, "Cardiology"),
            new Department(2, "Neurology")
        };
        }
    }
}
