using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
{
    public class Member
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // MemberNumber is assigned, not identity
        public int MemberNumber { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } 

        [Required, StringLength(50)]
        public string LastName { get; set; } 

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } 

       

        public bool IsShareholder { get; set; } 

        public DateTime JoinDate { get; set; } 

        public string Status { get; set; } = "Active"; 

        [Column(TypeName = "decimal(4,1)")]
        public decimal HandicapIndex { get; set; } 

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
       

           

            [Required, StringLength(50)]
            public string MembershipType { get; set; } = string.Empty; // Gold, Silver, Bronze, etc.

           
        }
    }


