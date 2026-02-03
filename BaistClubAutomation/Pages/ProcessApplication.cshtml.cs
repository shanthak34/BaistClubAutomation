using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class ProcessApplicationModel : PageModel
    {
        private readonly MembershipService _membershipService;
        public ProcessApplicationModel(MembershipService membershipService) => _membershipService = membershipService;

        [BindProperty]
        public ProspectiveMember Applicant { get; set; } = default!;

        public void OnGet(int id)
        {
            // Add a method to your BLL to get a single applicant by ID
            Applicant = _membershipService.GetApplicantById(id);
        }

        public IActionResult OnPost(int id, string status, string notes)
        {
            bool success = _membershipService.FinalizeApplication(id, status, notes);

            if (success)
                return RedirectToPage("/Review");

            return Page();
        }
    }
}
