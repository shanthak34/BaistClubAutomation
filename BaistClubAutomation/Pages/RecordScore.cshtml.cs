using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaistClubAutomation.Pages.Models;
using BaistClubAutomation.Pages.BLL;

namespace BaistClubAutomation.Pages
{
    public class RecordScoreModel : PageModel
    {
        private readonly IScoringService _service;
        public RecordScoreModel(IScoringService service) => _service = service;

        [BindProperty]
        public Score ScoreEntry { get; set; } = new Score { GolfCourse = "Club BAIST" };

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            ScoreEntry.TotalGrossScore = ScoreEntry.HoleScores.Sum(); // Automatic total calculation [cite: 115]
            bool success = _service.AddPlayerScore(ScoreEntry);

            if (success) return RedirectToPage("HandicapReport");

            ModelState.AddModelError("", "Member not found or database error.");
            return Page();
        }
    }
}