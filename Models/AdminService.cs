using HealthCenterSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace HealthCenterSystem.Services
{
    class AdminService
    {
        private List<Admins> admins = new List<Admins>();

        public void AddAdmin(Admins admin)
        {
            admins.Add(admin);
        }

        public List<Admins> GetAllAdmins()
        {
            return admins;
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var admin in admins)
                {
                    writer.WriteLine(admin.ToFileString());
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            admins.Clear();
            if (!File.Exists(filePath)) return;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Admins admin = Admins.FromFileString(line);
                if (admin != null)
                {
                    admins.Add(admin);
                }
            }
        }
    }
}
