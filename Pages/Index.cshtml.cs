using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace naboo_final_project.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string WelcomeText { get; set; }
    public string TermsLink { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Load content dynamically (this can be replaced with database logic)
        WelcomeText = "Welcome to WT Network"; // Placeholder for dynamic content
        TermsLink = "/TermsOfService"; // Placeholder for dynamic content
    }
}