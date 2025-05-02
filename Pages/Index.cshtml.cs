using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NabooFinalProject.Models; // Make sure this namespace matches your User and DbContext files
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail; // For sending emails
using System.Net; // For network credentials
using Microsoft.Extensions.Configuration; // To read configuration for email settings
using System.Text.Encodings.Web; // For encoding URLs
using System.ComponentModel.DataAnnotations; // For data annotations like Required and EmailAddress

namespace naboo_final_project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UsersDbContext _context; // Inject your database context
        private readonly IConfiguration _configuration; // Inject configuration to read email settings

        //Bind the email input from the form to this property
        [BindProperty]
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? UserEmail { get; set; } // Use a simple string property for the form input

        public IndexModel(ILogger<IndexModel> logger, UsersDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the model state is valid based on data annotations (Required, EmailAddress)
            if (!ModelState.IsValid)
            {
                // If validation fails, return the same page with validation errors
                return Page();
            }

            // 1. Check if a user with this email already exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == UserEmail);

            if (user == null)
            {
                // If user does not exist, create a new one
                user = new User
                {
                    Email = UserEmail,
                    CreatedAt = DateTime.UtcNow,
                    IsEmailConfirmed = false // New users are not confirmed initially
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync(); // Save the user to get the UserId
            }
            else if (user.IsEmailConfirmed)
            {
                // If user exists and email is already confirmed, we can redirect them to landing page
                ModelState.AddModelError(string.Empty, "This email address is already confirmed.");
                return Page();
            }

            // If user exists but email is not confirmed, or if it's a new user, proceed with sending confirmation email

            // 2. Generate Token
            user.EmailConfirmationToken = Guid.NewGuid().ToString(); // Simple token generation
            user.EmailConfirmationTokenExpires = DateTime.UtcNow.AddHours(24); // Token expires in 24 hours (adjust as needed)

            // 3. Update User Record in Database
            _context.Users.Update(user); // Mark the user entity as modified
            await _context.SaveChangesAsync(); // Save changes including the token and expiration

            // 4. Send Validation Email
            try
            {
                await SendEmailConfirmationEmail(user.Email, user.UserId, user.EmailConfirmationToken);
                // Redirect to a confirmation message page after sending the email
                return RedirectToPage("/RegistrationConfirmation", new { email = UserEmail }); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email confirmation email to {Email}", user.Email);
                ModelState.AddModelError(string.Empty, "Error sending confirmation email. Please try again later.");
                return Page();
            }
        }

        private async Task SendEmailConfirmationEmail(string email, int userId, string token)
        {
            // Construct the validation link.
            // We'll create a new Razor Page named "EmailConfirmation" to handle this link.
            var validationLink = Url.PageLink(
                pageName: "/EmailConfirmation", // The Razor Page that will handle the confirmation
                values: new { userId = userId, token = token }, // Pass userId and token as query parameters
                protocol: Request.Scheme // Use the current request's scheme (http or https)
            );

            var smtpClient = new SmtpClient(_configuration["SmtpSettings:Server"])
            {
                Port = int.Parse(_configuration["SmtpSettings:Port"] ?? "587"), // Default to 587 if not in config
                Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
                EnableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"] ?? "true"), // Default to true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]),
                Subject = "Confirm your email address for WT Network",
                Body = $"Please confirm your email address by clicking this link: <a href='{HtmlEncoder.Default.Encode(validationLink)}'>Confirm Email</a>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
