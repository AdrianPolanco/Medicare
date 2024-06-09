using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.LabTests
{
    public class CreateLabTestViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El campo Nombre no puede tener más de 100 caracteres")]
        public string Name { get; set; }
        [Required]
        public Guid OfficeId { get; set; }
    }
}
