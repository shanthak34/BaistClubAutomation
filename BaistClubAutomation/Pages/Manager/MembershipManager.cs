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
    }
}
  