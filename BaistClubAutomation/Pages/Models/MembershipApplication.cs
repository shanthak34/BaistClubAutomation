using System.ComponentModel.DataAnnotations;

namespace BaistClubAutomation.Pages.Models
{
    public class MembershipApplication
    {
        public int ApplicationID { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Occupation { get; set; }

        public required string CompanyName { get; set; }

        public required string Address { get; set; }

        public required string PostalCode { get; set; }

        public required string Phone { get; set; }

        public string AlternatePhone { get; set; }

        public required string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime ApplicationDate { get; set; }

        public   string Status { get; set; }

        public int Sponsor1MemberID { get; set; }

        public int Sponsor2MemberID { get; set; }
    }
}