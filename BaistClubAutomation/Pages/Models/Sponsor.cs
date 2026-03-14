using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
{
    public class Sponsor
    {
        [Key]
        public int SponsorID { get; set; }

        
        public int ApplicationID { get; set; }
        [ForeignKey("ApplicationID")]
        public virtual MembershipApplication Application { get; set; }

        
        public int SponsorMemberID { get; set; }
        [ForeignKey("SponsorMemberID")]
        public virtual Member SponsorMember { get; set; }
    }
}
