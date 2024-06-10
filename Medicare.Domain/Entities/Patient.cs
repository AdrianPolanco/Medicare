

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class Patient: Entity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }     
        public DateTime BirthDate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergy { get; set; }
        public string? Photo { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<LabTestResult> LabTestResults { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
