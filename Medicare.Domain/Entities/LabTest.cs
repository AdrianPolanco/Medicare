using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class LabTest: Entity
    {
        public string Name { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<LabTestResult> LabTestResults { get; set; }
    }
}
