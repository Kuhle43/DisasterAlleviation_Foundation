using DisasterAlleviation_Foundation.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterAlleviation_Foundation.Models
{
    public class GoodsDonation
    {
        public int Id { get; set; } // Primary Key

        [Display(Name = "Donor Name")]
        public string DonorName { get; set; } // Can be "Anonymous"

        [Required]
        [Display(Name = "Donation Date")]
        public DateTime DonationDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string GoodsCategory { get; set; } // e.g., Food, Clothing, Medical

        [Required]
        [Display(Name = "Item Description / Quantity")]
        public string GoodsDescription { get; set; } // e.g., "10 blankets, 5 boxes of canned food"

        // Foreign Key
        public int? DisasterId { get; set; }

        // Navigation Property: Links the donation to the specific disaster
        [ForeignKey("DisasterId")]
        public Disaster AssignedDisaster { get; set; }
    }
}