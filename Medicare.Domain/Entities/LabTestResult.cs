

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class LabTestResult: Entity
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid LabTestId { get; set; }
        public LabTest LabTest { get; set; }
        public string? Result { get; set; }
        public bool IsCompleted { get; set; }   
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
