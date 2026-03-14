using System.ComponentModel.DataAnnotations;

namespace BaistClubAutomation.Pages.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string? Email { get; set; }

        // This must match the SQL column name exactly
        public string MembershipLevel { get; set; } = "Associate";

        public string Status { get; set; } = "Active";
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public decimal HandicapIndex { get; set; }
    }
}