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
    public class ClientService(IClientRepository clientRepository, IMapper mapper) : IClientService
    {
        private readonly IClientRepository _clientRepository=clientRepository;
        private readonly IMapper _mapper = mapper;

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
            TrimClientFields(dto);
            ValidatePhone(dto.Country, dto.Phone);

            var client= _mapper.Map<Client>(dto);
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

        private void TrimClientFields(CreateClientDto dto)
        {
            dto.CompanyName = dto.CompanyName?.Trim();
            dto.CompanyContactPerson = dto.CompanyContactPerson?.Trim();
            dto.Email = dto.Email?.Trim();
            dto.Phone = dto.Phone?.Trim();
            dto.AddressLine1 = dto.AddressLine1?.Trim();
            dto.AddressLine2 = dto.AddressLine2?.Trim();
            dto.City = dto.City?.Trim();
            dto.StateProvince = dto.StateProvince?.Trim();
            dto.PostalCode = dto.PostalCode?.Trim();
            dto.Country = dto.Country?.Trim();
        }

        private void ValidatePhone(string country, string phone)
        {
            phone = phone.Trim();

            if (!phone.All(char.IsDigit))
                throw new ArgumentException("Phone number must contain only digits");

            switch (country.ToLower())
            {
                case "india":
                    if (phone.Length != 10)
                        throw new ArgumentException("Indian mobile number must be 10 digits");
                    break;

                case "usa":
                    if (phone.Length != 10)
                        throw new ArgumentException("US mobile number must be 10 digits");
                    break;

                case "uk":
                    if (phone.Length < 10 || phone.Length > 11)
                        throw new ArgumentException("UK mobile number must be 10–11 digits");
                    break;

                default:
                    if (phone.Length < 7 || phone.Length > 15)
                        throw new ArgumentException("Invalid phone number length");
                    break;
            }
        }


    }
}
