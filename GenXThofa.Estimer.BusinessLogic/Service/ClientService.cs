using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Model;
using GenXThofa.Technologies.Estimer.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public ClientService(IClientRepository clientRepository, IMapper mapper) 
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ClientDto>> GetAllAsync(Pagination pagination)
        {
            var clients = _clientRepository.GetAll().ApplyPagination(pagination);
            var dtoList= _mapper.Map<List<ClientDto>>(clients.Data);
            return new PagedResult<ClientDto>
            {
                PageNumber=clients.PageNumber,
                PageSize=clients.PageSize,
                TotalRecords=clients.TotalRecords,
                TotalPages=clients.TotalPages,
                Data = dtoList
            };
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client= await _clientRepository.GetByIdAsync(id);
            if(client == null) 
                return null;
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            if (await _clientRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("Email already Exists");
            }
            if(await _clientRepository.ExistsByPhoneAsync(dto.Phone))
            {
                throw new Exception("Phone number already exists");
                            }
            var client= _mapper.Map<Client>(dto);
            client.IsActive = true;
            client.CreatedAt = DateTime.Now;
            await _clientRepository.CreateAsync(client);
            await _clientRepository.SaveChangesAsync();
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto?> UpdateAsync(int id, UpdateClientDto dto)
        {
            var existingClient =await _clientRepository.GetByIdAsync(id);
            if (existingClient == null)
            {
               return null;
            }
            if (existingClient.Email != dto.Email && await _clientRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("Email already Exists");
            }
            if (existingClient.Phone != dto.Phone && await _clientRepository.ExistsByPhoneAsync(dto.Phone))
            {
                throw new Exception("Phone number already exists");
            }
            _mapper.Map(dto, existingClient);
            existingClient.UpdatedAt = DateTime.Now;
            await _clientRepository.UpdateAsync(existingClient);
            await _clientRepository.SaveChangesAsync();
            return _mapper.Map<ClientDto?>(existingClient);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return false;
            if (client.IsActive)
                throw new Exception("Active client cannot be deleted");
            await _clientRepository.DeleteAsync(client);
            await _clientRepository.SaveChangesAsync();
            return true;
        }
    }
}
