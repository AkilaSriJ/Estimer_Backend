using GenXThofa.Technologies.Estimer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Interface
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();
        Task<Project> GetByIdAsync(int id);
        Task<Project> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        Task SaveChangesAsync();
    }
}
