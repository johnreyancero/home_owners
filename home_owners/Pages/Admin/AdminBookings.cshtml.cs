using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using home_owners.Models;
using home_owners.Data;

namespace home_owners.Pages.Admin
{
    public class AdminBookingsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminBookingsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BookingViewModel> Bookings { get; set; }

        public void OnGet()
        {
            // Updated query to include the Status of the booking
            Bookings = (from booking in _dbContext.Bookings
                        join user in _dbContext.Users on booking.GuestName equals user.FirstName + " " + user.LastName
                        select new BookingViewModel
                        {
                            Id = booking.Id,
                            GuestName = booking.GuestName,
                            Email = user.Email,
                            Contact = user.ContactNumber,
                            Status = booking.Status // Add the Status field from the Booking model
                        }).ToList();
        }

        // Updated ViewModel to include Status
        public class BookingViewModel
        {
            public int Id { get; set; }
            public string GuestName { get; set; }
            public string Email { get; set; }
            public string Contact { get; set; }
            public string Status { get; set; } // Add Status property
        }
    }
}
