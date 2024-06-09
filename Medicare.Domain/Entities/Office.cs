using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class Office: Entity
    {
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<LabTest> LabTests { get; set; }
        public ICollection<Patient> Patients { get; set; }

    }
}