using BaistClubAutomation.Pages.Manager;
using BaistClubAutomation.Pages.Models;

namespace BaistClubAutomation.Pages.BLL
{
    public class MembershipService
    {
        private readonly MembershipManager _manager;
        public MembershipService(MembershipManager manager) => _manager = manager;

        public bool SubmitApplication(ProspectiveMember applicant)
        {
            // Business Logic: Force status to Pending
            applicant.ApplicationStatus = "Pending";
            applicant.ApplicationDate = DateTime.Now;

            // Business Logic: Validation
            if (string.IsNullOrEmpty(applicant.Email)) return false;

            return _manager.AddProspectiveMember(applicant);
        }
    }
}
