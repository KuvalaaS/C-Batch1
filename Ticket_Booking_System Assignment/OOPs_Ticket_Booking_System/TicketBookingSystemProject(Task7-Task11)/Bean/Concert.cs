using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.Bean
{
    public class Concert : Event
    {
        public string Artist { get; set; }
        public string Type { get; set; } // Theatrical, Rock, etc.

        public Concert() { }

        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string artist, string type)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "concert")
        {
            Artist = artist;
            Type = type;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName}");
            Console.WriteLine($"Artist: {Artist}, Type: {Type}");
            Console.WriteLine($"Date: {EventDate:d}, Time: {EventTime}");
            Console.WriteLine($"Venue: {Venue.VenueName}, Available Seats: {AvailableSeats}");
        }
    }


}
