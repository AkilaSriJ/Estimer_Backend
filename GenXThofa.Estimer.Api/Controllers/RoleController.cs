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
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var roles = await _roleService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<RoleDto>>.SuccessResponse(roles, "Roles Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound(ApiResponseDto<ClientDto>.ErrorResponse("Role Not Found"));
            }
            return Ok(ApiResponseDto<RoleDto>.SuccessResponse(role, "role Fetched Successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            var createdRole = await _roleService.CreateAsync(dto);
            if (createdRole == null)
                return BadRequest(createdRole);
            return Ok(ApiResponseDto<RoleDto>.SuccessResponse(createdRole, "Role Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateRoleDto dto)
        {
            var updatedRole = await _roleService.UpdateAsync(id, dto);
            if (updatedRole == null)
            {
                return NotFound(ApiResponseDto<RoleDto>.ErrorResponse("Role not found"));
            }

            return Ok(ApiResponseDto<RoleDto>.SuccessResponse(updatedRole, "Role Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _roleService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("Role Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "Client Deleted Successfully"));
        }
    }
}
