using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.Doctors
{
    public class DoctorsMenuViewModel
    {
        public List<Doctor> Doctors { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
