using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Client;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using GenXThofa.Technologies.Estimer.Model.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusController(IProjectStatusService projectStatusService) : ControllerBase
    {
        private readonly IProjectStatusService _projectStatusService=projectStatusService;
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var projectStatus = await _projectStatusService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<ProjectStatusDto>>.SuccessResponse(projectStatus, "Project Status Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var projectStatus = await _projectStatusService.GetByIdAsync(id);
            if (projectStatus == null)
            {
                return NotFound(ApiResponseDto<ProjectStatusDto>.ErrorResponse("Status Not Found"));
            }
            return Ok(ApiResponseDto<ProjectStatusDto>.SuccessResponse(projectStatus, "Project Status Fetched Successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            var createdStatus = await _projectStatusService.CreateAsync(dto);
            if (createdStatus == null)
                return BadRequest(createdStatus);
            return Ok(ApiResponseDto<ProjectStatusDto>.SuccessResponse(createdStatus, "Status Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, CreateProjectStatusDto dto)
        {
            var updatedStatus = await _projectStatusService.UpdateAsync(id, dto);
            if (updatedStatus == null)
            {
                return NotFound(ApiResponseDto<ProjectStatusDto>.ErrorResponse("Status not found"));
            }

            return Ok(ApiResponseDto<ProjectStatusDto>.SuccessResponse(updatedStatus, "Status Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _projectStatusService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("Status Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "ProjectStatus Deleted Successfully"));
        }
    }
}
