using HealthCenterSystem.Models;

namespace HealthCenterSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            Console .WriteLine("Welcome to Health Center System");
            // create mune
            SuperAdmin superAdmin = new SuperAdmin(users);
            // add admin
                
        }
    }
}
