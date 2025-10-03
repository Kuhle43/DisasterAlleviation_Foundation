using DisasterAlleviation_Foundation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviation_Foundation.Data
{
    // Inherit from IdentityDbContext<ApplicationUser> to include your custom user model
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- Core Application Models ---
        public DbSet<Disaster> Disasters { get; set; }

        public DbSet<GoodsDonation> GoodsDonations { get; set; }

        public DbSet<MonetaryDonation> MonetaryDonations { get; set; }

        // --- Volunteer Management Models ---
        // CRITICAL: This DbSet was missing and is required by the VolunteersController
        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<VolunteerAssignment> VolunteerAssignments { get; set; }

        // Must be called first to initialize Identity tables
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // CRITICAL FIX: This line MUST be the first thing called to resolve the Identity key error.
            base.OnModelCreating(builder);

            // Your custom configurations (if any) go here.
        }
    }
}