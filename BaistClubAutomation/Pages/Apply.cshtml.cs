using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class ApplyModel : PageModel
    {
        public void OnGet()
        {
        }
        private readonly MembershipService _membershipService;

        // Constructor: The BLL is automatically "injected" here from Step 5
        public ApplyModel(MembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [BindProperty]
        public ProspectiveMember Applicant { get; set; }

      

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Step: Call the BLL (Service) to handle the application
            bool success = _membershipService.SubmitApplication(Applicant);

            if (success)
            {
                // Redirect to a simple success page or home
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Error: Could not submit application.");
            return Page();
        }
    }
}

