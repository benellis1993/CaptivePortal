using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using naboo_final_project.Models;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace naboo_final_project.Pages
{
    public class EmailConfirmationModel : PageModel
    {
        private readonly UsersDbContext _context;
        private readonly ILogger<EmailConfirmationModel> _logger;

        public string Message { get; set; } // Property to display messages to the user

        public EmailConfirmationModel(UsersDbContext context, ILogger<EmailConfirmationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int userId, string token)
        {
            if (token == null)
            {
                Message = "Invalid confirmation link.";
                _logger.LogWarning("Email confirmation attempt with missing token.");
                return Page();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                Message = "User not found.";
                _logger.LogWarning("Email confirmation attempt for non-existent user ID: {UserId}", userId);
                return Page();
            }

            // Check if the email is already confirmed
            if (user.IsEmailConfirmed)
            {
                Message = "Email address has already been confirmed.";
                 _logger.LogInformation("Email for user ID {UserId} already confirmed.", userId);
                return RedirectToPage("/AdvertLandingPage");
            }

            // Verify the token and expiration
            if (user.EmailConfirmationToken == token && user.EmailConfirmationTokenExpires > DateTime.UtcNow)
            {
                user.IsEmailConfirmed = true;
                user.EmailConfirmationToken = null;
                user.EmailConfirmationTokenExpires = null;

                await _context.SaveChangesAsync();

                // ✅ Set a cookie to remember that the user has confirmed
                HttpContext.Response.Cookies.Append("WTEmailConfirmed", "true", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Message = "Thank you for confirming your email address!";
                _logger.LogInformation("Email confirmed successfully for user ID: {UserId}", userId);

                // ✅ Optional: redirect them immediately
                return RedirectToPage("/AdvertLandingPage");
            }

            else
            {
                // Handle invalid or expired token
                Message = "Invalid or expired confirmation link.";
                 _logger.LogWarning("Email confirmation failed for user ID {UserId}: Invalid or expired token.", userId);
            }

            return Page();
        }
    }
}
