using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.MileStone;
using GenXThofa.Technologies.Estimer.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class MileStoneService(IMileStoneRepository mileStoneRepository, IMapper mapper):IMileStoneService
    {
        private readonly IMileStoneRepository _mileStoneRepository = mileStoneRepository ?? throw new ArgumentNullException(nameof(mileStoneRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<PagedResult<MileStoneDto>> GetAllAsync(Pagination pagination)
        {
            var roles = _mileStoneRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<MileStoneDto>>(roles.Data);
            return new PagedResult<MileStoneDto>
            {
                PageNumber = roles.PageNumber,
                PageSize = roles.PageSize,
                TotalRecords = roles.TotalRecords,
                TotalPages = roles.TotalPages,
                Data = dtoList
            };
        }

        public async Task<MileStoneDto?> GetByIdAsync(int id)
        {
            var mileStone = await _mileStoneRepository.GetByIdAsync(id);
            if (mileStone == null)
                return null;
            return _mapper.Map<MileStoneDto>(mileStone);
        }

        public async Task<MileStoneDto> CreateAsync(CreateMileStone dto)
        {
            var milestone = _mapper.Map<ProjectMilestone>(dto);
            milestone.CreatedAt = DateTime.Now;
            await _mileStoneRepository.CreateAsync(milestone);
            await _mileStoneRepository.SaveChangesAsync();
            var createdMileStone = await _mileStoneRepository.GetByIdAsync(milestone.ProjectMilestoneId);
            return _mapper.Map<MileStoneDto>(createdMileStone);
        }

        public async Task<MileStoneDto> UpdateAsync(int id, UpdateMileStone dto)
        {
            var existingMileStone = await _mileStoneRepository.GetByIdAsync(id);
            if (existingMileStone == null)
                return null;
            _mapper.Map(dto, existingMileStone);
            existingMileStone.UpdatedAt = DateTime.Now;
            await _mileStoneRepository.UpdateAsync(existingMileStone);
            await _mileStoneRepository.SaveChangesAsync();
            return _mapper.Map<MileStoneDto>(existingMileStone);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mileStone = await _mileStoneRepository.GetByIdAsync(id);
            if (mileStone == null)
                return false;
            await _mileStoneRepository.DeleteAsync(mileStone);
            await _mileStoneRepository.SaveChangesAsync();
            return true;
        }
    }
}
