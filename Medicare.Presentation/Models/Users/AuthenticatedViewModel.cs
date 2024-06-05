using Medicare.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Models.Users
{
    public class AuthenticatedViewModel
    {
        public User CurrentUser { get; set; }

        public List<Role> Roles { get; set; }
    }
}
