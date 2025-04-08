using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;
using TickectBookingSystem_Project.Bean;

namespace TickectBookingSystem_Project.Bean
{

    public class Movie : Event
    {
        public string Genre { get; set; }
        public string ActorName { get; set; }
        public string ActressName { get; set; }

        public Movie() { }

        public Movie(string eventName, DateTime eventDate, TimeSpan eventTime, Venue venue, int totalSeats, decimal ticketPrice, string genre, string actor, string actress)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "movie")
        {
            Genre = genre;
            ActorName = actor;
            ActressName = actress;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName}");
            Console.WriteLine($"Genre: {Genre}, Starring: {ActorName} & {ActressName}");
            Console.WriteLine($"Date: {EventDate:d}, Time: {EventTime}");
            Console.WriteLine($"Venue: {Venue.VenueName}, Available Seats: {AvailableSeats}");
        }
    }
}