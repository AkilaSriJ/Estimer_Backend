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

            var response = RetrResponse<PagedResult<ProjectDto>>.Success(
                projects,
                $"Successfully fetched {projects.Data.Count()} projects"
            );

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
                return NotFound(RetrResponse<ProjectDto>.Failure(
                       "NOT_FOUND",
                       $"Project with ID {id} not found"
                   ));
            }
            return Ok(RetrResponse<ProjectDto>.Success(project, "Project fetched successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(RetrResponse<ProjectDto>.Failure("VALIDATION_ERROR","Validation failed",errors));

            }
            var createdProject = await _projectService.CreateAsync(dto);
            if (createdProject == null)
                return BadRequest(createdProject);
            return CreatedAtAction(
                   nameof(GetById),
                   new { id = createdProject.ProjectId },
                   RetrResponse<ProjectDto>.Success(createdProject, "Project created successfully")
               );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
        {
            var updatedProject = await _projectService.UpdateAsync(id, dto);
            if (updatedProject == null)
            {
                return NotFound(RetrResponse<ProjectDto>.Failure(
                       "NOT_FOUND",
                       $"Project with ID {id} not found"
                   ));
            }

            return Ok(RetrResponse<ProjectDto>.Success(updatedProject, "Project updated successfully"));

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _projectService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(RetrResponse<bool>.Failure(
                        "NOT_FOUND",
                        $"Project with ID {id} not found"
                    ));
            }
            return Ok(RetrResponse<bool>.Success(true, "Project deleted successfully"));
        }
    }
}
