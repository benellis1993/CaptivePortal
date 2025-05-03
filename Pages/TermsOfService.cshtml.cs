using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace naboo_final_project.Pages;

public class TermsOfServiceModel : PageModel
{
    public string TermsContent { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;

    private readonly string _termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TermsOfService.txt");
    private readonly string _imagePathFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagePath.txt");

    public void OnGet()
    {
        // Load TermsContent from file
        TermsContent = System.IO.File.Exists(_termsFilePath) 
            ? System.IO.File.ReadAllText(_termsFilePath) 
            : "Terms of Service content is not available.";

        // Load ImagePath from file
        ImagePath = System.IO.File.Exists(_imagePathFilePath) 
            ? System.IO.File.ReadAllText(_imagePathFilePath) 
            : "/images/default.jpg";
    }
}