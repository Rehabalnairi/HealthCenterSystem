using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Models
{
    class Admins : User
    {
        public List<Doctor> doctors { get; set; }
        public Admins(int userId, string name, string email, string password, string phoneNumber, string role)
    : base(userId, name, email, password, phoneNumber, role)
        {
            this.IsActive = true;
        }




    }
}

