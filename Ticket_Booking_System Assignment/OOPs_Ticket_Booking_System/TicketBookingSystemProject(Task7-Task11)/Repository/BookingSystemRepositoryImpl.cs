using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TickectBookingSystem_Project.Bean;
using TickectBookingSystem_Project.Bean.TicketBookingSystem.Bean;
using TickectBookingSystem_Project.Service;
using TickectBookingSystem_Project.Util;

namespace TickectBookingSystem_Project.App
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
    {
        public Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                conn.Open();
                string query = @"INSERT INTO tbl_event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type)
                                 OUTPUT INSERTED.event_id
                                 VALUES (@name, @date, @time, @venueId, @totalSeats, @availableSeats, @price, @type)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", eventName);
                cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
                cmd.Parameters.AddWithValue("@time", TimeSpan.Parse(time));
                cmd.Parameters.AddWithValue("@venueId", venue.VenueId);
                cmd.Parameters.AddWithValue("@totalSeats", totalSeats);
                cmd.Parameters.AddWithValue("@availableSeats", totalSeats);
                cmd.Parameters.AddWithValue("@price", ticketPrice);
                cmd.Parameters.AddWithValue("@type", eventType);

                object result = cmd.ExecuteScalar();
                int eventId = result != null ? Convert.ToInt32(result) : 0;

                RegularEvent newEvent = new RegularEvent(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, (decimal)ticketPrice);
                newEvent.EventId = eventId;
                return newEvent;
            }
        }

        public List<Event> GetEventDetails()
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                conn.Open();
                string query = "SELECT * FROM tbl_event";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Venue venue = new Venue { VenueId = Convert.ToInt32(reader["venue_id"]) };
                    RegularEvent evt = new RegularEvent(
                        reader["event_name"].ToString(),
                        Convert.ToDateTime(reader["event_date"]),
                        (TimeSpan)reader["event_time"],
                        venue,
                        Convert.ToInt32(reader["total_seats"]),
                        Convert.ToDecimal(reader["ticket_price"])
                    );
                    evt.EventId = Convert.ToInt32(reader["event_id"]);
                    evt.AvailableSeats = Convert.ToInt32(reader["available_seats"]);
                    events.Add(evt);
                }
            }

            return events;
        }

        public int GetAvailableNoOfTickets(int eventId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                conn.Open();
                string query = "SELECT available_seats FROM tbl_event WHERE event_id = @eventId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@eventId", eventId);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public decimal CalculateBookingCost(int numTickets, decimal ticketPrice)
        {
            return numTickets * ticketPrice;
        }

        public int BookTickets(string eventName, int numTickets, List<Customer> listOfCustomers)
        {
            try
            {
                using (SqlConnection conn = DBUtil.GetDBConn())
                {
                    conn.Open();

                    // Get event
                    string eventQuery = "SELECT * FROM tbl_event WHERE event_name = @eventName";
                    SqlCommand eventCmd = new SqlCommand(eventQuery, conn);
                    eventCmd.Parameters.AddWithValue("@eventName", eventName);

                    Event ev = null;
                    using (SqlDataReader reader = eventCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ev = new RegularEvent(
                                reader["event_name"].ToString(),
                                Convert.ToDateTime(reader["event_date"]),
                                (TimeSpan)reader["event_time"],
                                new Venue { VenueId = Convert.ToInt32(reader["venue_id"]) },
                                Convert.ToInt32(reader["total_seats"]),
                                Convert.ToDecimal(reader["ticket_price"])
                            );
                            ev.EventId = Convert.ToInt32(reader["event_id"]);
                            ev.AvailableSeats = Convert.ToInt32(reader["available_seats"]);
                        }
                    }

                    if (ev == null || ev.AvailableSeats < numTickets)
                        throw new InvalidOperationException("Not enough tickets available or event not found.");

                    decimal totalCost = CalculateBookingCost(numTickets, ev.TicketPrice);

                    SqlCommand bookingCmd = new SqlCommand(@"
                        INSERT INTO tbl_booking (customer_id, event_id, num_tickets, total_cost, booking_date)
                        OUTPUT INSERTED.booking_id
                        VALUES (@customerId, @eventId, @numTickets, @totalCost, @bookingDate)", conn);

                    int customerId = EnsureCustomerExists(conn, listOfCustomers[0]);
                    bookingCmd.Parameters.AddWithValue("@customerId", customerId);
                    bookingCmd.Parameters.AddWithValue("@eventId", ev.EventId);
                    bookingCmd.Parameters.AddWithValue("@numTickets", numTickets);
                    bookingCmd.Parameters.AddWithValue("@totalCost", totalCost);
                    bookingCmd.Parameters.AddWithValue("@bookingDate", DateTime.Now);

                    object result = bookingCmd.ExecuteScalar();
                    int bookingId = result != null ? Convert.ToInt32(result) : 0;

                    SqlCommand updateEvent = new SqlCommand(
                        "UPDATE tbl_event SET available_seats = available_seats - @numTickets WHERE event_id = @eventId", conn);
                    updateEvent.Parameters.AddWithValue("@numTickets", numTickets);
                    updateEvent.Parameters.AddWithValue("@eventId", ev.EventId);
                    updateEvent.ExecuteNonQuery();

                    return bookingId;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("A database error occurred: " + ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Booking failed: " + ex.Message);
                throw;
            }
        }

        private int EnsureCustomerExists(SqlConnection conn, Customer customer)
        {
            string selectQuery = "SELECT customer_id FROM tbl_customer WHERE email = @Email";
            SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
            selectCmd.Parameters.AddWithValue("@Email", customer.Email);
            object result = selectCmd.ExecuteScalar();

            if (result != null)
            {
                return Convert.ToInt32(result); // Customer already exists
            }

            // Insert new customer
            string insertQuery = "INSERT INTO tbl_customer (customer_name, email) OUTPUT INSERTED.customer_id VALUES (@Name, @Email)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
            insertCmd.Parameters.AddWithValue("@Name", customer.CustomerName);
            insertCmd.Parameters.AddWithValue("@Email", customer.Email);

            return (int)insertCmd.ExecuteScalar(); // Return new customer ID
        }

        public void CancelBooking(int bookingId)
        {
            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                conn.Open();

                int eventId = 0;
                int numTickets = 0;

                SqlCommand getBooking = new SqlCommand("SELECT event_id, num_tickets FROM tbl_booking WHERE booking_id = @bookingId", conn);
                getBooking.Parameters.AddWithValue("@bookingId", bookingId);
                using (SqlDataReader reader = getBooking.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        eventId = Convert.ToInt32(reader["event_id"]);
                        numTickets = Convert.ToInt32(reader["num_tickets"]);
                    }
                }

                SqlCommand deleteBooking = new SqlCommand("DELETE FROM tbl_booking WHERE booking_id = @bookingId", conn);
                deleteBooking.Parameters.AddWithValue("@bookingId", bookingId);
                deleteBooking.ExecuteNonQuery();

                SqlCommand updateEvent = new SqlCommand("UPDATE tbl_event SET available_seats = available_seats + @numTickets WHERE event_id = @eventId", conn);
                updateEvent.Parameters.AddWithValue("@numTickets", numTickets);
                updateEvent.Parameters.AddWithValue("@eventId", eventId);
                updateEvent.ExecuteNonQuery();
            }
        }

        public Booking GetBookingDetails(int bookingId)
        {
            Booking booking = null;

            using (SqlConnection conn = DBUtil.GetDBConn())
            {
                conn.Open();

                string query = "SELECT * FROM tbl_booking WHERE booking_id = @bookingId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookingId", bookingId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            BookingId = Convert.ToInt32(reader["booking_id"]),
                            NumTickets = Convert.ToInt32(reader["num_tickets"]),
                            TotalCost = Convert.ToDecimal(reader["total_cost"]),
                            BookingDate = Convert.ToDateTime(reader["booking_date"]),
                            Customers = new List<Customer>
                            {
                                new Customer
                                {
                                    CustomerId = Convert.ToInt32(reader["customer_id"]),
                                    CustomerName = "FetchedName", 
                                    Email = "email@example.com"
                                }
                            },
                            Event = new RegularEvent("Unknown", DateTime.Now, TimeSpan.Zero, new Venue { VenueId = 0 }, 0, 0)
                            {
                                EventId = Convert.ToInt32(reader["event_id"])
                            }
                        };
                    }
                }
            }

            return booking;
        }
    }
}
