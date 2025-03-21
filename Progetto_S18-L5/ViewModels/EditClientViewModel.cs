using System.ComponentModel.DataAnnotations;

namespace Progetto_S18_L5.ViewModels
{
    public class EditClientViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(
            50,
            ErrorMessage = "First name must be from 2 to 50 chars!",
            MinimumLength = 2
        )]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(
            50,
            ErrorMessage = "Last name must be from 2 to 50 chars!",
            MinimumLength = 2
        )]
        public required string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "Phone number not valid!")]
        public required string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email not valid!")]
        public required string Email { get; set; }
    }
}
