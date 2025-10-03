using DisasterAlleviation_Foundation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Optional, but good practice

namespace DisasterAlleviation_Foundation.Models
{
    public class Donation
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [Display(Name = "Donation Date")]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; } // Can be "Anonymous"

        [Required]
        [StringLength(50)]
        [Display(Name = "Donation Type")]
        public string Type { get; set; } // "Money" or "Goods"

        [Display(Name = "Amount (R)")]
        // Only required if Type is Money, decimal? makes it nullable
        public decimal? Amount { get; set; }

        // Only required if Type is Goods
        [StringLength(255)]
        [Display(Name = "Goods Description")]
        public string? GoodsDescription { get; set; } // Make nullable

        // Foreign Key relationship to Disaster (int? makes it nullable)
        [Display(Name = "Target Disaster")]
        public int? DisasterId { get; set; }

        // Navigation property to the Disaster entity (must be type Disaster?)
        public Disaster? Disaster { get; set; }

        // --- THE PROBLEM PROPERTIES HAVE BEEN REMOVED ---
        // public object AssignedDisaster { get; internal set; }
        // public object FullName { get; internal set; }
    }
}