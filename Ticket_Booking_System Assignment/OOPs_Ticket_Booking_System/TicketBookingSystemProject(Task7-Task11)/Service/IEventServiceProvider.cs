using System.Collections.Generic;
using TickectBookingSystem_Project.Bean;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.Service
{
    public interface IEventServiceProvider
    {
        Event CreateEvent(string name, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue);
        List<Event> GetAllEventDetails();
        int GetAvailableSeats(int eventId);
    }
}
