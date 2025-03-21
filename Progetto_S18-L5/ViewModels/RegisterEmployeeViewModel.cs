using System.ComponentModel.DataAnnotations;

namespace Progetto_S18_L5.ViewModels
{
    public class RegisterEmployeeViewModel
    {
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

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirm password must be the same!")]
        public required string ConfirmPassword { get; set; }

        [Required]
        [StringLength(36, ErrorMessage = "Invalid role")]
        [Display(Name = "Role")]
        public required string RoleId { get; set; }
    }
}
