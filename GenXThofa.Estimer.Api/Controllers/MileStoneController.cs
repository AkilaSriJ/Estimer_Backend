using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.MileStone;
using GenXThofa.Technologies.Estimer.Model.Project;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileStoneController(IMileStoneService mileStoneService): ControllerBase
    {
        private readonly IMileStoneService _mileStoneService= mileStoneService;
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var mileStones = await _mileStoneService.GetAllAsync(pagination);
            var response = ApiResponseDto<PagedResult<MileStoneDto>>.SuccessResponse(mileStones, "MileStones Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var mileStone = await _mileStoneService.GetByIdAsync(id);
            if (mileStone == null)
            {
                return NotFound(ApiResponseDto<MileStoneDto>.ErrorResponse("MileStone Not Found"));
            }
            return Ok(ApiResponseDto<MileStoneDto>.SuccessResponse(mileStone, "MileStone Fetched Successfully"));
        }
        
        [HttpGet("by-project")]
        public async Task<IActionResult> GetByProjectId([FromQuery] int projectId)
        {
            var milestones = await _mileStoneService.GetMileStonesByProjectId(projectId);
            return Ok(milestones);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateMileStone dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            var createdMileStone = await _mileStoneService.CreateAsync(dto);
            if (createdMileStone == null)
                return BadRequest(createdMileStone);
            return Ok(ApiResponseDto<MileStoneDto>.SuccessResponse(createdMileStone, "MileStone Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateMileStone dto)
        {
            var updatedMileStone = await _mileStoneService.UpdateAsync(id, dto);
            if (updatedMileStone == null)
            {
                return NotFound(ApiResponseDto<MileStoneDto>.ErrorResponse("MileStone not found"));
            }

            return Ok(ApiResponseDto<MileStoneDto>.SuccessResponse(updatedMileStone, "MileStone Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _mileStoneService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("MileStone Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "MileStone Deleted Successfully"));
        }
    }
}
