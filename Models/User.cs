using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{ // user update
    public class User // create user class
    {
        private static int IndexUserID = 2; // static index to keep track of user IDs
        public int UserId { get; protected set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; } // property to hold user phone number
        public string Role { get; set; } // property to hold user role
        public bool IsActive { get; set; } // property to hold user active status

        public User( string name, string email, string password, string PhoneNumber, string role) //constructor to initialize user properties
        {
            this.UserId = IndexUserID++;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = PhoneNumber; // initialize phone number
            this.Role = role; // default role is User
            this.IsActive = true; // default active status is true
        }
    }

    class SuperAdmin : User // create a class for super admin that inherits from User
    {
        public List<User> UsersList = new List<User>(); // list to hold all users

        public SuperAdmin(List<User> usersList) : base("Super Admin", "SuAdmin@gmail.com", "123", "999", "Super Admin")
        {
           
        }




        public void AddAdmin(string name, string email, string password) // method to add an admin
        {
            Admins newAdmin = new Admins(name, email, password); // create a new admin with the next index
            this.UsersList.Add(newAdmin); // add the new admin to the list of users
        }

        public void AddDoctor(string name, string email, string password, string Spelcialization) // method to add a doctor
        {
            Doctor newDoctor = new Doctor(0, name, email, password, Spelcialization); // create a new doctor with the next index
            this.UsersList.Add(newDoctor); // add the new doctor to the list of users
        }

        //internal void AddDoctor(string? doctorName, string? doctorEmail, string? doctorPassword)
        //{
        //    throw new NotImplementedException();
        //}
        List<Branch> BranchesList = new List<Branch>();
        public void AddBranch(string branchName, string branchLocation, int noOfFloors, int noOfRooms, string departments, string clinics)
        // method to add a branch
        {
            Branch newBranch = new Branch();// create a new branch with the next index
            this.BranchesList.Add(newBranch); // add the new branch to the list of users
        }
        public void ViewUsers() // method to view all users
        {
            Console.WriteLine("List of Users:");
            foreach (var user in this.UsersList) // iterate through the list of users
            {
                Console.WriteLine($"User ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Role: {user.Role}, Active: {user.IsActive}"); // print user information
            }
        }
    }

    class Admins : User
    {
        public Admins(string name, string email, string password) : base( name, email, password, "99999", "Admin") // constructor to initialize admin properties
        {
            this.IsActive = true; // default active status is true
        }
    }
}

       
    
