using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickectBookingSystem_Project.Bean
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer() { }

        public Customer(int customerId, string name, string email, string phone)
        {
            CustomerId = customerId;
            CustomerName = name;
            Email = email;
            PhoneNumber = phone;
        }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"Customer: {CustomerName}");
            Console.WriteLine($"Email: {Email}, Phone: {PhoneNumber}");
        }
    }
}
