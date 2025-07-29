using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthCenterSystem.Models
{
    public class SuperAdmin : User
    {
        // <summary>
        /// SuperAdmin class inherits from User and manages the system's users and branches.
        public List<User> UsersList { get; set; }// List to hold all users in the system, including admins and doctors.
        public List<Branch> BranchesList { get; set; }// List to hold all branches in the system.
        /// Constructor initializes the SuperAdmin with default values and empty lists for users and branches.
       
        public SuperAdmin(List<User> usersList) : base("Super Admin", "suadmin@healthsystem.com", "123", "999", "Super Admin")
        {
            this.UsersList = new List<User>();
            BranchesList = new List<Branch>();
        }
        public SuperAdmin() : base("Super Admin", "suadmin@healthsystem.com", "123", "999", "Super Admin")
        {
            this.UsersList = new List<User>();
            BranchesList = new List<Branch>();
        }
        /// Constructor that accepts a list of users to initialize the SuperAdmin.
        private string GenerateEmail(string name, string role)
        {
            // Generates an email address based on the user's name and role.
            return $"{name.ToLower().Replace(" ", ".")}@{role.ToLower()}.healthsystem.com";
        }
        /// <summary>
        /// Adds a new admin to the system and returns the generated email.
        public string AddAdmin(string name, string password)
        {
            string email = GenerateEmail(name, "admin");
            Admins newAdmin = new Admins(name, email, password);
            UsersList.Add(newAdmin);
            return email;
        }
        /// Adds a new doctor to the system and returns the generated email.
        public string AddDoctor(string name, string password, string Spelcialization)
        {
            string email = GenerateEmail(name, "doctor");
            Doctor newDoctor = new Doctor(0, name, email, password, Spelcialization);
            UsersList.Add(newDoctor);
            return email;
        }
        /// Adds a new patient to the system and returns the generated email.
        public void ViewUsers()
        {
            Console.WriteLine("List of Users:");
            foreach (var user in UsersList)
            {
                if (user.Role == "Super Admin")
                {
                    continue; // Skip Super Admin
                    Console.WriteLine($"User ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Role: {user.Role}, Active: {user.IsActive}");
                }
            }
        }
        /// Adds a new branch to the system with the specified details.
        public void AddBranch(string branchName, string branchLocation, int noOfFloors, int noOfRooms, string departments, string clinics)
        {
            // Validate inputs
            Branch newBranch = new Branch
            {
                BranchId = BranchesList.Count + 1, // Assign a new ID based on the current count
                BranchName = branchName ?? throw new ArgumentNullException(nameof(branchName)),
                BranchLocation = branchLocation ?? throw new ArgumentNullException(nameof(branchLocation)),
                NoOfFloors = noOfFloors,
                NoOfRooms = noOfRooms,
                IsActive = true, // Default to active when adding a new branch
                Departments = new List<Department>(), // Initialize departments list
                Clinics = clinics?.Split(',').Select(c => c.Trim()).ToList() ?? new List<string>() // Split clinics by comma and trim whitespace
            };
            // Add the new branch to the list of branches
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
                {
                    Console.WriteLine($"- {department.DepName})");
                }
                Console.WriteLine("Clinics:");
                foreach (var clinic in branch.Clinics)
                {
                    Console.WriteLine($"- {clinic}");
                }
            }
        }

        public bool RemoveBranch(int branchId)
        {
            // Find the branch by ID
            var branch = BranchesList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                // Remove the branch from the list
                BranchesList.Remove(branch);
                return true; // Return true if removal was successful
            }
            return false; // Return false if no branch was found with the given ID
        }

        public bool UpdateBranch(int branchId, string name, string location, int floors, int rooms)
        {
            // Find the branch by ID
            var branch = BranchesList.FirstOrDefault(b => b.BranchId == branchId);
            if (branch != null)
            {
                // Update the branch details
                branch.BranchName = name;
                branch.BranchLocation = location;
                branch.NoOfFloors = floors;
                branch.NoOfRooms = rooms;
                return true; // Return true if update was successful
            }
            return false; // Return false if no branch was found with the given ID
        }


    }
}





















