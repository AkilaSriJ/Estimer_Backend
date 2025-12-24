using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using System.Reflection.Metadata.Ecma335;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService=clientService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            var clients= await _clientService.GetAllAsync(pagination);
            var response= ApiResponseDto<PagedResult<ClientDto>>.SuccessResponse(clients, "Clients Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound(ApiResponseDto<ClientDto>.ErrorResponse("Client Not Found"));
            }
            return Ok(ApiResponseDto<ClientDto>.SuccessResponse(client, "Client Fetched Successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ApiResponseDto<object>.ErrorResponse("Validation failed",ModelState.Values.SelectMany(v=>v.Errors).Select(e=>e.ErrorMessage).ToList()));
            var createdClient= await _clientService.CreateAsync(dto);
            if (createdClient==null)
                return BadRequest(createdClient);
            return Ok(ApiResponseDto<ClientDto>.SuccessResponse(createdClient, "Client Created Successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdateClientDto dto)
        {
            var updatedClient=await _clientService.UpdateAsync(id, dto);
            if(updatedClient == null)
            {
                return NotFound(ApiResponseDto<ClientDto>.ErrorResponse("Client not found"));
            }
           
            return Ok(ApiResponseDto<ClientDto>.SuccessResponse(updatedClient, "Client Updated Successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted= await _clientService.DeleteAsync(id);
            if(!isDeleted)
            {
                return NotFound(ApiResponseDto<bool>.ErrorResponse("Client Not Found"));
            }
            return Ok(ApiResponseDto<bool>.SuccessResponse(true, "Client Deleted Successfully"));
        }
    }
}
