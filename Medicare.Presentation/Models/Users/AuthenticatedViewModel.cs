using Medicare.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Users
{
    public class AuthenticatedViewModel
    {
        public AuthenticatedViewModel(User user, List<Role> roles)
        {
            CurrentUser = user;
            Roles = roles;
        }

        [Required]
        public User CurrentUser { get; set; }
        [Required]
        public List<Role> Roles { get; set; }
    }
}
