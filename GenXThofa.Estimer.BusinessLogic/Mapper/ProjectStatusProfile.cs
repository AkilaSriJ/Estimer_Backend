using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class ProjectStatusProfile : Profile
    {
        public ProjectStatusProfile() 
        {
            CreateMap<CreateProjectStatusDto, ProjectStatus>();  
            CreateMap<ProjectStatus, ProjectStatusDto>().ForMember(dest => dest.ProjectStatus,opt => opt.MapFrom(src => src.ProjectStatus != null ? src.ProjectStatus.StatusName : null)); ;
        
        
        }
    }
}
