using System.Collections.Generic;
using TickectBookingSystem_Project.App;
using TickectBookingSystem_Project.Bean;

namespace TickectBookingSystem_Project.Service
{
    public class BookingSystemServiceProviderImpl : IBookingSystemServiceProvider
    {
        private readonly IBookingSystemRepository repository = new BookingSystemRepositoryImpl();

        public int BookTickets(string eventName, int numTickets, List<Customer> customers)
        {
            return repository.BookTickets(eventName, numTickets, customers);
        }

        public void CancelBooking(int bookingId)
        {
            repository.CancelBooking(bookingId);
        }

        public Booking GetBookingDetails(int bookingId)
        {
            return repository.GetBookingDetails(bookingId);
        }

        public decimal CalculateBookingCost(int numTickets, decimal ticketPrice)
        {
            return repository.CalculateBookingCost(numTickets, ticketPrice);
        }
    }
}
