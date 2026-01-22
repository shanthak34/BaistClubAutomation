using System.ComponentModel.DataAnnotations;

namespace BaistClubAutomation.Pages.Models
{
    public class ProspectiveMember
    {
        [Key]
        public int ApplicantId { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } 

        [Required, StringLength(50)]
        public string LastName { get; set; } 

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } 

        [Phone, StringLength(20)]
        public string Phone { get; set; } 

        [StringLength(200)]
        public string Address { get; set; } 

        public string DesiredTier { get; set; } 

        public int SponsorId { get; set; } 

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        public string ApplicationStatus { get; set; } = "Pending"; 

        public DateTime? CommitteeReviewDate { get; set; }

        public string ApprovalNotes { get; set; } 
    }
}
