using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    class IUserService
    {
        void AddUser(User user);
        User Login(string email, string password);
        User GetUserById(int id);
        List<User> GetAllUsers();
        void DeactivateUser(int id);
        void ActivateUser(int id);
    }
}
