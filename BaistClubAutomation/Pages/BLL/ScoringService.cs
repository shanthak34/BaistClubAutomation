using BaistClubAutomation.Pages.Models;
using BaistClubAutomation.Pages.Manager;

namespace BaistClubAutomation.Pages.BLL
{
    public interface IScoringService
    {
        bool AddPlayerScore(Score score);
        double CalculateHandicapIndex(int memberNumber);
    }

    public class ScoringService : IScoringService
    {
        private readonly IScoreManager _scoreManager;

        public ScoringService(IScoreManager scoreManager) => _scoreManager = scoreManager;

        public bool AddPlayerScore(Score score)
        {
            if (!_scoreManager.CheckMemberExists(score.MemberNumber)) return false;

           
            score.HandicapDifferential = Math.Round((score.TotalGrossScore - score.CourseRating) * (113.0 / score.SlopeRating), 1);
            return _scoreManager.AddScore(score);
        }

        public double CalculateHandicapIndex(int memberNumber)
        {
            // Retrieve the list of Score objects from the DAL
            List<Score> scoreHistory = _scoreManager.GetRecentScores(memberNumber, 20);

           
            if (scoreHistory.Count < 3) return 0.0;

            
            var differentials = scoreHistory.Select(s => s.HandicapDifferential).ToList();


            int countToTake = differentials.Count >= 20 ? 8 : (int)Math.Ceiling(differentials.Count * 0.4);
            var bestDifferentials = differentials.OrderBy(d => d).Take(countToTake);

            return Math.Round(bestDifferentials.Average(), 1);
        }
    }
}