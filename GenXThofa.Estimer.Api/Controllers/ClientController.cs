using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using System.Reflection.Metadata.Ecma335;

namespace GenXThofa.Technologies.Estimer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients= await _clientService.GetAllAsync();
            var response= ApiResponseDto<IEnumerable<ClientDto>>.SuccessResponse(clients, "Clients Fetched Sucessfully");
            return Ok(response);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            var createdClient= await _clientService.CreateAsync(dto);
            return Ok(ApiResponseDto<ClientDto>.SuccessResponse(createdClient, "Client Created Successfully"));
        }

        [HttpPut("{id}")]
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
