using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Interface
{
    public interface IEmployeeService
    {
        Task<RetrResponse<List<ProjectManagerDto>>> GetProjectManagersAsync();
        Task<RetrResponse<EmployeeDetailDto>> GetEmployeeByIdAsync(int employeeId);

    }
}
