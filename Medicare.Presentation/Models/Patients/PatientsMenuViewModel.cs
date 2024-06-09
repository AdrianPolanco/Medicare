using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.Patients
{
    public class PatientsMenuViewModel
    {
        public List<Patient> Patients { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
