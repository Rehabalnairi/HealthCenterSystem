using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    //add List of departments to the branch
    // addList of branch departments

    // add List of branch departments to the branch

    class BranchDepartment
    {
        public List<Department> Departments { get; set; } // property to hold list of departments in the branch
        public BranchDepartment() // constructor to initialize the list of departments
        {
            this.Departments = new List<Department>();
        }
        public void AddDepartment(Department department) // method to add a department to the list
        {
            this.Departments.Add(department);
        }
        public void RemoveDepartment(Department department) // method to remove a department from the list
        {
            this.Departments.Remove(department);
        }
        public List<Department> GetDepartments() // method to get the list of departments
        {
            return this.Departments;
        }
      
    }
}
