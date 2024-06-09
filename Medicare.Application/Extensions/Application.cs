using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.Services.Interfaces.Base;
using Medicare.Application.UseCases.Doctors;
using Medicare.Application.UseCases.Doctors.Interfaces;
using Medicare.Application.UseCases.Patients;
using Medicare.Application.UseCases.Patients.Interfaces;
using Medicare.Application.UseCases.Users;
using Medicare.Application.UseCases.Users.Interfaces;
using Medicare.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Medicare.Application.Extensions
{
    public static class Application
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            //Servicios de entidades
            services.AddScoped<IReadOnlyService<Role>, RoleService>();
            services.AddScoped<IPartialService<Office>, OfficeService>();
            services.AddScoped<IService<User>, UserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILabTestService, LabTestService>();
            services.AddScoped<IPatientService, PatientService>();

           //Servicios de autenticacion e subida de imagenes
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFileService<Doctor>, FileService<Doctor>>();
            services.AddScoped<IFileService<Patient>, FileService<Patient>>();

            //Casos de uso de usuarios
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<IRegisterUserFromAdminAccountUseCase, RegisterUserFromAdminAccountUseCase>(); 
            services.AddScoped<IRegisterAdministratorUseCase, RegisterAdministratorUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            
            //Casos de uso de Doctores
            services.AddScoped<IRegisterDoctorUseCase, RegisterDoctorUseCase>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUpdateDoctorUseCase, UpdateDoctorUseCase>();
            services.AddScoped<IDeleteDoctorUseCase, DeleteDoctorUseCase>();

            //Casos de uso de pacientes
            services.AddScoped<ICreatePatientUseCase, CreatePatientUseCase>();

            return services;
        }
    }
}
