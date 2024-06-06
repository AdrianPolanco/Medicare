using Medicare.Application.Enums;
using Medicare.Domain.Entities;

namespace Medicare.Presentation.Models.Users
{
    public class UsersMenuViewModel: AuthenticatedViewModel
    {
        public UserFilterOptions All = UserFilterOptions.All;
        public UserFilterOptions Admins = UserFilterOptions.Admins;
        public UserFilterOptions Assistants = UserFilterOptions.Assistants;
        public List<User> Users { get; set; }
        public int Pages { get; set; }
    }
}
