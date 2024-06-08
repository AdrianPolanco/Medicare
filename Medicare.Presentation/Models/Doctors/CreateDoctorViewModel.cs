using Medicare.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Doctors
{
    public class CreateDoctorViewModel
    {
            [Required]
            [MinLength(1, ErrorMessage = "El nombre debe tener al menos 1 caracter")]
            [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'-]*$",
             ErrorMessage = "El apellido solo puede contener letras y caracteres latinos")]
            public string Name { get; set; }

            [Required]
            [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
            [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'-]*$",
             ErrorMessage = "El apellido solo puede contener letras y caracteres latinos")]
            public string Lastname { get; set; }

            [Required]
            [MinLength(3, ErrorMessage = "El apellido debe tener al menos 3 caracteres")]
            [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'-]*$",
             ErrorMessage = "El apellido solo puede contener letras y caracteres latinos")]
            public string Email { get; set; }

            [Required]
            [Phone]
            public string Phone { get; set; }

            [Required]
            [RegularExpression(@"^\d{3}-\d{7}-\d$", 
                ErrorMessage = "El número de identificación debe tener el formato 123-4567890-1.")]
            public string IdentityCard { get; set; }

            [Required]
            public Guid OfficeId { get; set; }

            [DataType(DataType.Upload)]
            public IFormFile Image { get; set; }
    }
}
