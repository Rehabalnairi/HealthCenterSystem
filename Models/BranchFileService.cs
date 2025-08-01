using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HealthCenterSystem.Models;

public static class BranchFileService
{
    const string filePath = "branches.txt";

    public static void SaveToFile(List<Branch> branches)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var branch in branches)
            {
                string departments = string.Join(",", branch.Departments.Select(d => d.DepName));
                string clinics = string.Join(",", branch.Clinics);

                string line = $"{branch.BranchId}|{branch.BranchName}|{branch.BranchLocation}|{branch.NoOfFloors}|{branch.NoOfRooms}|{departments}|{clinics}|{branch.IsActive}";
                writer.WriteLine(line);
            }
        }
    }

    public static List<Branch> LoadFromFile()
    {
        List<Branch> branches = new List<Branch>();

        if (!File.Exists(filePath))
            return branches;

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (parts.Length >= 8)
            {
                Branch branch = new Branch
                {
                    BranchId = int.Parse(parts[0]),
                    BranchName = parts[1],
                    BranchLocation = parts[2],
                    NoOfFloors = int.Parse(parts[3]),
                    NoOfRooms = int.Parse(parts[4]),
               //     Departments = parts[5].Split(',').Where(d => !string.IsNullOrWhiteSpace(d)).Select(name => new Department(name)).ToList(),
                    Clinics = parts[6].Split(',').Where(c => c != "").ToList(),
                    IsActive = bool.Parse(parts[7])
                };

                Branch.BranchList.Add(branch);
            }
        }

        return branches;
    }
}
