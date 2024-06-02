

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class User: Entity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid IdOffice { get; set; }
        public Guid IdRole { get; set; }

        public virtual Office Office { get; set; }
        public virtual Role Role { get; set; }
    }
}
