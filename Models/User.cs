using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{ // user update
    class User // create user class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // property to hold user role
        public bool ISActive { get; set; } // property to hold user active status


        public User(int id, string name, string email, string password, string role) //constructor to initialize user properties
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = role; // default role is User
            this.ISActive = true; // default active status is true
        }
    }

    class SuperAdmin : User // create a class for super admin that inherits from User
    {

        public SuperAdmin() : base(
            id: 1, name: "Super Admin", email: "XXXX@gmail.com", password: "XXXX", role: "SuperAdmin")  // initialize super admin properties)
        {

        }


    }
}  
       
    
