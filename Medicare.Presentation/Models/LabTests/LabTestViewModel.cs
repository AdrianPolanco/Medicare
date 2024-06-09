using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.LabTests
{
    public class LabTestViewModel
    {
        public List<LabTest> LabTests { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
