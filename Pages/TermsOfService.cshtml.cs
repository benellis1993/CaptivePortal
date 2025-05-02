using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace NabooFinalProject.Pages;

public class TermsOfServiceModel : PageModel
{
    public string TermsContent { get; set; } = string.Empty;

    private readonly string _termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TermsOfService.txt");

    public void OnGet()
    {
        // Load TermsContent from file
        TermsContent = System.IO.File.Exists(_termsFilePath) 
            ? System.IO.File.ReadAllText(_termsFilePath) 
            : "Terms of Service content is not available.";
    }
}