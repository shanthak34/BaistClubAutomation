using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
    {
        public class MembershipType
        {
            [Key]
            public int MembershipTypeID { get; set; }

            [Required]
            [StringLength(20)]
            public string Level { get; set; } 

            [Required]
            [StringLength(50)]
            public string TypeName { get; set; } 

            [Column(TypeName = "decimal(10,2)")]
            public decimal AnnualFee { get; set; } 

            [Column(TypeName = "decimal(10,2)")]
            public decimal FoodBeverageMinimum { get; set; } 
        }
    }
}
}
