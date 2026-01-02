using GenXThofa.Technologies.Estimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IProjectTeamMemberRepository
    {
        IQueryable<ProjectTeamMember> GetAll();
        Task<ProjectTeamMember> GetByIdAsync(int id);
        Task<List<ProjectTeamMember>> GetByProjectId(int projectId);
        Task<ProjectTeamMember> CreateAsync(ProjectTeamMember projectTeamMember);
        Task UpdateAsync(ProjectTeamMember projectTeamMember);
        Task DeleteAsync(ProjectTeamMember projectTeamMember);
        Task SaveChangesAsync();
    }
}
