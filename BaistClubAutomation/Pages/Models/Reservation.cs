using BaistClubAutomation.Pages.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaistClubAutomation.Pages.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Member Number is required to book.")]
        public int MemberNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        // This stores the specific 8-minute interval (e.g., 07:08:00)
        public TimeSpan ReservationTime { get; set; }

        [Range(1, 4, ErrorMessage = "A booking must be for 1 to 4 players.")]
        public int NumberOfPlayers { get; set; }

        public bool IsCartRequired { get; set; }

        // Navigation property to the Member table
        [ForeignKey("MemberNumber")]
        public virtual Member? ReservedBy { get; set; }
    }
}
