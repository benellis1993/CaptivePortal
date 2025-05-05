using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using naboo_final_project.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace naboo_final_project.Pages.Admin;

[Authorize(Roles = "Admin")] // Restricts access to admin users
public class EditAdvertsModel : PageModel
{
    private readonly UsersDbContext _context; // Database context for accessing adverts

    [BindProperty]
    public string AdvertTitle { get; set; } = string.Empty; // Title of the new advertisement

    [BindProperty]
    public IFormFile? AdvertImage { get; set; } // Uploaded image file for the advertisement

    [BindProperty]
    public string AdvertLink { get; set; } = string.Empty; // URL link for the new advertisement

    [BindProperty]
    public int DisplayLocation { get; set; } // Location identifier for the new advertisement

    public List<Advert> Adverts { get; set; } = new(); // List of existing advertisements

    public EditAdvertsModel(UsersDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        Adverts = _context.Adverts.ToList(); // Fetch all advertisements from the database
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page(); // Return to the page if the model state is invalid
        }

        var advert = new Advert
        {
            Title = AdvertTitle,
            Link = AdvertLink,
            Location = DisplayLocation
        };

        // Handle image upload
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

        _context.Adverts.Add(advert); // Add the new advertisement to the database
        await _context.SaveChangesAsync();

        return RedirectToPage(); // Refresh the page
    }

    public IActionResult OnPostDelete(int id)
    {
        var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
        if (advert != null)
        {
            _context.Adverts.Remove(advert); // Remove the advertisement from the database
            _context.SaveChanges();
        }

        return RedirectToPage(); // Refresh the page
    }

    public IActionResult OnPostToggleActive(int id)
    {
        var advert = _context.Adverts.FirstOrDefault(a => a.Id == id);
        if (advert != null)
        {
            advert.IsActive = !advert.IsActive; // Toggle the IsActive status
            _context.Adverts.Update(advert);
            _context.SaveChanges();
        }

        return RedirectToPage(); // Refresh the page
    }
}