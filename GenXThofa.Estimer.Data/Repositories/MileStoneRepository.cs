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
    public class MileStoneRepository(AppDbContext dbContext, ILogger<MileStoneRepository> logger):IMileStoneRepository
    {
        private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<MileStoneRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public IQueryable<ProjectMilestone> GetAll()
        {
            return _dbContext.ProjectMilestones.Include(p => p.MilestoneStatus).AsNoTracking();

        }
        public async Task<ProjectMilestone> GetByIdAsync(int id)
        {
            return await _dbContext.ProjectMilestones
                                            .Include(ps => ps.MilestoneStatus)
                                            .FirstOrDefaultAsync(u => u.ProjectMilestoneId == id);
        }
        public async Task<ProjectMilestone> CreateAsync(ProjectMilestone projectMileStone)
        {
            if (projectMileStone == null)
                throw new ArgumentNullException(nameof(projectMileStone));
            var entry = await _dbContext.ProjectMilestones.AddAsync(projectMileStone);
            return entry.Entity;
        }

        public Task UpdateAsync(ProjectMilestone projectMileStone)
        {
            if (projectMileStone == null)
                throw new ArgumentNullException(nameof(projectMileStone));
            _dbContext.ProjectMilestones.Update(projectMileStone);
            return Task.CompletedTask;

        }
        public Task DeleteAsync(ProjectMilestone projectMileStone)
        {
            if (projectMileStone == null)
                throw new ArgumentNullException(nameof(projectMileStone));
            _dbContext.ProjectMilestones.Remove(projectMileStone);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
