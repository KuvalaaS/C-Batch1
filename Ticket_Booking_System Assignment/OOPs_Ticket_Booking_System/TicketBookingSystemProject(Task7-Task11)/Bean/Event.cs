using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.Bean
{
    public abstract class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        protected Event() { }

        protected Event(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string eventType)
        {   

            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            Venue = venue;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = eventType;
        }

        public decimal CalculateTotalRevenue()
        {
            return (TotalSeats - AvailableSeats) * TicketPrice;
        }

        public int GetBookedNoOfTickets()
        {
            return TotalSeats - AvailableSeats;
        }

        public void BookTickets(int numTickets)
        {
            if (numTickets <= AvailableSeats)
                AvailableSeats -= numTickets;
            else
                throw new InvalidOperationException("Not enough seats available.");
        }

        public void CancelBooking(int numTickets)
        {
            if (numTickets > 0 && numTickets <= (TotalSeats - AvailableSeats))
                AvailableSeats += numTickets;
            else
                throw new InvalidOperationException("Invalid number of tickets to cancel.");
        }

        public abstract void DisplayEventDetails();
    }
     
    public class RegularEvent : Event
    {
        public RegularEvent(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Regular")
        {
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Regular Event: {EventName}, Date: {EventDate}, Time: {EventTime}, Venue: {Venue.VenueName}, Price: {TicketPrice}");
        }
    }


}