using GenXThofa.Technologies.Estimer.Data.ExternalModels;
using GenXThofa.Technologies.Estimer.Data.Interface;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace GenXThofa.Technologies.Estimer.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(HttpClient httpClient, ILogger<EmployeeRepository> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (_httpClient.BaseAddress == null)
            {
                _logger.LogError("HttpClient BaseAddress is NULL!");
                throw new InvalidOperationException("HttpClient BaseAddress must be configured");
            }

            _logger.LogInformation("EmployeeRepository initialized with BaseAddress: {BaseAddress}",
                _httpClient.BaseAddress);
        }

        public async Task<List<EmployeeModel>> GetAllEmployeesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all employees from: {Url}",
                    $"{_httpClient.BaseAddress}employees");

                var response = await _httpClient.GetAsync("employees");

                _logger.LogInformation("GetAllEmployees Response Status: {StatusCode}", response.StatusCode);

                response.EnsureSuccessStatusCode();

                var employees = await response.Content.ReadFromJsonAsync<List<EmployeeModel>>();

                _logger.LogInformation("Successfully fetched {Count} employees", employees?.Count ?? 0);

                return employees ?? new List<EmployeeModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all employees from JSON server");
                throw;
            }
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                // Use query parameter instead of path parameter
                var url = $"employees?employeeId={employeeId}";
                _logger.LogInformation("Fetching employee from: {Url}", $"{_httpClient.BaseAddress}{url}");

                var response = await _httpClient.GetAsync(url);

                _logger.LogInformation("GetEmployeeById Response Status: {StatusCode} for ID: {EmployeeId}",
                    response.StatusCode, employeeId);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogWarning("Employee with ID {EmployeeId} not found (404)", employeeId);
                    return null;
                }

                response.EnsureSuccessStatusCode();

                // JSON Server returns an array when using query parameters
                var employees = await response.Content.ReadFromJsonAsync<List<EmployeeModel>>();

                if (employees == null || !employees.Any())
                {
                    _logger.LogWarning("Employee with ID {EmployeeId} not found (empty result)", employeeId);
                    return null;
                }

                var employee = employees.First();

                _logger.LogInformation("Employee found - ID: {Id}, Name: {Name}, Department: {Dept}, Active: {Active}",
                    employee.EmployeeId,
                    employee.EmployeeName,
                    employee.Department ?? "NULL",
                    employee.IsActive);

                return employee;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found (HttpRequestException)", employeeId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employee {EmployeeId} from JSON server", employeeId);
                throw;
            }
        }

        public async Task<List<EmployeeModel>> GetEmployeesByDepartmentAsync(string department)
        {
            try
            {
                _logger.LogInformation("Fetching employees from department: {Department}", department);

                var allEmployees = await GetAllEmployeesAsync();

                var filtered = allEmployees
                    .Where(e => e.Department?.Equals(department, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();

                _logger.LogInformation("Found {Count} employees in {Department} department out of {Total} total",
                    filtered.Count, department, allEmployees.Count);

                return filtered;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching employees by department {Department}", department);
                throw;
            }
        }
    }
}