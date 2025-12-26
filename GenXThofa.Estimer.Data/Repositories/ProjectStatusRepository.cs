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
    public class ProjectStatusRepository(AppDbContext dbContext, ILogger<ProjectStatusRepository> logger) : IProjectStatusRepository
    {
        private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<ProjectStatusRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public IQueryable<ProjectStatus> GetAll()
        {
            return _dbContext.ProjectStatuses.AsNoTracking();
        }

        public async Task<ProjectStatus> GetByIdAsync(int id)
        {
            return await _dbContext.ProjectStatuses.FirstOrDefaultAsync(ps => ps.ProjectStatusId == id);
        }
        public async Task<ProjectStatus> GetByNameAsync(string name)
        {
            return await _dbContext.ProjectStatuses.FirstOrDefaultAsync(ps => ps.StatusName == name);
        }

        public async Task<ProjectStatus> CreateAsync(ProjectStatus projectStatus)
        {
            if (projectStatus == null)
                throw new ArgumentNullException(nameof(projectStatus));
            var entry = await _dbContext.ProjectStatuses.AddAsync(projectStatus);
            return entry.Entity;
        }

        public Task UpdateAsync(ProjectStatus projectStatus)
        {
            if (projectStatus == null)
                throw new ArgumentNullException(nameof(projectStatus));
            _dbContext.ProjectStatuses.Update(projectStatus);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(ProjectStatus projectStatus)
        {
            if (projectStatus == null)
                throw new ArgumentNullException(nameof(projectStatus));
            _dbContext.ProjectStatuses.Remove(projectStatus);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
             await _dbContext.SaveChangesAsync();
        }
    }
}
