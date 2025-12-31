using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.ApiResponse;
using GenXThofa.Technologies.Estimer.Model.Client;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class ClientService(IClientRepository clientRepository, IProjectRepository projectRepository, IMapper mapper) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<ClientDto>> GetAllAsync(Pagination pagination)
        {
            var clients = _clientRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<ClientDto>>(clients.Data);
            return new PagedResult<ClientDto>
            {
                PageNumber = clients.PageNumber,
                PageSize = clients.PageSize,
                TotalRecords = clients.TotalRecords,
                TotalPages = clients.TotalPages,
                Data = dtoList
            };
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return null;
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateAsync(CreateClientDto dto)
        {
            if (await _clientRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("Email already Exists");
            }
            if (await _clientRepository.ExistsByPhoneAsync(dto.Phone))
            {
                throw new Exception("Phone number already exists");

            }
            TrimClientFields(dto);
            ValidatePhone(dto.Country, dto.Phone);

            var client = _mapper.Map<Client>(dto);
            client.CreatedAt = DateTime.Now;
            await _clientRepository.CreateAsync(client);
            await _clientRepository.SaveChangesAsync();
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto?> UpdateAsync(int id, UpdateClientDto dto)
        {
            var existingClient = await _clientRepository.GetByIdAsync(id);
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

            TrimClientFields(dto);
            ValidatePhone(dto.Country, dto.Phone);

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
            bool hasProjects = _projectRepository.GetAll().Any(p => p.ClientId == client.ClientId);
            if (hasProjects)
            {
                // return ApiResponseDto<bool>.ErrorResponse("Client cannot be deleted because it has associate Projects");
            }
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

        private void TrimClientFields(UpdateClientDto dto)
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
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required for phone validation");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone number is required");

            phone = phone.Trim();

            // Get country code from country name
            string countryCode = GetCountryCode(country);

            var phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                // Parse the phone number with country code
                var numberProto = phoneUtil.Parse(phone, countryCode);

                // Validate if it's a valid number for that country
                if (!phoneUtil.IsValidNumber(numberProto))
                {
                    throw new ArgumentException($"Invalid phone number for {country}");
                }

                // Check if it's a mobile number
                var numberType = phoneUtil.GetNumberType(numberProto);
                if (numberType != PhoneNumberType.MOBILE &&
                    numberType != PhoneNumberType.FIXED_LINE_OR_MOBILE)
                {
                    throw new ArgumentException("Phone number must be a mobile number");
                }
            }
            catch (NumberParseException ex)
            {
                throw new ArgumentException($"Invalid phone number format: {ex.Message}");
            }
        }

        private string GetCountryCode(string countryName)
        {
            var countryMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                // Common countries
                ["india"] = "IN",
                ["usa"] = "US",
                ["united states"] = "US",
                ["united states of america"] = "US",
                ["uk"] = "GB",
                ["united kingdom"] = "GB",
                ["china"] = "CN",
                ["canada"] = "CA",
                ["australia"] = "AU",
                ["germany"] = "DE",
                ["france"] = "FR",
                ["japan"] = "JP",
                ["south korea"] = "KR",
                ["singapore"] = "SG",
                ["malaysia"] = "MY",
                ["thailand"] = "TH",
                ["indonesia"] = "ID",
                ["philippines"] = "PH",
                ["vietnam"] = "VN",
                ["united arab emirates"] = "AE",
                ["uae"] = "AE",
                ["saudi arabia"] = "SA",
                ["brazil"] = "BR",
                ["mexico"] = "MX",
                ["spain"] = "ES",
                ["italy"] = "IT",
                ["netherlands"] = "NL",
                ["belgium"] = "BE",
                ["switzerland"] = "CH",
                ["sweden"] = "SE",
                ["norway"] = "NO",
                ["denmark"] = "DK",
                ["poland"] = "PL",
                ["russia"] = "RU",
                ["south africa"] = "ZA",
                ["new zealand"] = "NZ",
                ["ireland"] = "IE",
                ["pakistan"] = "PK",
                ["bangladesh"] = "BD",
                ["sri lanka"] = "LK",
                ["nepal"] = "NP"
                // Add more countries as needed
            };

            if (countryMap.TryGetValue(countryName, out var code))
                return code;

            throw new ArgumentException($"Unsupported country: {countryName}. Please use a valid country name.");
        }
    }
}