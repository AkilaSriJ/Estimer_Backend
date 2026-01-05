using GenXThofa.Technologies.Estimer.Data.ExternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetAllEmployeesAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        Task<List<EmployeeModel>> GetEmployeesByDepartmentAsync(string department);
    }
}
