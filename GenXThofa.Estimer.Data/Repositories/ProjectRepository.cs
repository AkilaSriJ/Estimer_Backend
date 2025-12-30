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
    public class ProjectRepository(AppDbContext dbContext, ILogger<ProjectRepository> logger):IProjectRepository
    {
        private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<ProjectRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public IQueryable<Project> GetAll()
        {
            return _dbContext.Projects.Include(p=>p.ProjectManager).Include(p=>p.ProjectStatus).Include(p=>p.Client).AsNoTracking();

        }
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects.Include(p=>p.Client).Include(p=>p.ProjectManager)
                                            .Include(ps=>ps.ProjectStatus)
                                            .FirstOrDefaultAsync(u => u.ProjectId == id);
        }

        public async Task<Project> CreateAsync(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            var entry = await _dbContext.Projects.AddAsync(project);
            return entry.Entity;
        }

        public Task UpdateAsync(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            _dbContext.Projects.Update(project);
            return Task.CompletedTask;

        }
        public Task DeleteAsync(Project project)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));
            _dbContext.Projects.Remove(project);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
