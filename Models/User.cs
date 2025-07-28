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

   
}

       
    
