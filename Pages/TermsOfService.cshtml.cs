using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NabooFinalProject.Pages
{
    public class TermsOfServiceModel : PageModel
    {
        public string TermsContent { get; set; }

        public void OnGet()
        {
            TermsContent = @"
                Welcome to the West Texas A&M University public Wi-Fi network. By using this network, you agree to the following terms:
                1. You will not use the network for any illegal activities.
                2. The University is not responsible for any data loss or security breaches.
                3. Your activity may be monitored for security purposes.
                Please read these terms carefully before proceeding.
            ";
        }
    }
}