﻿using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static bool UpdateBranchById(int branchId, string newName, string newLocation, int floors, int rooms)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.BranchName = newName;
                branch.BranchLocation = newLocation;
                branch.NoOfFloors = floors;
                branch.NoOfRooms = rooms;
                return true;
            }
            return false;
        }

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

        public string ToFileString()
        {
            // departments names separated by '|'
            string depts = Departments != null && Departments.Count > 0
                ? string.Join("|", Departments.Select(d => d.DepName))
                : "";

            // clinics separated by '|'
            string clinicsStr = Clinics != null && Clinics.Count > 0
                ? string.Join("|", Clinics)
                : "";

            return $"{BranchId},{BranchName},{BranchLocation},{NoOfFloors},{NoOfRooms},{IsActive},{depts},{clinicsStr}";
        }
        public static Branch FromFileString(string line)
        {
            var parts = line.Split(',');

            if (parts.Length < 8) return null; 
            Branch branch = new Branch
            {
                BranchId = int.Parse(parts[0]),
                BranchName = parts[1],
                BranchLocation = parts[2],
                NoOfFloors = int.Parse(parts[3]),
                NoOfRooms = int.Parse(parts[4]),
                IsActive = bool.Parse(parts[5]),
                Departments = parts[6].Split('|', StringSplitOptions.RemoveEmptyEntries).Select(d => new Department { DepName = d }).ToList(),
                Clinics = parts[7].Split('|', StringSplitOptions.RemoveEmptyEntries).ToList()
            };

            
            if (branch.BranchId >= _idCounter)
            {
                _idCounter = branch.BranchId + 1;
            }

            return branch;
        }

        public static void SaveBranchesToFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var branch in BranchList)
                {
                    writer.WriteLine(branch.ToFileString());
                }
            }
        }

        public static void LoadBranchesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                BranchList = new List<Branch>();
                return;
            }

            BranchList = new List<Branch>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var branch = FromFileString(line);
                if (branch != null)
                {
                    BranchList.Add(branch);
                }
            }
        }

    }
}
