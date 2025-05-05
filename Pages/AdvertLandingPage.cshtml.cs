using Microsoft.AspNetCore.Mvc.RazorPages;
using naboo_final_project.Models;

namespace naboo_final_project.Pages;

// Represents the model for the Advert Landing Page
public class AdvertLandingPageModel : PageModel
{
    private readonly UsersDbContext _context; // Database context for accessing adverts

    public List<Advert> Advertisements { get; set; } = new(); // List to store adverts fetched from the database

    // Constructor to initialize the database context
    public AdvertLandingPageModel(UsersDbContext context)
    {
        _context = context;
    }

    // Handles GET requests to fetch all adverts from the database
    public void OnGet()
    {
        Advertisements = _context.Adverts.ToList(); // Retrieve all adverts and store them in the Advertisements list
    }
}

// Represents an advertisement entity with properties for display
public class Advertisement
{
    public string Title { get; set; } = string.Empty; // Title of the advertisement
    public string Description { get; set; } = string.Empty; // Description of the advertisement
    public string ImagePath { get; set; } = string.Empty; // Path to the advertisement's image
    public int Location { get; set; } // Location identifier for categorizing adverts
}