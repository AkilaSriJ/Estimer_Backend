using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Client;
using GenXThofa.Technologies.Estimer.Model.Project;
using GenXThofa.Technologies.Estimer.Model.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var projects = await _projectService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<ProjectDto>>.SuccessResponse(projects, "Project Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var project= await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound(ApiResponseDto<ProjectDto>.ErrorResponse("Project Not Found"));
            }
            return Ok(ApiResponseDto<ProjectDto>.SuccessResponse(project, "Project Fetched Successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            var createdProject = await _projectService.CreateAsync(dto);
            if (createdProject == null)
                return BadRequest(createdProject);
            return Ok(ApiResponseDto<ProjectDto>.SuccessResponse(createdProject, "Project Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
        {
            var updatedProject = await _projectService.UpdateAsync(id, dto);
            if (updatedProject == null)
            {
                return NotFound(ApiResponseDto<ProjectDto>.ErrorResponse("Project not found"));
            }

            return Ok(ApiResponseDto<ProjectDto>.SuccessResponse(updatedProject, "Project Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _projectService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("Project Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "Project Deleted Successfully"));
        }
    }
}
