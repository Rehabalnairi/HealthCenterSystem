using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class Doctor:User
    {
        public Doctor(int id, string name, string email, string password)
            : base(id, name, email, password, "98376256", "Doctor") // constructor to initialize doctor properties
        {
            // Doctor specific initialization can go here if needed
        }
    }
}
