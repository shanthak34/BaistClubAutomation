using BaistClubAutomation.Pages.BLL;
using BaistClubAutomation.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaistClubAutomation.Pages
{
    public class BookTeeTimeModel : PageModel
    {
       
        private readonly TeeTimeService _teeTimeService;
        public BookTeeTimeModel(TeeTimeService teeTimeService) => _teeTimeService = teeTimeService;

        [BindProperty]
        public Reservation Booking { get; set; } = new() { ReservationDate = DateTime.Today };

        public List<TimeSpan> AvailableSlots { get; set; } = new();

        public void OnGet()
        {
            // Populate slots for the default date
            AvailableSlots = _teeTimeService.GetAvailableSlots(Booking.ReservationDate);
        }

        public IActionResult OnPost()
        {
            // Check booking window (7 days for Shareholders, 2 for others)
            bool canBook = _teeTimeService.ValidateBookingWindow(Booking.MemberNumber, Booking.ReservationDate);

            if (!canBook)
            {
                ModelState.AddModelError(string.Empty, "You are outside your allowed booking window.");
                AvailableSlots = _teeTimeService.GetAvailableSlots(Booking.ReservationDate);
                return Page();
            }

            bool success = _teeTimeService.MakeReservation(Booking);
            return success ? RedirectToPage("/Index") : Page();
        }
    }
}
