using System.Collections.Generic;
using TickectBookingSystem_Project.Bean;

namespace TickectBookingSystem_Project.Service
{
    public interface IBookingSystemServiceProvider
    {
        int BookTickets(string eventName, int numTickets, List<Customer> customers);
        void CancelBooking(int bookingId);
        Booking GetBookingDetails(int bookingId);
        decimal CalculateBookingCost(int numTickets, decimal ticketPrice);
    }
}
