using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.MileStoneStatus;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Interface
{
    public interface IMileStoneStatusService
    {
        Task<PagedResult<MileStoneStatusDto>> GetAllAsync(Pagination pagination);
        Task<MileStoneStatusDto?> GetByIdAsync(int id);
        Task<MileStoneStatusDto> CreateAsync(CreateMileStoneStatus dto);
        Task<MileStoneStatusDto> UpdateAsync(int id, CreateMileStoneStatus dto);
        Task<bool> DeleteAsync(int id);
    }
}
