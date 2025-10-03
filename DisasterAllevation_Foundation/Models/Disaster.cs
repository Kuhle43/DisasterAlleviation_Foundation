using DisasterAlleviation_Foundation.Models; // Removed duplicate/incorrect using
using System;
using System.Collections.Generic; // Added for ICollection and HashSet
using System.ComponentModel.DataAnnotations;

// NOTE: Ensure the namespace spelling matches the project's root namespace exactly!
namespace DisasterAlleviation_Foundation.Models
{
    public class Disaster
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Added for Incident Reporting feature
        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Aid Required")]
        public string AidTypeRequired { get; set; } // e.g., Food, Clothing, Funds

        public string Description { get; set; }

        // Added for Incident Reporting feature
        [Display(Name = "Date Reported")]
        public DateTime DateReported { get; set; }

        // =================================================================
        // NAVIGATION PROPERTIES (CRITICAL for linking to donations/volunteers)
        // Corrected GoodsDonation type to be singular for consistency
        // =================================================================
        public ICollection<GoodsDonation> GoodsDonations { get; set; }
        public ICollection<MonetaryDonation> MonetaryDonations { get; set; }
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }


        // Constructor to initialize the collections and prevent NullReferenceExceptions
        public Disaster()
        {
            GoodsDonations = new HashSet<GoodsDonation>();
            MonetaryDonations = new HashSet<MonetaryDonation>();
            VolunteerAssignments = new HashSet<VolunteerAssignment>();
        }
    }
}