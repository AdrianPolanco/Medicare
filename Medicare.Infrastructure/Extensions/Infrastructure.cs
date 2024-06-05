using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Domain.Repositories.Base;
using Medicare.Domain.Helpers;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Options;
using Medicare.Infrastructure.Repositories;
using Medicare.Infrastructure.Repositories.Base;
using Medicare.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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

        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
