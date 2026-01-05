using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.ExternalModels;
using GenXThofa.Technologies.Estimer.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() 
        {
            CreateMap<EmployeeModel, ProjectManagerDto>();

            CreateMap<EmployeeModel, EmployeeDetailDto>()
                .ForMember(dest => dest.Skills,
                    opt => opt.MapFrom(src => src.Skills ?? new List<string>()));

        }    
        
    }
}
