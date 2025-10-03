// Controllers/AccountController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DisasterAlleviation_Foundation.Models;
using DisasterAlleviation_Foundation.ViewModels;

namespace DisasterAlleviation_Foundation.Controllers
{
    public class AccountController : Controller
    {
        // Dependency Injection for Identity services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ------------------------------------------------------------------------------------------------
        // Registration Actions
        // ------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Requires Views/Account/Register.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Address = model.Address, // Assume Address is now in the view model
                    IsVolunteer = model.IsVolunteer
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Sign the user in immediately after successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Display errors if registration failed (e.g., password too weak)
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // ------------------------------------------------------------------------------------------------
        // Login Actions
        // ------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); // Requires Views/Account/Login.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt. Check email and password.");
            }
            return View(model);
        }

        // ------------------------------------------------------------------------------------------------
        // Logout Action
        // ------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}