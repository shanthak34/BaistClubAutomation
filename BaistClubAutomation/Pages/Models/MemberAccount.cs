using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
{
    public class MemberAccount
    {
        [Key]
        public int AccountID { get; set; }

        [Required]
        public int MemberID { get; set; }
        [ForeignKey("MemberID")]
        public virtual Member Member { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; } 

        public DateTime? LastPaymentDate { get; set; }

       
        public bool IsOverdue => DateTime.Now > new DateTime(DateTime.Now.Year, 4, 1) && Balance > 0;
    }
}
