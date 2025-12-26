using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.Client;
using GenXThofa.Technologies.Estimer.Model.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class RoleService(IRoleRepository roleRepository,IMapper mapper):IRoleService
    {
        private readonly IRoleRepository _roleRepository= roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        private readonly IMapper _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PagedResult<RoleDto>> GetAllAsync(Pagination pagination)
        {
            var roles = _roleRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<RoleDto>>(roles.Data);
            return new PagedResult<RoleDto>
            {
                PageNumber = roles.PageNumber,
                PageSize = roles.PageSize,
                TotalRecords = roles.TotalRecords,
                TotalPages = roles.TotalPages,
                Data = dtoList
            };
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return null;
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var existingRole= await _roleRepository.GetByNameAsync(dto.RoleName);
            if (existingRole != null)
            {
                throw new Exception("Role Name already Exists");
            }

            var role = _mapper.Map<Role>(dto);
            role.CreatedAt = DateTime.Now;
            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> UpdateAsync(int id,UpdateRoleDto dto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
                return null;
            _mapper.Map(dto, existingRole);
            existingRole.UpdatedAt = DateTime.Now;
            await _roleRepository.UpdateAsync(existingRole);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleDto>(existingRole);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return false;
            if (role.IsActive)
                throw new Exception("Active Role cannot be deleted");
            await _roleRepository.DeleteAsync(role);
            await _roleRepository.SaveChangesAsync();
            return true;
        }
    }
}
