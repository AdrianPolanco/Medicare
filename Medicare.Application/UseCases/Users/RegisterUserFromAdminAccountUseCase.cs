

using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Users.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Helpers;

namespace Medicare.Application.UseCases.Users
{
    public class RegisterUserFromAdminAccountUseCase : IRegisterUserFromAdminAccountUseCase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IOfficeService _officeService;
        private readonly IPasswordManager _passwordManager;

        public RegisterUserFromAdminAccountUseCase(
            IUserService userService, 
            IRoleService roleService, 
            IOfficeService officeService, 
            IPasswordManager passwordManager)
        {
            _userService = userService;
            _roleService = roleService;
            _officeService = officeService;
            _passwordManager = passwordManager;
        }
        public async Task<bool> ExecuteAsync(User user, CancellationToken cancellationToken)
        {
            //Si el id del rol no es valido, no se puede registrar el usuario
            var role = await _roleService.GetByIdAsync(user.RoleId, cancellationToken);
            if(role is null) return false;

            //Si el id de la oficina no es valido, no se puede registrar el usuario
            var office = await _officeService.GetByIdAsync(user.OfficeId, cancellationToken);
            if(office is null) return false;

            //Si el usuario ya existe, no se puede registrar
            User? userExists = await _userService.UserExists(user.Username, cancellationToken);
            if (userExists is not null) return false;

            //Si el usuario no tiene todos los datos necesarios, no se puede registrar
            //Si el usuario ya tiene una oficina asignada, no se puede registrar
            if(user.Name is null 
                || user.Lastname is null 
                || user.Username is null 
                || user.Email is null 
                || user.Password is null
                || user.OwnedOfficeId is not null
                || user.OwnedOffice is not null) return false;

            //Si el usuario paso todas las validaciones, hasheamos la contraseña
            user.Password = _passwordManager.HashPassword(user.Password);

            //Registramos el usuario
            await _userService.AddAsync(user, cancellationToken);


            //Caso de uso completado exitosamente
            return true;
        }
    }
}
