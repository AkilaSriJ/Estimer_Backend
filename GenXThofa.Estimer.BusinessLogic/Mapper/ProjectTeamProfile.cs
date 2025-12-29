using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.ProjectTeamMember;
using GenXThofa.Technologies.Estimer.Model.StatusProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class ProjectTeamProfile: Profile
    {
        public ProjectTeamProfile() 
        {
            CreateMap<CreateProjectTeamMemberDto, ProjectTeamMember>();
            CreateMap<ProjectTeamMember, ProjectTeamMemberDto>();
        }
    }
}
