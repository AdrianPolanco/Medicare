using Medicare.Application.Services;
using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.Services.Interfaces.Base;
using Medicare.Application.UseCases.Doctors;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Application.UseCases.Users;
using Medicare.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Medicare.Application.Extensions
{
    public static class Application
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IReadOnlyService<Role>, RoleService>();
            services.AddScoped<IPartialService<Office>, OfficeService>();
            services.AddScoped<IService<User>, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILabTestService, LabTestService>();
            services.AddScoped<IRegisterAdministratorUseCase, RegisterAdministratorUseCase>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IRegisterUserFromAdminAccountUseCase, RegisterUserFromAdminAccountUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IFileService<Doctor>, FileService<Doctor>>();
            services.AddScoped<IRegisterDoctorUseCase, RegisterDoctorUseCase>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IUpdateDoctorUseCase, UpdateDoctorUseCase>();

            return services;
        }
    }
}
