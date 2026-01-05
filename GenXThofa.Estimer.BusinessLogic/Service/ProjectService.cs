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
    public class ProjectService(IProjectRepository projectRepository, IMapper mapper, IEmployeeService employeeService) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IEmployeeService _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));

        public async Task<PagedResult<ProjectDto>> GetAllAsync(Pagination pagination)
        {
            var pagedProjects = _projectRepository.GetAll().ApplyPagination(pagination);
            var projectDtos = _mapper.Map<List<ProjectDto>>(pagedProjects.Data);

            foreach (var projectDto in projectDtos)
            {
                if (projectDto.ProjectManagerId.HasValue)
                {
                    var employeeResponse = await _employeeService.GetEmployeeByIdAsync(projectDto.ProjectManagerId.Value);
                    if (employeeResponse?.Response?.Status == "SUCCESS" && employeeResponse.Result != null)
                    {
                        projectDto.ProjectManager = employeeResponse.Result.EmployeeName;
                    }
                }
            }

            return new PagedResult<ProjectDto>
            {
                PageNumber = pagedProjects.PageNumber,
                PageSize = pagedProjects.PageSize,
                TotalRecords = pagedProjects.TotalRecords,
                TotalPages = pagedProjects.TotalPages,
                Data = projectDtos
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return null;

            var projectDto = _mapper.Map<ProjectDto>(project);

            if (projectDto.ProjectManagerId.HasValue)
            {
                var employeeResponse = await _employeeService.GetEmployeeByIdAsync(projectDto.ProjectManagerId.Value);
                if (employeeResponse?.Response?.Status == "SUCCESS" && employeeResponse.Result != null)
                {
                    projectDto.ProjectManager = employeeResponse.Result.EmployeeName;
                }
            }

            return projectDto;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        { 
          
            if (dto.ProjectManagerId.HasValue)
            {
                await ValidateProjectManagerAsync(dto.ProjectManagerId.Value);
            }

            var project = _mapper.Map<Project>(dto);
            project.CreatedAt = DateTime.UtcNow;
            project.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.CreateAsync(project);
            await _projectRepository.SaveChangesAsync();

            var createdProject = await _projectRepository.GetByIdAsync(project.ProjectId);
            var projectDto = _mapper.Map<ProjectDto>(createdProject);

            if (projectDto.ProjectManagerId.HasValue)
            {
                var employeeResponse = await _employeeService.GetEmployeeByIdAsync(projectDto.ProjectManagerId.Value);
                if (employeeResponse?.Response?.Status == "SUCCESS" && employeeResponse.Result != null)
                {
                    projectDto.ProjectManager = employeeResponse.Result.EmployeeName;
                }
            }

            return projectDto;
        }

        public async Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(id);
            if (existingProject == null)
                return null;

            if (dto.ProjectManagerId.HasValue)
            {
                await ValidateProjectManagerAsync(dto.ProjectManagerId.Value);
            }

            _mapper.Map(dto, existingProject);
            existingProject.UpdatedAt = DateTime.UtcNow;

            await _projectRepository.UpdateAsync(existingProject);
            await _projectRepository.SaveChangesAsync();

            var updatedProject = await _projectRepository.GetByIdAsync(id);
            var projectDto = _mapper.Map<ProjectDto>(updatedProject);

            if (projectDto.ProjectManagerId.HasValue)
            {
                var employeeResponse = await _employeeService.GetEmployeeByIdAsync(projectDto.ProjectManagerId.Value);
                if (employeeResponse?.Response?.Status == "SUCCESS" && employeeResponse.Result != null)
                {
                    projectDto.ProjectManager = employeeResponse.Result.EmployeeName;
                }
            }

            return projectDto;
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

        private async Task ValidateProjectManagerAsync(int employeeId)
        {
            var employeeResponse = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (employeeResponse?.Response?.Status != "SUCCESS" || employeeResponse.Result == null)
            {
                throw new InvalidOperationException($"Project Manager with ID {employeeId} not found");
            }

            if (string.IsNullOrEmpty(employeeResponse.Result.Department))
            {
                throw new InvalidOperationException($"Employee {employeeId} has no department assigned");
            }

            if (!employeeResponse.Result.Department.Equals("Project Management", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Employee {employeeResponse.Result.EmployeeName} (ID: {employeeId}) is from {employeeResponse.Result.Department} department, not Project Management"
                );
            }

            if (!employeeResponse.Result.IsActive)
            {
                throw new InvalidOperationException(
                    $"Employee {employeeResponse.Result.EmployeeName} (ID: {employeeId}) is not active"
                );
            }
        }
    }
}
