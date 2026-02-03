using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class ReviewModel : PageModel
    {
       
        private readonly MembershipService _membershipService;

        public ReviewModel(MembershipService membershipService) => _membershipService = membershipService;

        
        public List<ProspectiveMember> PendingApplications { get; set; } = new();
        public void OnGet()
        {
            // BLL should have a method to get applications filtered by status
            PendingApplications = _membershipService.GetPendingApplications();
        }
    }
}
