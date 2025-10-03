using DisasterAlleviation_Foundation.Data;
using DisasterAlleviation_Foundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviation_Foundation.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to load active disasters for dropdowns
        private async Task PopulateDisastersDropdown()
        {
            // Load only active disasters for donation targeting
            var activeDisasters = await _context.Disasters
                .Where(d => d.IsActive == true)
                .OrderByDescending(d => d.DateReported)
                .ToListAsync();

            // The SelectList uses the Disaster model's Id (value) and Location (text)
            ViewData["DisasterId"] = new SelectList(activeDisasters, "Id", "Location");
        }

        // =========================================================================
        // 1. INDEX/MANAGEMENT ACTIONS (Display/Track Donations)
        // =========================================================================

        // GET: Donations/Index (List ALL donations for management/tracking)
        public async Task<IActionResult> Index()
        {
            // IMPORTANT: The DbSets are named GoodsDonations and MonetaryDonations in the DbContext
            // The Include method must reference the Navigation Property name (AssignedDisaster), not the ID property.
            var viewModel = new
            {
                GoodsDonations = await _context.GoodsDonations // Corrected DbSet name
                    .Include(d => d.AssignedDisaster)         // Corrected Include reference
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync(),
                MonetaryDonations = await _context.MonetaryDonations // Corrected DbSet name
                    .Include(d => d.AssignedDisaster)            // Corrected Include reference
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync()
            };

            // You must create an Index View that can handle this anonymous type structure
            return View(viewModel);
        }

        // =========================================================================
        // 2. CREATION ACTIONS (Resource Donation Feature)
        // =========================================================================

        // GET: Donations/Create (Landing page to choose type)
        public IActionResult Create()
        {
            return View();
        }

        // GET: Donations/CreateGoods
        public async Task<IActionResult> CreateGoods()
        {
            await PopulateDisastersDropdown();
            return View(); // Expects /Views/Donations/CreateGoods.cshtml
        }

        // POST: Donations/CreateGoods
        [HttpPost]
        [ValidateAntiForgeryToken]
        // CRITICAL FIX: The model parameter MUST use the singular type: GoodsDonation
        public async Task<IActionResult> CreateGoods([Bind("DonorName,GoodsCategory,GoodsDescription,DisasterId")] GoodsDonation goodsDonation)
        {
            // Remove the system-managed date field from validation
            ModelState.Remove(nameof(goodsDonation.DonationDate));

            if (ModelState.IsValid)
            {
                goodsDonation.DonationDate = DateTime.Now;
                _context.GoodsDonations.Add(goodsDonation); // Use the DbSet name
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thank you! Your goods donation has been recorded.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateDisastersDropdown();
            return View(goodsDonation);
        }

        // GET: Donations/CreateMonetary
        public async Task<IActionResult> CreateMonetary()
        {
            await PopulateDisastersDropdown();
            return View(); // Expects /Views/Donations/CreateMonetary.cshtml
        }

        // POST: Donations/CreateMonetary
        [HttpPost]
        [ValidateAntiForgeryToken]
        // CORRECT: Model parameter already uses the singular type: MonetaryDonation
        public async Task<IActionResult> CreateMonetary([Bind("DonorName,Amount,DisasterId")] MonetaryDonation monetaryDonation)
        {
            // Remove the system-managed date field from validation
            ModelState.Remove(nameof(monetaryDonation.DonationDate));

            if (ModelState.IsValid)
            {
                // Note: The [Range] attribute on the model should handle the Amount > 0 check,
                // but explicit check is fine too.

                monetaryDonation.DonationDate = DateTime.Now;
                _context.MonetaryDonations.Add(monetaryDonation); // Use the DbSet name
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thank you! Your monetary donation has been recorded.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateDisastersDropdown();
            return View(monetaryDonation);
        }
    }
}