using BaistClubAutomation.Pages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [Required]
        public int MemberNumber { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePlayed { get; set; }
       

        [Required]
        public string GolfCourse { get; set; } = "Club BAIST"; 

        [Required]
        public string TeeColor { get; set; } 

        [Required]
        public int TotalGrossScore { get; set; }
       

        [Required]
        public double CourseRating { get; set; }
        

        [Required]
        public int SlopeRating { get; set; }
       

        // Calculated by the ScoringService BLL
        public double HandicapDifferential { get; set; }

        [ForeignKey("MemberNumber")]
        public virtual Member? Member { get; set; }
    }
}


