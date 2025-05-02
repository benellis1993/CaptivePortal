using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace naboo_final_project.Pages.Admin;

public class EditIndexModel : PageModel
{
    [BindProperty]
    public string WelcomeText { get; set; } = string.Empty;

    [BindProperty]
    public string TermsContent { get; set; } = string.Empty;

    private readonly string _welcomeTextFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "WelcomeText.txt");
    private readonly string _termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TermsOfService.txt");

    public void OnGet()
    {
        // Load WelcomeText from file
        WelcomeText = System.IO.File.Exists(_welcomeTextFilePath) 
            ? System.IO.File.ReadAllText(_welcomeTextFilePath) 
            : "Welcome to WT Network";

        // Load TermsContent from file
        TermsContent = System.IO.File.Exists(_termsFilePath) 
            ? System.IO.File.ReadAllText(_termsFilePath) 
            : "Terms Not Available";
    }

    public IActionResult OnPost()
    {
        // Save WelcomeText to file
        System.IO.File.WriteAllText(_welcomeTextFilePath, WelcomeText);

        // Save TermsContent to file
        System.IO.File.WriteAllText(_termsFilePath, TermsContent);

        return RedirectToPage("/Index");
    }
}