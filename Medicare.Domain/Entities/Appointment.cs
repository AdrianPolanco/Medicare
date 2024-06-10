

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class Appointment: Entity
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<LabTestResult> LabTestResults { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Hour { get; set; }
        public string Reason { get; set; }
        public AppointmentStates State { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
