using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthCenterSystem.Models
{
    public class Branch
    {
        private static int _idCounter = 1;

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchLocation { get; set; }
        public int NoOfFloors { get; set; }
        public int NoOfRooms { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<string> Clinics { get; set; } = new List<string>();
        public bool IsActive { get; set; }

        public static List<Branch> BranchList { get; private set; } = new List<Branch>();

        public Branch()
        {
            BranchId = _idCounter++;
        }

        public string ToFileString()
        {
            string depts = Departments != null && Departments.Count > 0
                ? string.Join("|", Departments.Select(d => d.DepName))
                : "";

            string clinicsStr = Clinics != null && Clinics.Count > 0
                ? string.Join("|", Clinics)
                : "";

            return $"{BranchId},{BranchName},{BranchLocation},{NoOfFloors},{NoOfRooms},{IsActive},{depts},{clinicsStr}";
        }

        public static Branch FromFileString(string line)
        {
            var parts = line.Split(',');

            if (parts.Length < 8) return null;

            var branch = new Branch
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

        private const string branchFilePath = "branches.txt";

        public static void LoadBranchesFromFile()
        {
            if (!File.Exists(branchFilePath))
            {
                BranchList = new List<Branch>();
                return;
            }

            var lines = File.ReadAllLines(branchFilePath);
            BranchList = new List<Branch>();

            foreach (var line in lines)
            {
                var branch = FromFileString(line);
                if (branch != null)
                    BranchList.Add(branch);
            }
        }

   
        public static void SaveBranchesToFile()
        {
            using (var writer = new StreamWriter(branchFilePath))
            {
                foreach (var branch in BranchList)
                {
                    writer.WriteLine(branch.ToFileString());
                }
            }
        }

  
        public static void AddBranch(Branch branch)
        {
            BranchList.Add(branch);
            SaveBranchesToFile();
        }

        public static bool UpdateBranch(int branchId, string newName, string newLocation, int floors, int rooms)
        {
            var branch = BranchList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                branch.BranchName = newName;
                branch.BranchLocation = newLocation;
                branch.NoOfFloors = floors;
                branch.NoOfRooms = rooms;
                SaveBranchesToFile();
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
                SaveBranchesToFile();
                return true;
            }
            return false;
        }
    }
}
