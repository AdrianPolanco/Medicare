using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.Appointments
{
    public class AppointmentDetailsViewModel
    {
        public Appointment Appointment { get; set; }
        public bool IsValid { get; set; }
    }
}
