using DisasterAlleviation_Foundation.Models;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation_Foundation.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Skills/Expertise")]
        public string Skills { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Foreign Key to track the disaster the volunteer is assigned to (optional)
        [Display(Name = "Assigned Disaster")]
        public int? AssignedDisasterId { get; set; }

        // Navigation property
        public Disaster? AssignedDisaster { get; set; }
    }
}