using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCenterSystem.Models
{
    //public class Department
    //{
    //    public string Name { get; set; }
    //    public string Head { get; set; }
    //}

    public class Branch
    {
        //Properties for one Branch
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchLocation { get; set; }
        public int NoOfFloors { get; set; }
        public int NoOfRooms { get; set; }
        public List<Department> Departments { get; set; }
        public List<string> Clinics { get; set; }
        public bool IsActive { get; set; }

        //Static list to hold all branches globally
        public static List<Branch> BranchList { get; set; } = new List<Branch>();

 
        public Branch()
        {
            Departments = new List<Department>();
            Clinics = new List<string>();
            BranchName = "";
        }

        //  Methods

        public static void AddBranch(Branch branch)
        {
            BranchList.Add(branch);
        }

        public static bool RemoveBranch(int branchId)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                BranchList.Remove(branch);
                return true;
            }
            return false;
        }

        public static bool UpdateBranch(int branchId, string name, string location, int floors, int rooms)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.BranchName = name;
                branch.BranchLocation = location;
                branch.NoOfFloors = floors;
                branch.NoOfRooms = rooms;
                return true;
            }
            return false;
        }

        public static bool SetBranchStatus(int branchId, bool isActive)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.IsActive = isActive;
                return true;
            }
            return false;
        }

        public static Branch GetBranchById(int branchId)
        {
            return BranchList.FirstOrDefault(b => b.BranchId == branchId);
        }

        public static List<Branch> GetAllBranches()
        {
            return BranchList;
        }

        public static bool AddDepartmentToBranch(int branchId, Department department)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.Departments.Add(department);
                return true;
            }
            return false;
        }

        public static bool RemoveDepartmentFromBranch(int branchId, string departmentName)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                var dept = branch.Departments.FirstOrDefault(d => d.DepName == departmentName);
                if (dept != null)
                {
                    branch.Departments.Remove(dept);
                    return true;
                }
            }
            return false;
        }
    }
}
