using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Model.MileStone;
using GenXThofa.Technologies.Estimer.Model.Project;
using GenXThofa.Technologies.Estimer.Model.ProjectTeamMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Interface
{
    public interface IMileStoneService
    {
        Task<PagedResult<MileStoneDto>> GetAllAsync(Pagination pagination);
        Task<MileStoneDto?> GetByIdAsync(int id);
        Task<List<MileStoneDto>> GetMileStonesByProjectId(int projectId);
        Task<MileStoneDto> CreateAsync(CreateMileStone dto);
        Task<MileStoneDto?> UpdateAsync(int id, UpdateMileStone dto);
        Task<bool> DeleteAsync(int id);
    }
}
