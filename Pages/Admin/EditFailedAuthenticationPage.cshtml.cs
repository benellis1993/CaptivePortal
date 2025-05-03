using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IO;
using System.Text.Json;

namespace naboo_final_project.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class EditFailedAuthenticationPageModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private const string ContentFilePath = "wwwroot/content/failed_auth_content.json";

        public EditFailedAuthenticationPageModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public FailedAuthPageContent PageContent { get; set; } = new();

        public class FailedAuthPageContent
        {
            public string Title { get; set; } = "Access Denied";
            public string HeaderText { get; set; } = "Access Denied";
            public string MainContent { get; set; } = "You do not have permission to access this page.";
            public string ImagePath { get; set; } = "/images/access-denied.jpg";
            public string TermsLink { get; set; } = "/TermsOfService";
            public string TermsContent { get; set; } = "Please review our Terms of Service for more information.";
        }

        public IActionResult OnGet()
        {
            LoadContent();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SaveContent();
            TempData["SuccessMessage"] = "Failed authentication page content updated successfully.";
            return RedirectToPage("/Admin/Index");
        }

        private void LoadContent()
        {
            var filePath = Path.Combine(_environment.WebRootPath, "content", "failed_auth_content.json");
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                PageContent = JsonSerializer.Deserialize<FailedAuthPageContent>(json) ?? new FailedAuthPageContent();
            }
        }

        private void SaveContent()
        {
            var contentDir = Path.Combine(_environment.WebRootPath, "content");
            if (!Directory.Exists(contentDir))
            {
                Directory.CreateDirectory(contentDir);
            }

            var filePath = Path.Combine(contentDir, "failed_auth_content.json");
            var json = JsonSerializer.Serialize(PageContent, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, json);
        }
    }
} 