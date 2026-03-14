using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    using BaistClubAutomation.Pages.Manager;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class RecordScoreModel : PageModel
    {
        private readonly IScoringService _scoringService;
        private readonly IScoreManager _scoreManager;

        public RecordScoreModel(IScoringService scoringService, IScoreManager scoreManager)
        {
            _scoringService = scoringService;
            _scoreManager = scoreManager;
        }

        [BindProperty]
        public Score ScoreEntry { get; set; }

        public void OnGet()
        {
            // Initialization logic if needed
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Business Logic Check: Ensure total matches the sum of hole scores
            int calculatedTotal = ScoreEntry.HoleScores.Sum();
            if (calculatedTotal != ScoreEntry.TotalScore)
            {
                ModelState.AddModelError("ScoreEntry.TotalScore", "The total score does not match the hole-by-hole sum.");
                return Page();
            }

            // 2. Data Access: Save the record to SQL Server (sarumugam3)
            // Assume MemberId is retrieved from the logged-in user's session
            int currentMemberId = 123;
            bool success = _scoreManager.AddScore(currentMemberId, ScoreEntry);

            if (success)
            {
                TempData["Message"] = "Score successfully recorded and handicap updated.";
                return RedirectToPage("HandicapReport");
            }

            return Page();
        }
    }
}
