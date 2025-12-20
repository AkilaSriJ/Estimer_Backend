using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users=await _userService.GetAllAsync();
            var response = ApiResponseDto<IEnumerable<UserDto>>.SuccessResponse(users, "Users Fetched Successfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var user=await _userService.GetByIdAsync(id);
            if(user ==null)
            {
                return NotFound(ApiResponseDto<UserDto>.ErrorResponse("User Not Found"));
            }
            return Ok(ApiResponseDto<UserDto>.SuccessResponse(user, "User Fetched Successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            var createdUser= await _userService.CreateAsync(createUserDto);
            return Ok(ApiResponseDto<UserDto>.SuccessResponse(createdUser, "User Created Successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateUserDto updateUserDto)
        {
            var updatedUser = await _userService.UpdateAsync(id,updateUserDto);
            if(updatedUser == null)
            {
                return NotFound(ApiResponseDto<UserDto>.ErrorResponse("User Not Found"));
            }
            return Ok(ApiResponseDto<UserDto>.SuccessResponse(updatedUser, "User Updated Successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.DeleteAsync(id);
            if(!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("User Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "User Deleted Successfully"));
        }
    }
}
