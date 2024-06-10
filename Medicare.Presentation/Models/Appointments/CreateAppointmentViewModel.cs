using Medicare.Domain.Entities;
using Medicare.Presentation.Filters.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Medicare.Presentation.Models.Appointments
{
    public class CreateAppointmentViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SelectedPatientId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public Guid SelectedDoctorId { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public TimeOnly Time { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(10, ErrorMessage = "La razón debe tener al menos 10 caracteres")]
        public string Reason { get; set; }
    }
}
