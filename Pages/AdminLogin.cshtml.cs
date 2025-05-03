using naboo_final_project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.IO;

namespace naboo_final_project.Pages;

public class AdminLoginModel : PageModel
{
    [BindProperty]
    public string? Email { get; set; }

    [BindProperty]
    public string? Password { get; set; }

    public string? ErrorMessage { get; set; }
    public string ImagePath { get; set; } = string.Empty;  // Initialize to avoid null warnings

    private readonly string _imagePathFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagePath.txt");

    private readonly UsersDbContext _dbContext;

    public AdminLoginModel(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        // Load ImagePath from file
            ImagePath = System.IO.File.Exists(_imagePathFilePath)
                ? System.IO.File.ReadAllText(_imagePathFilePath)
                : "/images/default.jpg";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var adminUser = _dbContext.AdminUsers.FirstOrDefault(u => u.Email == Email);

        if (adminUser == null || adminUser.PasswordHash != Password) // Replace with hashed password comparison
        {
            ErrorMessage = "Invalid email or password.";
            return Page();
        }

        // Create claims for the admin user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, adminUser.Email),
            new Claim(ClaimTypes.Role, adminUser.Role)
        };

        var identity = new ClaimsIdentity(claims, "CookieAuth");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("CookieAuth", principal);

        // Redirect to the AdminDashboard page after successful login
        return RedirectToPage("/Admin/AdminDashboard");
    }
}