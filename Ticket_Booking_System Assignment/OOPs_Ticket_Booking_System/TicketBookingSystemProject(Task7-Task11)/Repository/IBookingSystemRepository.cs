using System.Collections.Generic;
using TickectBookingSystem_Project.Bean;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.Service
{
    public interface IBookingSystemRepository
    {
        Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue);
        List<Event> GetEventDetails();
        int GetAvailableNoOfTickets(int eventId);
        decimal CalculateBookingCost(int numTickets, decimal ticketPrice);
        int BookTickets(string eventName, int numTickets, List<Customer> listOfCustomers);
        void CancelBooking(int bookingId);
        Booking GetBookingDetails(int bookingId);
    }
}
