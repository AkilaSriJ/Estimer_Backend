using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Interface
{
    public interface IProjectStatusService
    {
        Task<PagedResult<ProjectStatusDto>> GetAllAsync(Pagination pagination);
        Task<ProjectStatusDto?> GetByIdAsync(int id);
        Task<ProjectStatusDto> CreateAsync(CreateProjectStatusDto dto);
        Task<ProjectStatusDto> UpdateAsync(int id,CreateProjectStatusDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
