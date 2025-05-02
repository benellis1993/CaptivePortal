using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace naboo_final_project.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string WelcomeText { get; set; } = string.Empty; // Initialize to avoid null warnings
    public string TermsLink { get; set; } = string.Empty;  // Initialize to avoid null warnings
    public string ImagePath { get; set; } = string.Empty;  // Initialize to avoid null warnings

    private readonly string _welcomeTextFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "WelcomeText.txt");
    private readonly string _imagePathFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagePath.txt");

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Load WelcomeText from file
        WelcomeText = System.IO.File.Exists(_welcomeTextFilePath) 
            ? System.IO.File.ReadAllText(_welcomeTextFilePath) 
            : "Welcome to WT Network";

        // Load ImagePath from file
        ImagePath = System.IO.File.Exists(_imagePathFilePath) 
            ? System.IO.File.ReadAllText(_imagePathFilePath) 
            : "/images/default.jpg";

        TermsLink = "/TermsOfService"; // Placeholder for dynamic content
    }
}