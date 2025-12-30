using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.MileStoneStatus;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class MileStoneStatusProfile: Profile
    {
        public MileStoneStatusProfile()
        {
            CreateMap<CreateMileStoneStatus, MilestoneStatus>();
            CreateMap<MilestoneStatus, MileStoneStatusDto>();
        }
    }
}
