using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
     public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; } // property to hold branch name
        public string BranchLocation { get; set; } // property to hold branch location
        public int NoOFfloors { get; set; } // property to hold number of floors in the branch
        public int NoOFRooms { get; set; } // property to hold number of rooms in the branch
        public string Departments { get; set; } // property to hold departments in the branch
        public string Clinics { get; set; } // property to hold clinics in the branch


        //construct a class to hold a list of branches
        public Branch()
        {
           Departments= new List<Department>().ToString(); // initialize departments as an empty string

        }
    class Branches
        {
            public List<Department> Departments { get; set; } // property to hold list of departments
            public List<Branch> BranchList { get; set; } // property to hold list of branches
            public Branches() // constructor to initialize the list of branches
            {
                this.BranchList = new List<Branch>();
            }
            public void AddBranch(Branch branch) // method to add a branch to the list
            {
                this.BranchList.Add(branch);
            }
            public void AddDepartment(Department department) // method to add a department to the list
            { 
                Departments.Add(department);
            }

            public void RemoveDepartment(Department department) // method to remove a department from the list
            {
                Departments.Remove(department);
            }
        }

      
    } }
