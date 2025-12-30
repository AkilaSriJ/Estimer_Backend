using GenXThofa.Technologies.Estimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IMileStoneRepository
    {
        IQueryable<ProjectMilestone> GetAll();
        Task<ProjectMilestone> GetByIdAsync(int id);
        Task<ProjectMilestone> CreateAsync(ProjectMilestone projectMileStone);
        Task UpdateAsync(ProjectMilestone projectMileStone);
        Task DeleteAsync(ProjectMilestone projectMileStone);
        Task SaveChangesAsync();
    }
}
