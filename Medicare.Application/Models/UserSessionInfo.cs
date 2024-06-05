

namespace Medicare.Application.Models
{
    public class UserSessionInfo
    {
        public Guid UserId { get; set; }
        public Guid OfficeId { get; set; }
        public Guid RoleId { get; set; }
        public bool OwnsOffice { get; set; }
    }
}
