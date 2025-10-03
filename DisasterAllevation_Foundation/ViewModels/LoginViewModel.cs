using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation_Foundation.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        // --- FIX for 'ReturnUrl' Error ---
        public string? ReturnUrl { get; set; }
    }
}