using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCenterSystem.Models
{
    public class SuperAdmin : User
    {
        public List<User> UsersList { get; set; }
        public List<Branch> BranchesList { get; set; }

        public SuperAdmin(List<User> usersList) : base(1,"Super Admin", "suadmin@healthsystem.com", "123", "999", "Super Admin")
        {
            this.UsersList = usersList;
            BranchesList = new List<Branch>();
        }

        //public SuperAdmin() : base(1,"Super Admin", "suadmin@healthsystem.com", "123", "999", "Super Admin")
        //{
        //    this.UsersList = new List<User>();
        //    BranchesList = new List<Branch>();
        //}

        public string GenerateEmail(string name, string role)
        {
            return $"{name.ToLower().Replace(" ", ".")}@{role.ToLower()}.healthsystem.com";
        }

        public string AddAdmin(int userId, string name, string password, string phoneNumber, string role = "Admin")
        {
            string email = GenerateEmail(name, role.ToLower());
            Admins newAdmin = new Admins(userId, name, email, password, phoneNumber, role);
            UsersList.Add(newAdmin);
            Program.admins.Add(newAdmin);
            return email;
        }


        public string AddDoctor(int doctorId, string name, string password, string specialization)
        {
            string email = GenerateEmail(name, "doctor");
            Doctor newDoctor = new Doctor(doctorId, name, email, password, specialization);
            UsersList.Add(newDoctor);
            return email;
        }

        public void ViewUsers()
        {
            var nonSuperAdminUsers = UsersList.Where(u => u.Role != "Super Admin").ToList();

            if (nonSuperAdminUsers.Count == 0)
            {
                Console.WriteLine("No users have been registered yet.");
                return;
            }

            Console.WriteLine("List of Users:");
            foreach (var user in nonSuperAdminUsers)
            {
                Console.WriteLine($"User ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Role: {user.Role}, Active: {user.IsActive}");
            }
        }

        public void AddBranch(string branchName, string branchLocation, int noOfFloors, int noOfRooms, string departments, string clinics)
        {
            Branch newBranch = new Branch
            {
                BranchId = BranchesList.Count + 1,
                BranchName = branchName ?? throw new ArgumentNullException(nameof(branchName)),
                BranchLocation = branchLocation ?? throw new ArgumentNullException(nameof(branchLocation)),
                NoOfFloors = noOfFloors,
                NoOfRooms = noOfRooms,
                IsActive = true,
               Clinics = clinics?.Split(',').Select(c => c.Trim()).ToList() ?? new List<string>(),
               Departments = departments?.Split(',').Where(d => !string.IsNullOrWhiteSpace(d))
                    .Select(d => new Department(0, d.Trim(), "")).ToList() ?? new List<Department>()
            };

            BranchesList.Add(newBranch);
        }

        public void ViewBranches()
        {
            Console.WriteLine("List of Branches:");
            foreach (var branch in BranchesList)
            {
                Console.WriteLine($"Branch ID: {branch.BranchId}, Name: {branch.BranchName}, Location: {branch.BranchLocation}, Floors: {branch.NoOfFloors}, Rooms: {branch.NoOfRooms}, Active: {branch.IsActive}");
                Console.WriteLine("Departments:");
                foreach (var department in branch.Departments)
                    Console.WriteLine($"- {department.DepName}");
                Console.WriteLine("Clinics:");
                foreach (var clinic in branch.Clinics)
                    Console.WriteLine($"- {clinic}");
            }
        }

        public bool RemoveBranch(int branchId)
        {
            var branch = BranchesList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                BranchesList.Remove(branch);
                return true;
            }
            return false;
        }

        public bool RemoveBranchByName(string branchName)
        {
            var branch = BranchesList.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                BranchesList.Remove(branch);
                return true;
            }
            return false;
        }

        public bool UpdateBranch(int branchId, string name, string location, int floors, int rooms)
        {
            var branch = BranchesList.FirstOrDefault(b => b.BranchId == branchId);
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

        public bool UpdateBranchByName(string branchName, string newName, string newLocation, int newFloors, int newRooms)
        {
            var branch = BranchesList.FirstOrDefault(b => b.BranchName.Equals(branchName, StringComparison.OrdinalIgnoreCase));
            if (branch != null)
            {
                branch.BranchName = newName;
                branch.BranchLocation = newLocation;
                branch.NoOfFloors = newFloors;
                branch.NoOfRooms = newRooms;
                return true;
            }
            return false;
        }
       

    }
}
