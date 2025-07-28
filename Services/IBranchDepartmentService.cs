using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HealthCenterSystem.Services
{
    public interface IBranchDepartmentService
    {
       List<string> GetAllBranchDepartments(); // Returns a list of all branch department names
        List<string> GetActiveBranchDepartments(); // Returns a list of active branch department names
        List<string> GetInactiveBranchDepartments(); // Returns a list of inactive branch department names
    }
}
