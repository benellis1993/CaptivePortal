using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using naboo_final_project.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace naboo_final_project.Pages.Admin;

public class EditSingleAdvertModel : PageModel
{
    private readonly UsersDbContext _context; // Database context for accessing adverts

    [BindProperty]
    public int Id { get; set; } // ID of the advertisement being edited

    [BindProperty]
    public string AdvertTitle { get; set; } = string.Empty; // Title of the advertisement

    [BindProperty]
    public IFormFile? AdvertImage { get; set; } // Uploaded image file for the advertisement

    [BindProperty]
    public string AdvertLink { get; set; } = string.Empty; // URL link for the advertisement

    [BindProperty]
    public int DisplayLocation { get; set; } // Location identifier for the advertisement

    public string CurrentImagePath { get; set; } = string.Empty; // Path to the current image file

    public EditSingleAdvertModel(UsersDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet(int id)
    {
        var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
        if (advert == null)
        {
            return NotFound(); // Return 404 if the advertisement is not found
        }

        // Populate properties for the edit form
        Id = advert.Id;
        AdvertTitle = advert.Title;
        AdvertLink = advert.Link;
        DisplayLocation = advert.Location;
        CurrentImagePath = advert.ImagePath;

        return Page(); // Render the edit page
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var advert = _context.Adverts.FirstOrDefault(a => a.Id == Id);
        if (advert == null)
        {
            return NotFound(); // Return 404 if the advertisement is not found
        }

        // Update advertisement properties
        advert.Title = AdvertTitle;
        advert.Link = AdvertLink;
        advert.Location = DisplayLocation;

        // Set IsActive to true if the advertisement is assigned to a valid program
        advert.IsActive = DisplayLocation != 0;

        // Handle image upload if a new image is provided
        if (AdvertImage != null)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adverts");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Ensure the directory exists
            }

            var filePath = Path.Combine(directoryPath, AdvertImage.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await AdvertImage.CopyToAsync(stream);
            }
            advert.ImagePath = $"/adverts/{AdvertImage.FileName}";
        }

        _context.Adverts.Update(advert); // Save changes to the database
        await _context.SaveChangesAsync();

        return RedirectToPage("./EditAdverts"); // Redirect to the advertisements list page
    }
}