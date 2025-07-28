using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IDepartmentService
    {
        void CreateDepartment(string name, string description);
        void UpdateDepartment(int id, string name, string description);
        void DeleteDepartment(int id);
        void SetDepartmentStatus(int id, bool isActive);
        List<string> GetAllDepartments(); // Returns a list of all department names
        string GetDepartmentById(int id); // Returns the name of the department by its ID
        List<string> GetActiveDepartments(); // Returns a list of active department names
        List<string> GetInactiveDepartments(); // Returns a list of inactive department names
        void AddClinicToDepartment(int departmentId, int clinicId); // Adds a clinic to a department

    }
}
