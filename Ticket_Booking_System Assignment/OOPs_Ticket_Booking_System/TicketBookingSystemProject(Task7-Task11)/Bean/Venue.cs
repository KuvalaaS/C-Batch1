using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickectBookingSystem_Project.Bean
{
    namespace TicketBookingSystem.Bean
    {
        public class Venue
        {
            public int VenueId { get; set; }
            public string VenueName { get; set; }
            public string Address { get; set; }

            public Venue() { }

            public Venue(int venueId, string venueName, string address)
            {
                VenueId = venueId;
                VenueName = venueName;
                Address = address;
            }

            public void DisplayVenueDetails()
            {
                Console.WriteLine($"Venue: {VenueName}");
                Console.WriteLine($"Address: {Address}");
            }
        }
    }
}
