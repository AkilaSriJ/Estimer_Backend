using AutoMapper;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
using GenXThofa.Technologies.Estimer.Common.HelperClasses;
using GenXThofa.Technologies.Estimer.Data.Extension;
using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Data.Repositories;
using GenXThofa.Technologies.Estimer.Model.MileStoneStatus;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Service
{
    public class MileStoneStatusService(IMileStoneStatusRepository mileStoneStatusRepository, IMapper mapper):IMileStoneStatusService
    {
        private readonly IMileStoneStatusRepository _mileStoneStatusRepository = mileStoneStatusRepository ?? throw new ArgumentNullException(nameof(mileStoneStatusRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<PagedResult<MileStoneStatusDto>> GetAllAsync(Pagination pagination)
        {
            var status = _mileStoneStatusRepository.GetAll().ApplyPagination(pagination);
            var dtoList = _mapper.Map<List<MileStoneStatusDto>>(status.Data);
            return new PagedResult<MileStoneStatusDto>
            {
                PageNumber = status.PageNumber,
                PageSize = status.PageSize,
                TotalRecords = status.TotalRecords,
                TotalPages = status.TotalPages,
                Data = dtoList
            };
        }

        public async Task<MileStoneStatusDto?> GetByIdAsync(int id)
        {
            var status = await _mileStoneStatusRepository.GetByIdAsync(id);
            if (status == null)
                return null;
            return _mapper.Map<MileStoneStatusDto>(status);
        }

        public async Task<MileStoneStatusDto> CreateAsync(CreateMileStoneStatus dto)
        {
            var existingStatus = await _mileStoneStatusRepository.GetByNameAsync(dto.StatusName);
            if (existingStatus != null)
            {
                throw new Exception("Status already Exists");
            }

            var status = _mapper.Map<MilestoneStatus>(dto);
            status.CreatedAt = DateTime.Now;
            await _mileStoneStatusRepository.CreateAsync(status);
            await _mileStoneStatusRepository.SaveChangesAsync();
            return _mapper.Map<MileStoneStatusDto>(status);
        }

        public async Task<MileStoneStatusDto> UpdateAsync(int id, CreateMileStoneStatus dto)
        {
            var existingStatus = await _mileStoneStatusRepository.GetByIdAsync(id);
            if (existingStatus == null)
                return null;
            _mapper.Map(dto, existingStatus);
            existingStatus.UpdatedAt = DateTime.Now;
            await _mileStoneStatusRepository.UpdateAsync(existingStatus);
            await _mileStoneStatusRepository.SaveChangesAsync();
            return _mapper.Map<MileStoneStatusDto>(existingStatus);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _mileStoneStatusRepository.GetByIdAsync(id);
            if (status == null)
                return false;
            if (status.IsActive)
                throw new Exception("Active Role cannot be deleted");
            await _mileStoneStatusRepository.DeleteAsync(status);
            await _mileStoneStatusRepository.SaveChangesAsync();
            return true;
        }
    }
}
