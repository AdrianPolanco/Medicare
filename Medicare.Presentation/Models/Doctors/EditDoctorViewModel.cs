using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Doctors
{
    public class EditDoctorViewModel: CreateDoctorViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public string ImageRoute { get; set; }
    }
}
