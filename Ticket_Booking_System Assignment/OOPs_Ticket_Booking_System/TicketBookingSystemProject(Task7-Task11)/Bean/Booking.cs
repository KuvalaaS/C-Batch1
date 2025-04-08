using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickectBookingSystem_Project.Bean
{
    public class Booking
    {
        public int BookingId { get; set; }
        public List<Customer> Customers { get; set; } // List of customers (Task 10)
        public Event Event { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking() { }

        public Booking(int bookingId, List<Customer> customers, Event ev, int numTickets, decimal totalCost, DateTime bookingDate)
        {
            BookingId = bookingId;
            Customers = customers;
            Event = ev;
            NumTickets = numTickets;
            TotalCost = totalCost;
            BookingDate = bookingDate;
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingId}");
            Console.WriteLine($"Event: {Event.EventName}, Date: {Event.EventDate:d}");
            Console.WriteLine($"Number of Tickets: {NumTickets}, Total Cost: {TotalCost:C}");
            Console.WriteLine("Customers:");
            foreach (var customer in Customers)
            {
                Console.WriteLine($"- {customer.CustomerName} ({customer.Email})");
            }
        }
    }

}
