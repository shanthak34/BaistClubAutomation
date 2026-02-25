using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Models;

namespace BaistClubAutomation.Pages.Manager
{
    public class ScoreManager
    {
        private readonly ApplicationDbContext _context;

        public ScoreManager(ApplicationDbContext context) => _context = context;

        public bool AddScore(Score score)
        {
            _context.Scores.Add(score);
            return _context.SaveChanges() > 0;
        }
        public bool CheckMemberExists(int memberNumber)
        {
            // Checks the Members table for a matching ID
            return _context.Members.Any(m => m.MemberNumber == memberNumber);
        }

        public List<double> GetRecentDifferentials(int memberNumber, int count)
        {
            return _context.Scores
                .Where(s => s.MemberNumber == memberNumber)
                .OrderByDescending(s => s.DatePlayed)
                .Take(count)
                .Select(s => s.HandicapDifferential)
                .ToList();
        }
        public List<Score> GetRecentScores(int memberNumber, int count)
        {
            return _context.Scores
                .Where(s => s.MemberNumber == memberNumber)
                .OrderByDescending(s => s.DatePlayed) // Most recent rounds first
                .Take(count)
                .ToList();
        }
    }
}

