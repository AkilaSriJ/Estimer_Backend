using GenXThofa.Technologies.Estimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IProjectStatusRepository
    {
        IQueryable<ProjectStatus>   GetAll();
        Task<ProjectStatus> GetByIdAsync(int id);
        Task<ProjectStatus> GetByNameAsync(string name);
        Task<ProjectStatus> CreateAsync(ProjectStatus projectStatus);
        Task UpdateAsync(ProjectStatus projectStatus);
        Task DeleteAsync(ProjectStatus projectStatus);
        Task SaveChangesAsync();
    }
}
