using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCenterSystem.Services
{
    interface IBranchService
    {
        void AddBranch(string name, string address, string phoneNumber);
        void UpdateBranch(int id, string name, string address, string phoneNumber);
        void DeleteBranch(int id);
        void SetBranchStatus(int id, bool isActive);
    }
}
