using System;
using System.Collections.Generic;
using TickectBookingSystem_Project.Bean;
using TickectBookingSystem_Project.Service;
using System.Data.SqlClient;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;

namespace TickectBookingSystem_Project.App
{
    class TicketBookingSystem
    {
        static void Main(string[] args)
        {
            IEventServiceProvider eventService = new EventServiceProviderImpl();
            IBookingSystemServiceProvider bookingService = new BookingSystemServiceProviderImpl();

            while (true)
            {
                Console.WriteLine("\n--- Ticket Booking System ---");
                Console.WriteLine("1. Create Event");
                Console.WriteLine("2. Book Tickets");
                Console.WriteLine("3. Cancel Tickets");
                Console.WriteLine("4. Get Available Seats");
                Console.WriteLine("5. Get Event Details");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Event Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Event Date (yyyy-mm-dd): ");
                            string date = Console.ReadLine();
                            Console.Write("Event Time (hh:mm:ss): ");
                            string time = Console.ReadLine();
                            Console.Write("Total Seats: ");
                            int seats = int.Parse(Console.ReadLine());
                            Console.Write("Ticket Price: ");
                            float price = float.Parse(Console.ReadLine());
                            Console.Write("Event Type: ");
                            string type = Console.ReadLine();
                            Console.Write("Venue ID: ");
                            int venueId = int.Parse(Console.ReadLine());
                            Console.Write("Venue Name: ");
                            string venueName = Console.ReadLine();

                            Venue venue = new Venue { VenueId = venueId, VenueName = venueName };
                            Event ev = eventService.CreateEvent(name, date, time, seats, price, type, venue);
                            Console.WriteLine("Event created successfully with ID: " + ev.EventId);
                            break;

                        case "2":
                            Console.Write("Event Name to Book: ");
                            string eventToBook = Console.ReadLine();
                            Console.Write("Number of Tickets: ");
                            int numTickets = int.Parse(Console.ReadLine());

                            Console.Write("Customer Name: ");
                            string customerName = Console.ReadLine();
                            Console.Write("Customer Email: ");
                            string email = Console.ReadLine();

                            Customer customer = new Customer { CustomerName = customerName, Email = email };
                            int bookingId = bookingService.BookTickets(eventToBook, numTickets, new List<Customer> { customer });
                            Console.WriteLine("Booking successful! Booking ID: " + bookingId);
                            break;

                        case "3":
                            Console.Write("Enter Booking ID to cancel: ");
                            int cancelId = int.Parse(Console.ReadLine());
                            bookingService.CancelBooking(cancelId);
                            Console.WriteLine("Booking cancelled successfully.");
                            break;

                        case "4":
                            Console.Write("Enter Event ID to check available seats: ");
                            int eventId = int.Parse(Console.ReadLine());
                            int available = eventService.GetAvailableSeats(eventId);
                            Console.WriteLine($"Available Seats: {available}");
                            break;

                        case "5":
                            List<Event> allEvents = eventService.GetAllEventDetails();
                            Console.WriteLine("\n--- Event List ---");
                            foreach (var e in allEvents)
                            {
                                Console.WriteLine($"\nEvent ID: {e.EventId}");
                                Console.WriteLine($"Name     : {e.EventName}");
                                Console.WriteLine($"Date     : {e.EventDate.ToShortDateString()}");
                                Console.WriteLine($"Time     : {e.EventTime}");
                                Console.WriteLine($"Seats    : {e.AvailableSeats}/{e.TotalSeats}");
                                Console.WriteLine($"Price    : {e.TicketPrice:C}");
                                Console.WriteLine("-------------------------------");
                            }
                            break;

                        case "6":
                            Console.WriteLine("Exiting system. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input error: " + ex.Message);
                }
            }
        }
    }
}
