using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace naboo_final_project.Pages
{
    // This page model handles the RegistrationConfirmation page.
    // It receives the email address as a query parameter to display it to the user.
    public class RegistrationConfirmationModel : PageModel
    {
        // Property to hold the email address passed from the previous page
        public string? Email { get; set; }

        public void OnGet(string email)
        {
            // Get the email from the query parameter named 'email'
            Email = email;
        }
    }
}
