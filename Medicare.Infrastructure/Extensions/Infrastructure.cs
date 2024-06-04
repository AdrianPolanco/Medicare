using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Domain.Repositories.Base;
using Medicare.Domain.Services;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Options;
using Medicare.Infrastructure.Repositories;
using Medicare.Infrastructure.Repositories.Base;
using Medicare.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medicare.Infrastructure.Extensions
{
    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, InfrastuctureOptions infrastructureOptions)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(infrastructureOptions.Database);
            });
            services.AddScoped<IReadOnlyRepository<Role>, RoleRepository>();
            services.AddScoped<IPartialRepository<Office>, OfficeRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddSingleton<IPasswordManager, PasswordManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            
            return services;
        }
    }
}
