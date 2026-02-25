using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class RecordScoreModel : PageModel
    {
      
        private readonly ScoringService _scoringService;
        public RecordScoreModel(ScoringService scoringService) => _scoringService = scoringService;

        [BindProperty]
        public Score NewScore { get; set; } = new();

        public void OnGet() { }
      
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

          
            switch (NewScore.TeeColor)
            {
                case "Blue": NewScore.CourseRating = 70.9; NewScore.SlopeRating = 127; break;
                case "White": NewScore.CourseRating = 68.8; NewScore.SlopeRating = 123; break;
                case "Red": NewScore.CourseRating = 66.2; NewScore.SlopeRating = 116; break;
            }

            bool success = _scoringService.AddPlayerScore(NewScore);
            if (success) return RedirectToPage("/HandicapHistory", new { memberNumber = NewScore.MemberNumber });

            return Page();
        }
    }
}
