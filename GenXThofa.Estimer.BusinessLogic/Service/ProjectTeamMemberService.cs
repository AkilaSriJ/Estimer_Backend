using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.ProjectTeamMember;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class ProjectTeamMemberService(IProjectTeamMemberRepository projectTeamMemberRepository,IRoleRepository roleRepository, IMapper mapper): IProjectTeamMemberService
    {
        private readonly IProjectTeamMemberRepository _projectTeamMemberRepository= projectTeamMemberRepository ?? throw new ArgumentNullException(nameof(projectTeamMemberRepository));
        private readonly IMapper _mapper= mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IRoleRepository _roleRepository= roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        private class HardcodedEmployee
        {
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string Designation { get; set; }
            public decimal HourlyRate { get; set; }
        }
        private readonly List<HardcodedEmployee> _hardcodedEmployees = new List<HardcodedEmployee>
    {
        new HardcodedEmployee
        {
            EmployeeId = 101,
            EmployeeName = "Rajesh Kumar",
            Designation = "Senior Developer",
            HourlyRate = 75.00m
        },
        new HardcodedEmployee
        {
            EmployeeId = 102,
            EmployeeName = "Priya Sharma",
            Designation = "Team Lead",
            HourlyRate = 90.00m
        },
        new HardcodedEmployee
        {
            EmployeeId = 103,
            EmployeeName = "Karthik Raja",
            Designation = "Developer",
            HourlyRate = 60.00m
        },
        new HardcodedEmployee
        {
            EmployeeId = 104,
            EmployeeName = "Divya Lakshmi",
            Designation = "UI/UX Designer",
            HourlyRate = 65.00m
        },
        new HardcodedEmployee
        {
            EmployeeId = 105,
            EmployeeName = "Arun Prakash",
            Designation = "QA Engineer",
            HourlyRate = 55.00m
        }
    };

        public async Task<PagedResult<ProjectTeamMemberDto>> GetAllAsync(Pagination pagination)
        {
            var teamMember = _projectTeamMemberRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<ProjectTeamMemberDto>>(teamMember.Data);
            return new PagedResult<ProjectTeamMemberDto>
            {
                PageNumber = teamMember.PageNumber,
                PageSize = teamMember.PageSize,
                TotalRecords = teamMember.TotalRecords,
                TotalPages = teamMember.TotalPages,
                Data = dtoList
            };
        }
        public async Task<ProjectTeamMemberDto> GetByIdAsync(int id)
        {
            var teamMember = await _projectTeamMemberRepository.GetByIdAsync(id);
            if (teamMember == null)
                return null;
            return _mapper.Map<ProjectTeamMemberDto>(teamMember);
        }
        public async Task<List<ProjectTeamMemberDto>> GetTeamMembersByProjectId(int projectId)
        {
            var project = await _projectTeamMemberRepository.GetByProjectId(projectId);
            if (project == null)
                return null;
            return _mapper.Map<List<ProjectTeamMemberDto>>(project);
        }

        public async Task<ProjectTeamMemberDto> CreateAsync(CreateProjectTeamMemberDto dto)
        {
            var employee = _hardcodedEmployees.FirstOrDefault(e => e.EmployeeId == dto.EmployeeId);
            if (employee == null)
            {
                throw new Exception($"Invalid Employee ID. Allowed IDs: {string.Join(", ", _hardcodedEmployees.Select(e => e.EmployeeId))}");
            }
           

            var existingTeam = await _projectTeamMemberRepository.GetAll().FirstOrDefaultAsync(x => x.ProjectId == dto.ProjectId &&
                                     x.EmployeeId == dto.EmployeeId &&
                                     x.IsActive);
            if (existingTeam != null)
            {
                throw new Exception("Employee already assigned to this project");
            }

            var teamMember = _mapper.Map<ProjectTeamMember>(dto);

            teamMember.EmployeeName = employee.EmployeeName;
            teamMember.Designation = employee.Designation;
            teamMember.HourlyRate = employee.HourlyRate;

            teamMember.TotalCost = employee.HourlyRate * dto.EstimatedHours;
            teamMember.IsActive = true;
            teamMember.CreatedAt = DateTime.Now;

            await _projectTeamMemberRepository.CreateAsync(teamMember);
            await _projectTeamMemberRepository.SaveChangesAsync();

            return _mapper.Map<ProjectTeamMemberDto>(teamMember);
        }

        public async Task<ProjectTeamMemberDto> UpdateAsync(int id, CreateProjectTeamMemberDto dto)
        {
            var employee = _hardcodedEmployees.FirstOrDefault(e => e.EmployeeId == dto.EmployeeId);
            if (employee == null)
            {
                throw new Exception($"Invalid Employee ID. Allowed IDs: {string.Join(", ", _hardcodedEmployees.Select(e => e.EmployeeId))}");
            }
            
            var existingMember = await _projectTeamMemberRepository.GetAll()
            .FirstOrDefaultAsync(x => x.ProjectId == dto.ProjectId &&
                                     x.EmployeeId == dto.EmployeeId &&
                                     x.IsActive);
            if (existingMember != null)
            {
                throw new Exception("Employee already assigned to this project");
            }
            var teamMember = _mapper.Map<ProjectTeamMember>(dto);

            teamMember.EmployeeName = employee.EmployeeName;
            teamMember.Designation = employee.Designation;
            teamMember.HourlyRate = employee.HourlyRate;

            teamMember.TotalCost = employee.HourlyRate * dto.EstimatedHours;

            teamMember.IsActive = true;
            teamMember.CreatedAt = DateTime.Now;

            await _projectTeamMemberRepository.CreateAsync(teamMember);
            await _projectTeamMemberRepository.SaveChangesAsync();

            return _mapper.Map<ProjectTeamMemberDto>(teamMember);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var teamMember = await _projectTeamMemberRepository.GetByIdAsync(id);
            if (teamMember == null)
                return false;

            if (!teamMember.IsActive)
                throw new Exception("Inactive team member cannot be deleted");

            // Soft delete
            teamMember.IsActive = false;
            teamMember.UpdatedAt = DateTime.Now;

            await _projectTeamMemberRepository.UpdateAsync(teamMember);
            await _projectTeamMemberRepository.SaveChangesAsync();

            return true;
        }

    }
}
