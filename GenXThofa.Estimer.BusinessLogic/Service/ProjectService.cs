using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.Client;
using GenXThofa.Technologies.Estimer.Model.Project;
using GenXThofa.Technologies.Estimer.Model.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class ProjectService(IProjectRepository projectRepository,IMapper mapper):IProjectService
    {
        private readonly IProjectRepository _projectRepository=projectRepository?? throw new ArgumentNullException(nameof(projectRepository));
        private readonly IMapper _mapper=mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<PagedResult<ProjectDto>> GetAllAsync(Pagination pagination)
        {
            var roles = _projectRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<ProjectDto>>(roles.Data);
            return new PagedResult<ProjectDto>
            {
                PageNumber = roles.PageNumber,
                PageSize = roles.PageSize,
                TotalRecords = roles.TotalRecords,
                TotalPages = roles.TotalPages,
                Data = dtoList
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return null;
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            project.CreatedAt = DateTime.Now;
            await _projectRepository.CreateAsync(project);
            await _projectRepository.SaveChangesAsync();
            var createdProject = await _projectRepository.GetByIdAsync(project.ProjectId);
            return _mapper.Map<ProjectDto>(createdProject);
        }

        public async Task<ProjectDto> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return null;
            _mapper.Map(dto, existingProject);
            existingProject.UpdatedAt = DateTime.Now;
            await _projectRepository.UpdateAsync(existingProject);
            await _projectRepository.SaveChangesAsync();
            return _mapper.Map<ProjectDto>(existingProject);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return false;
            await _projectRepository.DeleteAsync(project);
            await _projectRepository.SaveChangesAsync();
            return true;
        }
    }
}
