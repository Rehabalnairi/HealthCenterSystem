using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class Admins : User
    {
        
        public Admins(string name, string email, string password) : base(name, email, password, "99999", "Admin") // constructor to initialize admin properties
        {
            this.IsActive = true; // default active status is true
        }
    }
}
