using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.LabTestsResults
{
    public class LabTestResultViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MinLength(15, ErrorMessage = "El resultado debe ser expresivo: No se permiten resultados con menos de 15 caracteres.")]
        public string Result { get; set; }
    }
}
