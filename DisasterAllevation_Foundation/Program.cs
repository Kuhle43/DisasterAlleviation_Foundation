using DisasterAlleviation_Foundation.Data;
using DisasterAlleviation_Foundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// ====================================================================
// SECTION 1: CONFIGURE SERVICES (builder.Services)
// ====================================================================

// 1. Database Context Registration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Add ASP.NET Core Identity Services
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Set basic password policy
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

// 4. Add support for Razor Pages (required for Identity UI)
builder.Services.AddRazorPages();

// 5. CRITICAL FIX: Add authorization services to fix the exception
builder.Services.AddAuthorization();

var app = builder.Build();

// ====================================================================
// SECTION 2: CONFIGURE THE HTTP REQUEST PIPELINE (app.Use...)
// ====================================================================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication and Authorization middleware (these require the services above)
app.UseAuthentication();
app.UseAuthorization(); // This line now works

// CRITICAL: Map Razor Pages endpoints (required for /Identity/ routes)
app.MapRazorPages();

// Configure the default route for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();