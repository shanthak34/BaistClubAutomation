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
            // If the table is empty, .Max() will return null. The ?? 1000 starts your IDs at 1001.
            int maxId = _context.Members.Select(m => (int?)m.MemberNumber).Max() ?? 1000;
            return maxId + 1;
        }
        public ProspectiveMember GetApplicantById(int id)
        {
            // Uses Entity Framework's Find method to locate the record by Primary Key
            // Returns null if the ID does not exist in the database
            var applicant = _context.ProspectiveMembers.Find(id);
            return applicant;
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
  