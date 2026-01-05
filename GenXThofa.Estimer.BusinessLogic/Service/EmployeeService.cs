using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Data.ExternalModels;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Employee;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class EmployeeService(IEmployeeRepository employeeRepository,IMapper mapper) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository= employeeRepository;
        private readonly IMapper _mapper= mapper;

        
        public async Task<RetrResponse<List<ProjectManagerDto>>> GetProjectManagersAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesByDepartmentAsync("Project Management");

                if (employees == null || !employees.Any())
                {
                    return RetrResponse<List<ProjectManagerDto>>.Success(
                        new List<ProjectManagerDto>(),
                        "No project managers found"
                    );
                }

                var activeEmployees = employees.Where(e => e.IsActive).ToList();
            
                var projectManagers = _mapper.Map<List<ProjectManagerDto>>(activeEmployees);

                return RetrResponse<List<ProjectManagerDto>>.Success(
                    projectManagers,
                    $"Successfully fetched {projectManagers.Count} project managers"
                );
            }
            catch (Exception ex)
            {
                return RetrResponse<List<ProjectManagerDto>>.Failure(
                    "INTERNAL_ERROR",
                    "An unexpected error occurred",
                    new List<string> { ex.Message }
                );
            }
        }

        public async Task<RetrResponse<EmployeeDetailDto>> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return RetrResponse<EmployeeDetailDto>.Failure(
                    "NOT_FOUND",
                    $"Employee with ID {employeeId} not found"
                );
            }

            var employeeDto = _mapper.Map<EmployeeDetailDto>(employee);

            return RetrResponse<EmployeeDetailDto>.Success(
                employeeDto,
                "Employee details fetched successfully"
            );
        }
    }
}
