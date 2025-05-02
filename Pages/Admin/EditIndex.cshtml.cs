using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace naboo_final_project.Pages.Admin;

public class EditIndexModel : PageModel
{
    [BindProperty]
    public string WelcomeText { get; set; } = string.Empty;

    [BindProperty]
    public string TermsContent { get; set; } = string.Empty;

    [BindProperty]
    public IFormFile UploadedImage { get; set; } // For handling the uploaded image

    private readonly string _welcomeTextFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "WelcomeText.txt");
    private readonly string _termsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TermsOfService.txt");
    private readonly string _imagePathFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagePath.txt");
    private readonly string _imageUploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    public void OnGet()
    {
        WelcomeText = System.IO.File.Exists(_welcomeTextFilePath) 
            ? System.IO.File.ReadAllText(_welcomeTextFilePath) 
            : "Welcome to WT Network";

        TermsContent = System.IO.File.Exists(_termsFilePath) 
            ? System.IO.File.ReadAllText(_termsFilePath) 
            : "Terms Not Available";
    }

    public IActionResult OnPost()
    {
        // Save WelcomeText and TermsContent
        System.IO.File.WriteAllText(_welcomeTextFilePath, WelcomeText);
        System.IO.File.WriteAllText(_termsFilePath, TermsContent);

        // Handle image upload
        if (UploadedImage != null)
        {
            // Ensure the upload directory exists
            if (!Directory.Exists(_imageUploadDirectory))
            {
                Directory.CreateDirectory(_imageUploadDirectory);
            }

            // Save the uploaded file
            var fileName = Path.GetFileName(UploadedImage.FileName);
            var filePath = Path.Combine(_imageUploadDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                UploadedImage.CopyTo(stream);
            }

            // Save the relative path to the image
            var relativePath = $"/images/{fileName}";
            System.IO.File.WriteAllText(_imagePathFilePath, relativePath);
        }

        return RedirectToPage("/Index");
    }
}