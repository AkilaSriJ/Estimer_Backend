using AutoMapper;
using GenXThofa.Technologies.Estimer.Data.Models;
using GenXThofa.Technologies.Estimer.Model.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.BusinessLogic.Mapper
{
    public class RoleProfile: Profile
    {
        public RoleProfile() 
        {
            CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Permissions,
                opt => opt.MapFrom(src =>
                    JsonSerializer.Deserialize<Dictionary<string, List<string>>>(
                        src.Permissions,
                        (JsonSerializerOptions)null)));

            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.Permissions,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(
                            src.Permissions,
                            (JsonSerializerOptions)null)));

            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.Permissions,
                    opt => opt.MapFrom(src =>
                        JsonSerializer.Serialize(
                            src.Permissions,
                            (JsonSerializerOptions)null)));
        }
    }
}
