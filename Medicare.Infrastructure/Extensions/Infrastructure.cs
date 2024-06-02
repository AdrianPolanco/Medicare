using Medicare.Domain.Services;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Options;
using Medicare.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Medicare.Infrastructure.Extensions
{
    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastuctureOptions infrastructureOptions)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(infrastructureOptions.Database);
            });
            services.AddSingleton<IPasswordManager, PasswordManager>();
            return services;
        }
    }
}
