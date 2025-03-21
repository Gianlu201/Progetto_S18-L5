using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Progetto_S18_L5.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        // navigazione
        public ICollection<ApplicationUserRole>? ApplicationUserRole { get; set; }
    }
}
