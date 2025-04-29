using Microsoft.AspNetCore.Mvc.RazorPages;

namespace naboo_final_project.Pages;

public class AdvertLandingPageModel : PageModel
{
    public List<Advertisement> Advertisements { get; set; } = new();

    public void OnGet()
    {
        // Placeholder for dynamic content integration
        Advertisements = new List<Advertisement>
        {
            new Advertisement
            {
                Title = "Program 1",
                Description = "Learn more about our exciting Program 1.",
                ImageUrl = "/images/ad1.jpg"
            },
            new Advertisement
            {
                Title = "Program 2",
                Description = "Discover the benefits of Program 2.",
                ImageUrl = "/images/ad2.jpg"
            }
        };
    }
}

public class Advertisement
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}