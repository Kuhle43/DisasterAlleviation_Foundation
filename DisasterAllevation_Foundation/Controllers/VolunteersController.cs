using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; // Added for SelectList
using DisasterAlleviation_Foundation.Data;
using DisasterAlleviation_Foundation.Models;

namespace DisasterAlleviation_Foundation.Controllers
{
    // The VolunteersController class itself is correct.
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =================================================================
        // CORE FEATURE: Volunteer Registration - GET (Returns the form)
        // =================================================================
        // GET: Volunteers/Register
        public IActionResult Register()
        {
            // Populate the dropdown list for Disasters (Used by the view's select tag)
            ViewBag.AssignedDisasterId = new SelectList(_context.Disasters, "Id", "Description");

            // Returns the view at Views/Volunteers/Register.cshtml
            return View();
        }

        // =================================================================
        // CORE FEATURE: Volunteer Registration - POST (Handles form submission)
        // =================================================================
        // POST: Volunteers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                // **CRITICAL FIX: Assuming DbSet is named 'Volunteers'**
                // Saving the specific Volunteer profile data (FullName, Skills, etc.)
                _context.Volunteers.Add(volunteer); // Changed from _context.Volunteer to _context.Volunteers

                // Update ApplicationUser status if they are logged in (Optional but recommended)
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var appUser = await _context.Users.FindAsync(userId);

                    if (appUser != null)
                    {
                        appUser.IsVolunteer = true;
                        // You can also link the Volunteer record's Id to the ApplicationUser here if you have a foreign key set up.
                        _context.Users.Update(appUser);
                    }
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thank you for registering as a volunteer! You can now browse tasks.";
                return RedirectToAction(nameof(BrowseTasks));
            }

            // If validation fails, repopulate ViewBag and return the view
            ViewBag.AssignedDisasterId = new SelectList(_context.Disasters, "Id", "Description", volunteer.AssignedDisasterId);
            return View(volunteer);
        }

        // =================================================================
        // CORE FEATURE: Browse Available Tasks (for all users)
        // =================================================================
        // GET: Volunteers/BrowseTasks 
        public async Task<IActionResult> BrowseTasks()
        {
            // Show disasters that are currently active (EndDate hasn't passed)
            var activeDisasters = await _context.Disasters
                .Include(d => d.GoodsDonations)
                .Include(d => d.MonetaryDonations)
                .Where(d => d.EndDate.Date >= DateTime.Today || d.IsActive == true)
                .ToListAsync();

            return View(activeDisasters);
        }

        // =================================================================
        // CORE FEATURE: Volunteer for a Task (Assignment Logic)
        // =================================================================
        // POST: Volunteers/VolunteerForThis/{id} 
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VolunteerForThis(int id)
        {
            var volunteerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool alreadyAssigned = await _context.VolunteerAssignments
                .AnyAsync(a => a.DisasterId == id && a.VolunteerId == volunteerId);

            if (alreadyAssigned)
            {
                TempData["ErrorMessage"] = "You have already volunteered for this disaster.";
                return RedirectToAction(nameof(BrowseTasks));
            }

            var assignment = new VolunteerAssignment
            {
                DisasterId = id,
                VolunteerId = volunteerId,
                AssignmentDate = DateTime.Now,
                Status = "Assigned"
            };

            _context.VolunteerAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you! You have been assigned to the task.";
            return RedirectToAction(nameof(MyTasks));
        }

        // =================================================================
        // CORE FEATURE: Track Contributions (for logged-in volunteer)
        // =================================================================
        // GET: Volunteers/MyTasks 
        [Authorize]
        public async Task<IActionResult> MyTasks()
        {
            var volunteerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var assignments = await _context.VolunteerAssignments
                .Include(a => a.AssignedDisaster)
                .Where(a => a.VolunteerId == volunteerId)
                .OrderByDescending(a => a.AssignmentDate)
                .ToListAsync();

            return View(assignments);
        }
    }
}