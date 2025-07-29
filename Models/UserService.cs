using HealthCenterSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class UserService : IUserService
    {
        private List<User> users = new List<User>();

      
        public void AddUser(User user)
        {
            if (users.Any(u => u.Email == user.Email))
                throw new Exception("User with this email already exists.");

            users.Add(user);
        }

        public User Login(string email, string password)
        {
            return users.FirstOrDefault(u => u.Email == email && u.Password == password && u.IsActive);
        }

        public User GetUserById(int userid)
        {
            return users.FirstOrDefault(u => u.UserId == userid);
        }

        public List<User> GetAllUsers()
        {
            return users.Where(u => u.IsActive).ToList();
        }

        public void DeactivateUser(int userid)
        {
            var user = GetUserById(userid);
            if (user != null)
                user.IsActive = false;  
        }

        public void ActivateUser(int userid)
        {
            var user = GetUserById(userid);
            if (user != null)
                user.IsActive = true;  
        }
    }
}

