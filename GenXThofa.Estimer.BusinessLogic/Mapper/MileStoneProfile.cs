using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.MileStone;
using GenXThofa.Technologies.Estimer.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class MileStoneProfile: Profile
    {
        public MileStoneProfile() 
        {
            CreateMap<CreateMileStone, ProjectMilestone>();
            CreateMap<UpdateMileStone, ProjectMilestone>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProjectMilestone, MileStoneDto>()
                .ForMember(d => d.MileStoneStatusName,
                    o => o.MapFrom(s => s.MilestoneStatus.StatusName))
                .ForMember(d => d.StatusColor,
                    o => o.MapFrom(s => s.MilestoneStatus.StatusColor));
        }
    }
}
