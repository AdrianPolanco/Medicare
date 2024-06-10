using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.Appointments
{
    public class AppointmentsMenuViewModel
    {
        public List<Appointment> Appointments { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }

    }
}
