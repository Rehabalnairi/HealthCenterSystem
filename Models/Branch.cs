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
        private static int _idCounter = 1;
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
            BranchId = _idCounter++; // Auto-incrementing ID
            Departments = new List<Department>();
            Clinics = new List<string>();
            BranchName = ""; // Optional default
        }

        //  Methods

        //public  void AddBranch(Branch branch)
        //{
        //   Branch newBranch = new Branch
        //   {
        //       BranchId = branch.BranchId,
        //       BranchName = branch.BranchName,
        //       BranchLocation = branch.BranchLocation,
        //       NoOfFloors = branch.NoOfFloors,
        //       NoOfRooms = branch.NoOfRooms,
        //       IsActive = branch.IsActive,
        //       Departments = new List<Department>(branch.Departments),
        //       Clinics = new List<string>(branch.Clinics)
        //      // IsActive = true // Default to active when adding a new branch

        //   };
        //    BranchList.Add(newBranch);
        //}

        public void AddBranch(string branchName, string branchLocation, int noOfFloors, int noOfRooms)
        {
            Branch newBranch = new Branch
            {
                BranchId=BranchId,
                BranchName = branchName,
                BranchLocation = branchLocation,
                NoOfFloors = noOfFloors,
                NoOfRooms = noOfRooms,
                IsActive = true
            };

            ////Convert comma-separated string to Department list
            //newBranch.Departments = departments.Split(',')
            //    .Where(d => !string.IsNullOrWhiteSpace(d))
            //    .Select(d => new Department { DepName = d.Trim(), Head = "" })
            //    .ToList();

            ////  Convert comma-separated string to Clinic list
            //newBranch.Clinics = clinics.Split(',')
            //    .Where(c => !string.IsNullOrWhiteSpace(c))
            //    .Select(c => c.Trim())
            //    .ToList();

            // Add the new branch to the list
            BranchList.Add(newBranch);
        }

        public static bool UpdateBranchByName(string name, string location, int floors, int rooms)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                branch.BranchLocation = location;
                branch.NoOfFloors = floors;
                branch.NoOfRooms = rooms;
                return true;
            }
            return false;
        }

        public static bool RemoveBranchByName(string name)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                BranchList.Remove(branch);
                return true;
            }
            return false;
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

        //public static bool UpdateBranch(int branchId, string name, string location, int floors, int rooms)
        //{
        //    var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
        //    if (branch != null)
        //    {
        //        branch.BranchName = name;
        //        branch.BranchLocation = location;
        //        branch.NoOfFloors = floors;
        //        branch.NoOfRooms = rooms;
        //        return true;
        //    }
        //    return false;
        //}

        public void ViewBranches()
        {
            foreach (var b in Branch.GetAllBranches())
            {
                Console.WriteLine($"ID: {b.BranchId}, Name: {b.BranchName}, Location: {b.BranchLocation}");
            }
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
