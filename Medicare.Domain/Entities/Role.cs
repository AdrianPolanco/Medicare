using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Entities
{
    public class Role: Entity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}