using BaistClubAutomation.Pages.Models;
using BaistClubAutomation.Pages.Manager;

namespace BaistClubAutomation.Pages.BLL
{
    public class ScoringService
    {
        private readonly ScoreManager _scoreManager;

        public ScoringService(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        public double CalculateDifferential(int grossScore, double courseRating, int slopeRating)
        {
           // WHS Formula 
            double differential = (grossScore - courseRating) * (113.0 / slopeRating);
            return Math.Round(differential, 1);
        }

        public bool AddPlayerScore(Score score)
        {
            // Check if the member exists first
            var memberExists = _scoreManager.CheckMemberExists(score.MemberNumber);
            if (!memberExists)
            {
                return false; 
            }
            
            score.HandicapDifferential = CalculateDifferential(score.TotalGrossScore, score.CourseRating, score.SlopeRating);
            return _scoreManager.AddScore(score);
        }

        public double CalculateHandicapIndex(int memberNumber)
        {
            var differentials = _scoreManager.GetRecentDifferentials(memberNumber, 20);
            if (differentials.Count < 3) return 0.0; 

          
            int countToTake = differentials.Count >= 20 ? 8 : (int)Math.Ceiling(differentials.Count * 0.4);
            var bestDifferentials = differentials.OrderBy(d => d).Take(countToTake);

            return Math.Round(bestDifferentials.Average(), 1);
        }
    }
}

