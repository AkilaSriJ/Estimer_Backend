using GenXThofa.Technologies.Estimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IMileStoneStatusRepository
    {
        IQueryable<MilestoneStatus> GetAll();
        Task<MilestoneStatus> GetByIdAsync(int id);
        Task<MilestoneStatus> GetByNameAsync(string name);
        Task<MilestoneStatus> CreateAsync(MilestoneStatus milestoneStatus);
        Task UpdateAsync(MilestoneStatus milestoneStatus);
        Task DeleteAsync(MilestoneStatus milestoneStatus);
        Task SaveChangesAsync();
    }
}
