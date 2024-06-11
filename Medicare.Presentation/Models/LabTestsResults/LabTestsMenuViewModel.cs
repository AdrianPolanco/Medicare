using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.LabTestsResults
{
    public class LabTestsMenuViewModel
    {
        public Guid AppointmentId { get; set; }
        public List<LabTestResult> LabTestResults { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
