using BaistClubAutomation.Pages.Manager;
using BaistClubAutomation.Pages.Models;
namespace BaistClubAutomation.Pages.BLL
{
    public class TeeTimeService
    {
        private readonly TeeTimeManager _teeTimeManager;
        private readonly MembershipManager _membershipManager;

        public TeeTimeService(TeeTimeManager teeTimeManager,MembershipManager membershipManager)
        {
            _teeTimeManager = teeTimeManager;
            _membershipManager = membershipManager;
        }

        public List<TimeSpan> GetAvailableSlots(DateTime date)
        {
            List<TimeSpan> slots = new List<TimeSpan>();
            TimeSpan startTime = new TimeSpan(7, 0, 0); // 7:00 AM
            TimeSpan endTime = new TimeSpan(19, 0, 0);   // 7:00 PM

            while (startTime <= endTime)
            {
                slots.Add(startTime);
                startTime = startTime.Add(TimeSpan.FromMinutes(8)); // The 8-minute rule
            }

            // Logic to filter out slots already in the Reservations table goes here
            return slots;
        }
        public bool ValidateBookingWindow(int memberNumber, DateTime reservationDate)
        {
            // 1. Get member details from the database
            var member = _membershipManager.GetMemberByNumber(memberNumber);
            if (member == null || member.Status != "Active") return false;

            // 2. Calculate how many days away the requested date is
            int daysLeadTime = (reservationDate.Date - DateTime.Today).Days;

            // 3. Enforce business rules
            if (member.MembershipType == "Shareholder")
            {
                // Shareholders get a 7-day window
                return daysLeadTime >= 0 && daysLeadTime <= 7;
            }
            else
            {
                // All other tiers (Gold, Silver, Bronze) get a 2-day window
                return daysLeadTime >= 0 && daysLeadTime <= 2;
            }
        }

        public bool MakeReservation(Reservation booking)
        {
            // 1. Double check the slot is still available
            bool available = _teeTimeManager.IsSlotAvailable(booking.ReservationDate, booking.ReservationTime);

            if (!available) return false;

            // 2. Call the Manager to save the record to the database
            return _teeTimeManager.CreateReservation(booking);
        }
    }
}
