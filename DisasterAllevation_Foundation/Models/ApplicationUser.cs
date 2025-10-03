using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation_Foundation.Models
{
    // Inherits from IdentityUser to get standard fields (Email, PasswordHash, etc.)
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; } // <-- NEW

        [StringLength(255)]
        public string? Address { get; set; } // <-- NEW

        [Display(Name = "Register as Volunteer")]
        public bool IsVolunteer { get; set; } = false; // <-- NEW
    }
}