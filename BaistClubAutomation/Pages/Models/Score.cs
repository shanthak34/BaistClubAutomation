namespace BaistClubAutomation.Pages.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int MemberNumber { get; set; }
        public DateTime DatePlayed { get; set; }
        public required string GolfCourse { get; set; }
        public string? TeeColor { get; set; }
        public double CourseRating { get; set; }
        public int SlopeRating { get; set; }

        // Match these exactly in your CSHTML and Manager
        public int[] HoleScores { get; set; } = new int[18];
        public int TotalGrossScore { get; set; }
        public double HandicapDifferential { get; set; }
    }
}