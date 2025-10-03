using DisasterAlleviation_Foundation.Models;
using DisasterAlleviation_Foundation.Data;
using DisasterAlleviation_Foundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviation_Foundation.Controllers
{
    public class DisastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =================================================================
        // GET: Disasters/Index (View List of Disasters)
        // =================================================================
        public async Task<IActionResult> Index()
        {
            // Retrieve a list of all disasters to display
            // This returns an IEnumerable<Disaster> to the view
            return View(await _context.Disasters.ToListAsync());
        }

        // =================================================================
        // GET: Disasters/Report (Displays the report form)
        // =================================================================
        public IActionResult Report()
        {
            // Returns the view with an empty Disaster model instance
            return View();
        }

        // =================================================================
        // POST: Disasters/Report (Handles form submission and saves the incident)
        // =================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report([Bind("StartDate,EndDate,Location,AidTypeRequired,Description")] Disaster disaster)
        {
            // Ensure StartDate is before EndDate (example custom validation)
            if (disaster.StartDate > disaster.EndDate)
            {
                ModelState.AddModelError("EndDate", "The End Date cannot be before the Start Date.");
            }

            if (ModelState.IsValid)
            {
                // **CRITICAL LOGIC:** Set system-managed fields
                disaster.IsActive = true;
                disaster.DateReported = DateTime.Now;

                _context.Add(disaster);
                await _context.SaveChangesAsync();

                // Provide user feedback and redirect
                TempData["SuccessMessage"] = "Disaster incident reported successfully!";
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return the model back to the view to show errors
            return View(disaster);
        }

        // NOTE: You would typically add Details, Edit, and Delete actions here as well.
    }
}