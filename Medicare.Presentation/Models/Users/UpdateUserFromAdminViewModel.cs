using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Users
{
    public class UpdateUserFromAdminViewModel: IValidatableObject
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "El nombre debe tener al menos 1 caracter")]
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s'-]*$", ErrorMessage = "El nombre solo puede contener letras")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
        [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s'-]*$", ErrorMessage = "El apellido solo puede contener letras")]
        public string Lastname { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "El nombre de usuario debe tener al menos 6 caracteres")]
        [MaxLength(35, ErrorMessage = "El nombre de usuario no puede tener más de 35 caracteres")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "El email no es válido")]
        [MaxLength(100, ErrorMessage = "El email no puede tener más de 100 caracteres")]
        public string Email { get; set; }


        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "La contraseña debe tener al menos una minúscula, una mayúscula, dos números y un caracter especial")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "El nombre de la oficina debe tener al menos 5 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre de la oficina no puede tener más de 50 caracteres")]
        public string OfficeName { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword) yield return new ValidationResult("Las contraseñas no coinciden", new[] { nameof(Password), nameof(ConfirmPassword) });
        }

      /*  public bool IsUpdateUserViewModelValid()
        {
            return Validate(new ValidationContext(this))
                .Where(v => v.MemberNames.Contains(nameof(Name)) ||
                            v.MemberNames.Contains(nameof(Lastname)) ||
                            v.MemberNames.Contains(nameof(Username)) ||
                            v.MemberNames.Contains(nameof(Email)) ||
                            v.MemberNames.Contains(nameof(Password)) ||
                            v.MemberNames.Contains(nameof(ConfirmPassword)) ||
                            v.MemberNames.Contains(nameof(RoleId)))
                .Count() == 0;
        }*/
    }
}
