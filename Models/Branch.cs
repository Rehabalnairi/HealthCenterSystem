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
            this.BranchId = 1; // default branch ID
            this.BranchName = "Default Branch"; // default branch name
            this.BranchLocation = "Default Location"; // default branch location
            this.NoOFfloors = 1; // default number of floors
            this.NoOFRooms = 10; // default number of rooms
            this.Departments = "General"; // default departments
            this.Clinics = "General Clinic"; // default clinics

        }
     class Branches
        {
            public List<Branch> BranchList { get; set; } // property to hold list of branches
            public Branches() // constructor to initialize the list of branches
            {
                this.BranchList = new List<Branch>();
            }
            public void AddBranch(Branch branch) // method to add a branch to the list
            {
                this.BranchList.Add(branch);
            }
        }

        public virtual string GetBranchInfo() // method to get branch information
        {
            return $"Branch ID: {BranchId}, Name: {BranchName}, Location: {BranchLocation}, Floors: {NoOFfloors}, Rooms: {NoOFRooms}, Departments: {Departments}, Clinics: {Clinics}";
        }

    
     } 
}
