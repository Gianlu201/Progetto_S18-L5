using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Progetto_S18_L5.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public new required string UserId { get; set; }
        public new required string RoleId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
}
