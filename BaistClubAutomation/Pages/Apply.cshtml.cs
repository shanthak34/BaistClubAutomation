using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaistClubAutomation.Pages.Models;
using BaistClubAutomation.Pages.BLL;

namespace BaistClubAutomation.Pages
{
    public class ApplyModel : PageModel
    {
        private readonly MembershipService _membershipService;

        // Dependency Injection: The UI Tier only talks to the BLL
        public ApplyModel(MembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [BindProperty]
        public ProspectiveMember? Applicant { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            // Initializes the form on the first load
            Applicant = new ProspectiveMember();
        }

        public IActionResult OnPost()
        {
            // 1. Validate the UI Tier Input
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Applicant == null) return Page();

            // 2. Call the BLL Tier to process business rules and persistence
            bool isSuccess = _membershipService.SubmitApplication(Applicant);

            if (isSuccess)
            {
                // Clear the form and redirect or show success
                return RedirectToPage("/Index", new { message = "Application Submitted Successfully!" });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving. Please check your data.");
                return Page();
            }
        }

        // Handler for the Reset Button (Requirement: Allow more than one execution)
        public IActionResult OnPostReset()
        {
            ModelState.Clear();
            Applicant = new ProspectiveMember();
            return Page();
        }
    }
}