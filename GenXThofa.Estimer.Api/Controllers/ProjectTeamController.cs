using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.ProjectTeamMember;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTeamController(IProjectTeamMemberService projectTeamMemberService) : ControllerBase
    {
        private readonly IProjectTeamMemberService _projectTeamMemberService = projectTeamMemberService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var teamMembers = await _projectTeamMemberService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<ProjectTeamMemberDto>>.SuccessResponse(
                teamMembers,
                "Project Team Members Fetched Successfully"
            );
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var teamMember = await _projectTeamMemberService.GetByIdAsync(id);
            if (teamMember == null)
            {
                return NotFound(ApiResponseDto<ProjectTeamMemberDto>.ErrorResponse("Team Member Not Found"));
            }
            return Ok(ApiResponseDto<ProjectTeamMemberDto>.SuccessResponse(
                teamMember,
                "Project Team Member Fetched Successfully"
            ));
        }

        [HttpGet("by-project")]
        public async Task<IActionResult> GetByProjectId([FromQuery] int projectId)
        {
            var teamMembers = await _projectTeamMemberService.GetTeamMembersByProjectId(projectId);
            return Ok(teamMembers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectTeamMemberDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse(
                    "Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                ));

            try
            {
                var createdTeamMember = await _projectTeamMemberService.CreateAsync(dto);
                if (createdTeamMember == null)
                    return BadRequest(createdTeamMember);

                return Ok(ApiResponseDto<ProjectTeamMemberDto>.SuccessResponse(
                    createdTeamMember,
                    "Team Member Created Successfully"
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<object>.ErrorResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, CreateProjectTeamMemberDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse(
                    "Validation failed",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                ));

            try
            {
                var updatedTeamMember = await _projectTeamMemberService.UpdateAsync(id, dto);
                if (updatedTeamMember == null)
                {
                    return NotFound(ApiResponseDto<ProjectTeamMemberDto>.ErrorResponse("Team Member Not Found"));
                }
                return Ok(ApiResponseDto<ProjectTeamMemberDto>.SuccessResponse(
                    updatedTeamMember,
                    "Team Member Updated Successfully"
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<object>.ErrorResponse(ex.Message));
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _projectTeamMemberService.DeleteAsync(id);
                if (!isDeleted)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResponse("Team Member Not Found"));
                }
                return Ok(ApiResponseDto<bool>.SuccessResponse(true, "Team Member Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseDto<object>.ErrorResponse(ex.Message));
            }
        }
        [HttpGet("allowed-employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllowedEmployees()
        {
            var employees = new List<object>
        {
            new { EmployeeId = 101, EmployeeName = "Rajesh Kumar", Designation = "Senior Developer", HourlyRate = 75.00m },
            new { EmployeeId = 102, EmployeeName = "Priya Sharma", Designation = "Team Lead", HourlyRate = 90.00m },
            new { EmployeeId = 103, EmployeeName = "Karthik Raja", Designation = "Developer", HourlyRate = 60.00m },
            new { EmployeeId = 104, EmployeeName = "Divya Lakshmi", Designation = "UI/UX Designer", HourlyRate = 65.00m },
            new { EmployeeId = 105, EmployeeName = "Arun Prakash", Designation = "QA Engineer", HourlyRate = 55.00m }
        };

            return Ok(ApiResponseDto<List<object>>.SuccessResponse(
                employees,
                "Allowed Employees Fetched Successfully"
            ));
        }
    }
}
