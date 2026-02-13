using BaistClubAutomation.Pages.Manager;
namespace BaistClubAutomation.Pages.BLL
{
    public class TeeTimeService
    {
        private readonly TeeTimeManager _teeTimeManager;

        public TeeTimeService(TeeTimeManager teeTimeManager)
        {
            _teeTimeManager = teeTimeManager;
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
    }
}
