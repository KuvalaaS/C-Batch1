using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.Bean
{
    public class Sports : Event
    {
        public string SportName { get; set; }
        public string Teams { get; set; } // e.g., "India vs Pakistan"

        public Sports() { }

        public Sports(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string sportName, string teams)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "sports")
        {
            SportName = sportName;
            Teams = teams;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sports Event: {EventName}");
            Console.WriteLine($"Sport: {SportName}, Teams: {Teams}");
            Console.WriteLine($"Date: {EventDate:d}, Time: {EventTime}");
            Console.WriteLine($"Venue: {Venue.VenueName}, Available Seats: {AvailableSeats}");
        }
    }

}
