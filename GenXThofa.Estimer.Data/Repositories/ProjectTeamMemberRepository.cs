using GenXThofa.Technologies.Estimer.Data.Context;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Repositories
{
    public class ProjectTeamMemberRepository(AppDbContext dbContext, ILogger<ProjectTeamMemberRepository> logger):IProjectTeamMemberRepository
    {
        private readonly AppDbContext _dbContext=dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<ProjectTeamMemberRepository> _logger= logger ?? throw new ArgumentNullException(nameof(dbContext));

        public IQueryable<ProjectTeamMember> GetAll()
        {
            return _dbContext.ProjectTeamMembers.AsNoTracking();
        }

        public async Task<ProjectTeamMember> GetByIdAsync(int id)
        {
            return await _dbContext.ProjectTeamMembers.Include(x=>x.Project).FirstOrDefaultAsync(ps => ps.ProjectTeamMemberId == id);
        }
        public async Task<List<ProjectTeamMember>> GetByProjectId(int projectId)
        {
            return await _dbContext.ProjectTeamMembers
                           .Where(x => x.ProjectId == projectId)
                           .ToListAsync();
        }
        public async Task<ProjectTeamMember> CreateAsync(ProjectTeamMember projectTeamMember)
        {
            if (projectTeamMember == null)
                throw new ArgumentNullException(nameof(projectTeamMember));
            var entry = await _dbContext.ProjectTeamMembers.AddAsync(projectTeamMember);
            return entry.Entity;
        }

        public Task UpdateAsync(ProjectTeamMember projectTeamMember)
        {
            if (projectTeamMember == null)
                throw new ArgumentNullException(nameof(projectTeamMember));
            _dbContext.ProjectTeamMembers.Update(projectTeamMember);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(ProjectTeamMember projectTeamMember)
        {
            if (projectTeamMember == null)
                throw new ArgumentNullException(nameof(projectTeamMember));
            _dbContext.ProjectTeamMembers.Remove(projectTeamMember);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
