using Microsoft.AspNetCore.Identity;
using GenXThofa.Technologies.Estimer.BusinessLogic.Service;
using GenXThofa.Technologies.Estimer.BusinessLogic.Interface;
namespace GenXThofa.Technologies.Estimer.API.DependencyInjection
{
    public static class ServiceDI
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProjectStatusService, ProjectStatusService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectTeamMemberService, ProjectTeamMemberService>();
            services.AddScoped<IMileStoneStatusService,MileStoneStatusService>();
            services.AddScoped<IMileStoneService,MileStoneService>();
            return services;
        }
    }
}
