using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using GenXThofa.Technologies.Estimer.Model.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class ProjectStatusService(IProjectStatusRepository projectStatusRepository,IMapper mapper):IProjectStatusService
    {
        private readonly IProjectStatusRepository _projectStatusRepository=projectStatusRepository ?? throw new ArgumentNullException(nameof(projectStatusRepository));
        private readonly IMapper _mapper= mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PagedResult<ProjectStatusDto>> GetAllAsync(Pagination pagination)
        {
            var status = _projectStatusRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<ProjectStatusDto>>(status.Data);
            return new PagedResult<ProjectStatusDto>
            {
                PageNumber = status.PageNumber,
                PageSize = status.PageSize,
                TotalRecords = status.TotalRecords,
                TotalPages = status.TotalPages,
                Data = dtoList
            };
        }

        public async Task<ProjectStatusDto?> GetByIdAsync(int id)
        {
            var status = await _projectStatusRepository.GetByIdAsync(id);
            if (status == null)
                return null;
            return _mapper.Map<ProjectStatusDto>(status);
        }

        public async Task<ProjectStatusDto> CreateAsync(CreateProjectStatusDto dto)
        {
            var existingStatus = await _projectStatusRepository.GetByNameAsync(dto.StatusName);
            if (existingStatus != null)
            {
                throw new Exception("Status already Exists");
            }

            var status = _mapper.Map<ProjectStatus>(dto);
            status.CreatedAt = DateTime.Now;
            await _projectStatusRepository.CreateAsync(status);
            await _projectStatusRepository.SaveChangesAsync();
            return _mapper.Map<ProjectStatusDto>(status);
        }

        public async Task<ProjectStatusDto> UpdateAsync(int id, CreateProjectStatusDto dto)
        {
            var existingStatus = await _projectStatusRepository.GetByIdAsync(id);
            if (existingStatus == null)
                return null;
            _mapper.Map(dto, existingStatus);
            existingStatus.UpdatedAt = DateTime.Now;
            await _projectStatusRepository.UpdateAsync(existingStatus);
            await _projectStatusRepository.SaveChangesAsync();
            return _mapper.Map<ProjectStatusDto>(existingStatus);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _projectStatusRepository.GetByIdAsync(id);
            if (status == null)
                return false;
            if (status.IsActive)
                throw new Exception("Active Role cannot be deleted");
            await _projectStatusRepository.DeleteAsync(status);
            await _projectStatusRepository.SaveChangesAsync();
            return true;
        }
    }
}
