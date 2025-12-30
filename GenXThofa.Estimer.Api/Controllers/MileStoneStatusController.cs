using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.MileStoneStatus;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileStoneStatusController(IMileStoneStatusService mileStoneStatusService) : ControllerBase
    {
        private readonly IMileStoneStatusService _mileStoneStatusService = mileStoneStatusService;
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var mileStoneStatus = await _mileStoneStatusService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<MileStoneStatusDto>>.SuccessResponse(mileStoneStatus, "MileStone Status Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var mileStoneStatus = await _mileStoneStatusService.GetByIdAsync(id);
            if (mileStoneStatus == null)
            {
                return NotFound(ApiResponseDto<ProjectStatusDto>.ErrorResponse("Status Not Found"));
            }
            return Ok(ApiResponseDto<MileStoneStatusDto>.SuccessResponse(mileStoneStatus, "MileStone Status Fetched Successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateMileStoneStatus dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            var createdStatus = await _mileStoneStatusService.CreateAsync(dto);
            if (createdStatus == null)
                return BadRequest(createdStatus);
            return Ok(ApiResponseDto<MileStoneStatusDto>.SuccessResponse(createdStatus, "Status Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, CreateMileStoneStatus dto)
        {
            var updatedStatus = await _mileStoneStatusService.UpdateAsync(id, dto);
            if (updatedStatus == null)
            {
                return NotFound(ApiResponseDto<MileStoneStatusDto>.ErrorResponse("Status not found"));
            }

            return Ok(ApiResponseDto<MileStoneStatusDto>.SuccessResponse(updatedStatus, "Status Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _mileStoneStatusService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("Status Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "MileStoneStatus Deleted Successfully"));
        }
    }
}
