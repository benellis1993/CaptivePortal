using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace NabooFinalProject.Pages
{
    public class TermsOfServiceModel : PageModel
    {
        public string TermsContent { get; set; }

        public void OnGet()
        {
            var termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Pages", "TermsOfService.txt");
            if (System.IO.File.Exists(termsFilePath))
            {
                TermsContent = System.IO.File.ReadAllText(termsFilePath);
            }
            else
            {
                TermsContent = "Terms of Service content is not available.";
            }
        }
    }
}