using DisasterAlleviation_Foundation.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviation_Foundation.Models
{
    public class VolunteerAssignment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Volunteer")]
        // Key property name is fine, but we link it to ApplicationUser
        public string VolunteerId { get; set; }

        [Required]
        public int DisasterId { get; set; }

        [Display(Name = "Assignment Date")]
        public DateTime AssignmentDate { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public string Status { get; set; } = "Assigned"; // e.g., Assigned, In Progress, Completed

        // Navigation Properties

        // CRITICAL FIX: Changed from IdentityUser to ApplicationUser
        [ForeignKey("VolunteerId")]
        public ApplicationUser Volunteer { get; set; }

        [ForeignKey("DisasterId")]
        public Disaster AssignedDisaster { get; set; }
    }
}