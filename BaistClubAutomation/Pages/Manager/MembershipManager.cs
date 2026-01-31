using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Models;

namespace BaistClubAutomation.Pages.Manager
{
    public class MembershipManager
    {
        private readonly ApplicationDbContext _context;
        public MembershipManager(ApplicationDbContext context) => _context = context;

        public bool AddProspectiveMember(ProspectiveMember applicant)
        {
            _context.ProspectiveMembers.Add(applicant);
            return _context.SaveChanges() > 0;
        }
    }
}
