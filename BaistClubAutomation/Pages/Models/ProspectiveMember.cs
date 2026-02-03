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

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, StringLength(10)]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please select a membership category")]
        public string DesiredTier { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } // Required for age-based categories (Pee Wee, Junior, etc.)

        [StringLength(100)]
        public string Occupation { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        public int SponsorID { get; set; } // Required for private club referral logic

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        public string ApplicationStatus { get; set; } = "Pending";

        public DateTime? CommitteeReviewDate { get; set; }

        public string? ApprovalNotes { get; set; }
    }
}