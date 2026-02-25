using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Manager;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class HandicapHistoryModel : PageModel
    {
      
       
            private readonly ScoreManager _scoreManager;
            private readonly ScoringService _scoringService;

            public HandicapHistoryModel(ScoreManager scoreManager, ScoringService scoringService)
            {
                _scoreManager = scoreManager;
                _scoringService = scoringService;
            }

            public List<Score> MemberScores { get; set; } = new();
            public double CurrentHandicapIndex { get; set; }

            public void OnGet(int memberNumber)
            {
                // Retrieve last 20 rounds for the report 
                MemberScores = _scoreManager.GetRecentScores(memberNumber, 20);

                // Calculate the official Index (Best 8 of 20) [cite: 130, 131]
                CurrentHandicapIndex = _scoringService.CalculateHandicapIndex(memberNumber);
            }
        }
    }

