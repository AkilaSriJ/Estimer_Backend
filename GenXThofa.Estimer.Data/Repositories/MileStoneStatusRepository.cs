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
    public class MileStoneStatusRepository(AppDbContext dbContext, ILogger<MileStoneStatusRepository> logger):IMileStoneStatusRepository
    {
        private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private readonly ILogger<MileStoneStatusRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public IQueryable<MilestoneStatus> GetAll()
        {
            return _dbContext.MilestoneStatuses.AsNoTracking();
        }

        public async Task<MilestoneStatus> GetByIdAsync(int id)
        {
            return await _dbContext.MilestoneStatuses.FirstOrDefaultAsync(ps => ps.MilestoneStatusId == id);
        }
        public async Task<MilestoneStatus> GetByNameAsync(string name)
        {
            return await _dbContext.MilestoneStatuses.FirstOrDefaultAsync(ps => ps.StatusName == name);
        }

        public async Task<MilestoneStatus> CreateAsync(MilestoneStatus milestoneStatus)
        {
            if (milestoneStatus == null)
                throw new ArgumentNullException(nameof(milestoneStatus));
            var entry = await _dbContext.MilestoneStatuses.AddAsync(milestoneStatus);
            return entry.Entity;
        }

        public Task UpdateAsync(MilestoneStatus milestoneStatus)
        {
            if (milestoneStatus == null)
                throw new ArgumentNullException(nameof(milestoneStatus));
            _dbContext.MilestoneStatuses.Update(milestoneStatus);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MilestoneStatus milestoneStatus)
        {
            if (milestoneStatus == null)
                throw new ArgumentNullException(nameof(milestoneStatus));
            _dbContext.MilestoneStatuses.Remove(milestoneStatus);
            return Task.CompletedTask;
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
