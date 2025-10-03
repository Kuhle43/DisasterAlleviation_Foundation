using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation_Foundation.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // --- Properties to map to ApplicationUser ---

        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [Display(Name = "Register as Volunteer")]
        public bool IsVolunteer { get; set; }
    }
}