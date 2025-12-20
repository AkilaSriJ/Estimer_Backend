using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
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

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = _clientRepository.GetAll().ToList();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);

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
            var client= _mapper.Map<Client>(dto);
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
            _mapper.Map(dto, existingClient);
            await _clientRepository.UpdateAsync(existingClient);
            await _clientRepository.SaveChangesAsync();
            return _mapper.Map<ClientDto?>(existingClient);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return false;
            await _clientRepository.DeleteAsync(client);
            await _clientRepository.SaveChangesAsync();
            return true;
        }
    }
}
