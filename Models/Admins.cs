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
    public string ToFileString()
        {
            return $"{UserId},{Name},{Email},{Password},{PhoneNumber},{Role},{IsActive}";
        }

        public static Admins FromFileString(string line)
        {
            string[] parts = line.Split(',');
            if (parts.Length != 7) return null;

            int id = int.Parse(parts[0]);
            string name = parts[1];
            string email = parts[2];
            string password = parts[3];
            string phone = parts[4];
            string role = parts[5];
            bool isActive = bool.Parse(parts[6]);

            Admins admin = new Admins(id, name, email, password, phone, role);
            admin.IsActive = isActive;
            return admin;
        }

    }

