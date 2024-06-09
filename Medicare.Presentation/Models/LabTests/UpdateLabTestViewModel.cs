using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.LabTests
{
    public class UpdateLabTestViewModel: CreateLabTestViewModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
