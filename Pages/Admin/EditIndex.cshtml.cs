using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace naboo_final_project.Pages.Admin
{
    public class EditIndexModel : PageModel
    {
        [BindProperty]
        public string WelcomeText { get; set; }

        [BindProperty]
        public string TermsContent { get; set; }

        public void OnGet()
        {
            // Load Welcome Text (mocked for now)
            WelcomeText = "Welcome to the Wi-Fi portal!";

            // Load Terms of Service content
            var termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "TermsOfService.txt");
            if (System.IO.File.Exists(termsFilePath))
            {
                TermsContent = System.IO.File.ReadAllText(termsFilePath);
            }
        }

        public IActionResult OnPost()
        {
            // Save Welcome Text (mocked for now)
            // In a real application, this would save to a database or configuration file
            WelcomeText = WelcomeText;

            // Save Terms of Service content
            var termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "TermsOfService.txt");
            System.IO.File.WriteAllText(termsFilePath, TermsContent);

            return RedirectToPage("/Index");
        }
    }
}