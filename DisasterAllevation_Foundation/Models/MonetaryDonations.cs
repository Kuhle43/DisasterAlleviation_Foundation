using DisasterAlleviation_Foundation.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// NOTE: Ensure the spelling of the namespace matches your project's root namespace exactly.
namespace DisasterAlleviation_Foundation.Models
{
    public class MonetaryDonation
    {
        public int Id { get; set; } // Primary Key

        [Display(Name = "Donor Name")]
        public string DonorName { get; set; } // Can be "Anonymous"

        [Required]
        [Display(Name = "Amount (ZAR)")]
        [DataType(DataType.Currency)]
        // Ensures precise mapping to SQL Decimal type
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Donation Date")]
        public DateTime DonationDate { get; set; } = DateTime.Now;

        // =======================================================
        // NAVIGATION PROPERTIES & FOREIGN KEYS
        // =======================================================

        // Foreign Key to Disaster model (nullable, as donation can be general)
        public int? DisasterId { get; set; }

        // Navigation Property: Links the donation to the specific disaster
        [ForeignKey("DisasterId")]
        // This requires 'using DisasterAlleviation_Foundation.Models;' to be at the top
        public Disaster AssignedDisaster { get; set; }
    }
}