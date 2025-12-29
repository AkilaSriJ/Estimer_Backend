using GenXThofa.Technologies.Estimer.Data.Interface;
using GenXThofa.Technologies.Estimer.Data.Repositories;

namespace GenXThofa.Technologies.Estimer.API.DependencyInjection
{
    public static class DataDI
    {
        public static IServiceCollection AddDataDI(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTeamMemberRepository, ProjectTeamMemberRepository>();
            return services;
        }
    }
}
