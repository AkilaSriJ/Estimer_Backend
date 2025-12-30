using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.Client;
using GenXThofa.Technologies.Estimer.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.ProjectManager,
                    o => o.MapFrom(s => s.ProjectManager.FullName))
                .ForMember(d => d.ProjectStatus,
                    o => o.MapFrom(s => s.ProjectStatus.StatusName))
                .ForMember(d=>d.CompanyName,
                    o => o.MapFrom(s=>s.Client.CompanyName))
                .ForMember(d => d.StatusColor,
                    o => o.MapFrom(s => s.ProjectStatus.StatusColor));
        }
    }
}
