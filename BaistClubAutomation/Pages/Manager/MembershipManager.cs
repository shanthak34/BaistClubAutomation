using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace BaistClubAutomation.Pages.Manager
{
    public class MembershipManager
    {
        private readonly ApplicationDbContext _context;
        public MembershipManager(ApplicationDbContext context) => _context = context;

        // Create: Add a new application
        public bool AddProspectiveMember(ProspectiveMember applicant)
        {
            _context.ProspectiveMembers.Add(applicant);
            return _context.SaveChanges() > 0;
        }

        // Read: Find a member by ID (For Search Requirement)
        public Member GetMember(int memberId)
        {
            return _context.Members.Find(memberId);
        }

        // Update: Save changes to an existing member
        public bool UpdateMember(Member member)
        {
            _context.Members.Update(member);
            return _context.SaveChanges() > 0;
        }
        public List<ProspectiveMember> GetPendingApplications()
        {
            // Queries the database for applicants where the status is exactly 'Pending'
            return _context.ProspectiveMembers
                           .Where(a => a.ApplicationStatus == "Pending")
                           .ToList();
        }
        public int GenerateNewNumber()
        {
            // Finds the highest current member number and adds 1
            // If no members exist, starts at 1000
            int maxNumber = _context.Members.Max(m => (int?)m.MemberNumber) ?? 1000;
            return maxNumber + 1;
        }
        public bool FinalizeApplication(int id, string status, string notes)
        {
            var applicant = _context.ProspectiveMembers.Find(id);
            if (applicant == null) return false;

            applicant.ApplicationStatus = status; // "Approved" or "Rejected"
            applicant.CommitteeReviewDate = DateTime.Now;
            applicant.ApprovalNotes = notes;

            if (status == "Approved")
            {
                // Move to the Members table
                var newMember = new Member
                {
                    MemberNumber = GenerateNewNumber(), // Logic to get next ID
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    MembershipType = applicant.DesiredTier,
                    Status = "Active"
                };
                _context.Members.Add(newMember);
            }

            return _context.SaveChanges() > 0;
        }
    }
}
  