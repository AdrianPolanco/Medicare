using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Medicare.Application.Extensions
{
    public static class Application
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IRegisterAdministratorUseCase, RegisterAdministratorUseCase>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IRoleService, RoleService>();
            
            return services;
        }
    }
}
