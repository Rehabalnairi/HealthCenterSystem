using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{ // user update
   public class User // create user class
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; } // property to hold user phone number
        public string Role { get; set; } // property to hold user role
        public bool ISActive { get; set; } // property to hold user active status

        public User(int userid, string name, string email, string password, string PhoneNumber, string role) //constructor to initialize user properties
        {
            this.UserId = userid;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = PhoneNumber; // initialize phone number
            this.Role = role; // default role is User
            this.ISActive = true; // default active status is true
        }
    }

    class SuperAdmin : User // create a class for super admin that inherits from User
    {
        public List<User> UsersList;
        public int Index=2; // index to keep track of the next user ID
        public SuperAdmin(List<User> usersList) :base(1,"Super Admin","SuperAdmin@gmail.com", "SuperAdmin123", "99997799", "SuperAdmin") // constructor to initialize super admin properties
        {
            this.UsersList = usersList; // initialize the list of users
            this.UsersList.Add(this); // add the super admin to the list of users

        }

        public void AddAdmin(string name, string email, string password) // method to add an admin
        {
            Admins newAdmin = new Admins(Index++, name, email, password); // create a new admin with the next index
            this.UsersList.Add(newAdmin); // add the new admin to the list of users
        }

        public void AddDoctor(string name, string email, string password,string Spelcialization) // method to add a doctor
        {
            Doctor newDoctor = new Doctor (Index++, name, email, password, Spelcialization); // create a new doctor with the next index
            this.UsersList.Add(newDoctor); // add the new doctor to the list of users
        }

        internal void AddDoctor(string? doctorName, string? doctorEmail, string? doctorPassword)
        {
            throw new NotImplementedException();
        }
        List<Branch> BranchesList = new List<Branch>();
        public void AddBranch(string branchName, string branchLocation, int noOfFloors, int noOfRooms, string departments, string clinics)
        // method to add a branch
        {
            Branch newBranch = new Branch();// create a new branch with the next index
            this.BranchesList.Add(newBranch); // add the new branch to the list of users
        }
    }

    class Admins : User
    {
        public Admins(int userid, string name, string email, string password) : base(userid, name, email, password, "99999", "Admin") // constructor to initialize admin properties
        {
            this.ISActive = true; // default active status is true
        }
    }
}  
       
    
