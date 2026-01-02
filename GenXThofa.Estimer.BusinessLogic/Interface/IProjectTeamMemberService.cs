using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.ProjectTeamMember;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Interface
{
    public interface IProjectTeamMemberService
    {
        Task<PagedResult<ProjectTeamMemberDto>> GetAllAsync(Pagination pagination);
        Task<ProjectTeamMemberDto> GetByIdAsync(int id);
        Task<List<ProjectTeamMemberDto>> GetTeamMembersByProjectId(int projectId);
        Task<ProjectTeamMemberDto> CreateAsync(CreateProjectTeamMemberDto dto);
        Task<ProjectTeamMemberDto> UpdateAsync(int id, CreateProjectTeamMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
