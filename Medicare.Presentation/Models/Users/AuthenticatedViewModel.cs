using Medicare.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Users
{
    public class AuthenticatedViewModel
    {

        [Required]
        public User CurrentUser { get; set; }
        [Required]
        public List<Role> Roles { get; set; }
    }
}
