using BaistClubAutomation.Pages.Data;
using BaistClubAutomation.Pages.Models;

namespace BaistClubAutomation.Pages.Manager
{
   
        public class TeeTimeManager
        {
            private readonly ApplicationDbContext _context;

            public TeeTimeManager(ApplicationDbContext context)
            {
                _context = context;
            }

            // 1. Get all taken slots for a specific date to filter the UI
            public List<TimeSpan> GetReservedTimes(DateTime date)
            {
                return _context.Reservations
                               .Where(r => r.ReservationDate.Date == date.Date)
                               .Select(r => r.ReservationTime)
                               .ToList();
            }

            // 2. Save a new reservation to the database
            public bool CreateReservation(Reservation reservation)
            {
                try
                {
                    _context.Reservations.Add(reservation);
                    return _context.SaveChanges() > 0;
                }
                catch (Exception)
                {
                    // In a real scenario, log the exception here
                    return false;
                }
            }

            // 3. Check if a specific slot is still available (Double-check before saving)
            public bool IsSlotAvailable(DateTime date, TimeSpan time)
            {
                return !_context.Reservations.Any(r =>
                    r.ReservationDate.Date == date.Date &&
                    r.ReservationTime == time);
            }

            // 4. Retrieve a member's details to check their booking window (7-day vs 2-day)
            public Member? GetMemberByNumber(int memberNumber)
            {
                return _context.Members.FirstOrDefault(m => m.MemberNumber == memberNumber);
            }
       
    }
}
    

