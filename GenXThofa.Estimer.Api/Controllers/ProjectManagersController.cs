using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagersController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;
        [HttpGet]
        [ProducesResponseType(typeof(RetrResponse<List<ProjectManagerDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrResponse<List<ProjectManagerDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProjectManagers()
        {
            var response = await _employeeService.GetProjectManagersAsync();

            if (response.Response.Status == "FAILED")
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RetrResponse<EmployeeDetailDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RetrResponse<EmployeeDetailDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RetrResponse<EmployeeDetailDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);

            if (response.Response.Status == "FAILED")
            {
                if (response.Error.Code == "NOT_FOUND")
                {
                    return NotFound(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
            return Ok(response);
        }

    }
}
