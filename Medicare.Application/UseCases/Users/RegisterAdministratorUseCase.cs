using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Services;

namespace Medicare.Application.UseCases.Users
{
    public class RegisterAdministratorUseCase : IRegisterAdministratorUseCase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IOfficeService _officeService;
        private readonly IPasswordManager _passwordManager;

        public RegisterAdministratorUseCase(IUserService userService, IRoleService roleService, IOfficeService officeService, IPasswordManager passwordManager)
        {
            _userService = userService;
            _roleService = roleService;
            _officeService = officeService;
            _passwordManager = passwordManager;
        }
        public async Task<bool> ExecuteAsync(User user, string officeName, CancellationToken cancellationToken)
        {
            //Validando si el usuario ya existe
            User userExists = await _userService.UserExists(user.Username, cancellationToken);
            if(userExists != null) return false;

            //Hasheando la contraseña
            string password = _passwordManager.HashPassword(user.Password);

            //Obteniendo el rol de administrador
            Role role = await _roleService.GetByNameAsync("Administrador", cancellationToken);

            //Asignando el rol al usuario
            user.RoleId = role.Id;
            user.Password = password;

            //Creando la oficina
            Office office = new Office
            {
                Name = officeName
            };

            //Registrando la oficina
            await _officeService.AddAsync(office, cancellationToken);

			//Asignando la oficina al usuario
			user.OfficeId = office.Id;

			//Asignando la oficina como oficina propiedad del usuario
			user.OwnedOfficeId = office.Id;

			//Registrando el usuario
			await _userService.AddAsync(user, cancellationToken);

			Office ownedOffice = new Office
            {
                Id = office.Id,
				OwnerId = user.Id
			};

			//Actualizando la oficina para asignarle el propietario
			await _officeService.UpdateAsync(ownedOffice, cancellationToken);
  
            //Caso de uso ejecutado exitosamente
            return true;

        }
    }
}
