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
            applicant.ApplicationStatus = "Pending";
            applicant.ApplicationDate = DateTime.Now;

            if (string.IsNullOrEmpty(applicant.Email)) return false;

            return _manager.AddProspectiveMember(applicant);
        }
        public List<ProspectiveMember> GetPendingApplications()
        {
            // This assumes your manager has a way to query the context 
            // or you can query directly if the context is available here.
            return _manager.GetPendingApplications();
        }
        public bool FinalizeApplication(int id, string status, string notes)
        {
            // The Service calls the Manager to do the actual database work
            return _manager.FinalizeApplication(id, status, notes);
        }
        public ProspectiveMember GetApplicantById(int id)
        {
            // The BLL asks the Manager for the specific data
            return _manager.GetApplicantById(id);
        }

        // Added for Search Requirement
        public Member FindMember(int id)
        {
            if (id <= 0) return null;
            return _manager.GetMember(id);
        }

        // Added for Update Requirement
        public bool UpdateMemberDetails(Member member)
        {
            if (member == null) return false;
            // Business Rule: Ensure status remains Active during standard updates
            if (string.IsNullOrEmpty(member.Status)) member.Status = "Active";

            return _manager.UpdateMember(member);
        }
    }
}