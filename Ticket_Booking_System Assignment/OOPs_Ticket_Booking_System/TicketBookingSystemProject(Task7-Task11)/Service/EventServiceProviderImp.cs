using System.Collections.Generic;
using TickectBookingSystem_Project.App;
using TickectBookingSystem_Project.Bean;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;
using TickectBookingSystem_Project.Service;

namespace TickectBookingSystem_Project.Service
{
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        private readonly IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public Event CreateEvent(string name, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
        {
            return repository.CreateEvent(name, date, time, totalSeats, ticketPrice, eventType, venue);
        }

        public List<Event> GetAllEventDetails()
        {
            return repository.GetEventDetails();
        }

        public int GetAvailableSeats(int eventId)
        {
            return repository.GetAvailableNoOfTickets(eventId);
        }
    }
}
