using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IUserService
    {
        void AddUser(User user);
        User Login(string email, string password);
        User GetUserById(int userid);
        List<User> GetAllUsers();
        void DeactivateUser(int userid);
        void ActivateUser(int userid);
    }
}
