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
            CreateMap<CreateProjectDto, Project>()
                    .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                    .ForMember(dest => dest.Client, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectStatus, opt => opt.Ignore());

            
            CreateMap<UpdateProjectDto, Project>()
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentTerms, opt => opt.Ignore())
                .ForMember(dest => dest.FinalBillingAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectStatus, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.CompanyName,
                    o => o.MapFrom(s => s.Client != null ? s.Client.CompanyName : null))
                .ForMember(d => d.ProjectStatus,
                    o => o.MapFrom(s => s.ProjectStatus != null ? s.ProjectStatus.StatusName : null))
                .ForMember(d => d.StatusColor,
                    o => o.MapFrom(s => s.ProjectStatus != null ? s.ProjectStatus.StatusColor : null))
                // ProjectManager details will be populated from JSON server in service layer

                .ForMember(d => d.ProjectManager, opt => opt.Ignore());
        }
    }
}
