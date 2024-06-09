

using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Users.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Helpers;

namespace Medicare.Application.UseCases.Users
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IOfficeService _officeService;
        private readonly IPasswordManager _passwordManager;
        private readonly ISessionService _sessionService;

        public UpdateUserUseCase(
            IUserService userService, 
            IRoleService roleService, 
            IOfficeService officeService, 
            IPasswordManager passwordManager,
            ISessionService sessionService)
        {
            _userService = userService;
            _roleService = roleService;
            _officeService = officeService;
            _passwordManager = passwordManager;
            _sessionService = sessionService;
        }
        public async Task<bool> ExecuteAsync(User user, CancellationToken cancellationToken)
        {
            
            //Si el id del rol no es valido, no se puede registrar el usuario
            var role = await _roleService.GetByIdAsync(user.RoleId, cancellationToken);
            if (role is null) return false;

            //Si el id de la oficina no es valido, no se puede registrar el usuario
            var office = await _officeService.GetByIdAsync(user.OfficeId, cancellationToken);
            if (office is null) return false;

            //Si el usuario no tiene todos los datos necesarios, no se puede registrar
            //Si el usuario ya tiene una oficina asignada, no se puede registrar
            if (user.Name is null
                || user.Lastname is null
                || user.Username is null
                || user.Email is null
                || user.Password is null) return false;

            User userCurrentState = await _userService.GetByIdAsync(user.Id, cancellationToken);

            //Verificamos si la contraseña sera cambiada
            bool hasPasswordChanged = !_passwordManager.VerifyPassword(user.Password, userCurrentState.Password);

            //Recuperamos el rol actual del usuario
            Guid currentRoleId = userCurrentState.RoleId;

            //Si el usuario paso todas las validaciones, hasheamos la contraseña
            user.Password = _passwordManager.HashPassword(user.Password);

            //Registramos el usuario
            await _userService.UpdateAsync(user, cancellationToken);

            //Recuperamos el usuario actualizado
            User updatedUser = await _userService.GetByIdAsync(user.Id, cancellationToken);

            //Recuperamos el id del usuario que inicio sesion
            Guid loggedInUserId = _sessionService.GetSession().UserId;

            //Chequeamos si el usuario que inicio sesion es el mismo que se esta actualizando
            bool isCurrentUser = loggedInUserId == updatedUser.Id;

            //Chequeamos si el rol del usuario fue cambiado
            bool hasRoleChanged = !(currentRoleId == updatedUser.RoleId);

            //Si la contraseña, el rol o ambos fueron cambiados, cerramos la sesion para que el usuario tenga que iniciar sesion nuevamente
            if ((hasPasswordChanged || hasRoleChanged) && isCurrentUser) _sessionService.RemoveSession();

            //Caso de uso completado exitosamente
            return true;
        }
    }
}
