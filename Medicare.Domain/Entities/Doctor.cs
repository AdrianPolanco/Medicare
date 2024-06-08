

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class Doctor: Entity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }

    }
}
