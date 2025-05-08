using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using home_owners.Models;
using home_owners.Data;
using System.Linq;

namespace home_owners.Pages.Admin
{
    public class AdminGuestInfoModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminGuestInfoModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Guest Info ViewModel
        public AdminGuestInfoViewModel GuestInfo { get; set; }

        // OnGet method to fetch the guest details by booking ID
        public void OnGet(int id)
        {
            var booking = (from b in _dbContext.Bookings
                           join u in _dbContext.Users on new { Name = b.GuestName } equals new { Name = u.FirstName + " " + u.LastName } // Matching directly by name
                           where b.Id == id
                           select new AdminGuestInfoViewModel
                           {
                               GuestName = u.FirstName + " " + u.LastName,  // Grab FirstName and LastName from Users
                               Email = u.Email,
                               Contact = u.ContactNumber, // Ensure the correct field for contact number
                               RoomName = b.RoomName,     // Ensure the correct field for room name
                               CheckIn = b.CheckIn,       // Ensure the correct field for check-in date
                               CheckOut = b.CheckOut,     // Ensure the correct field for check-out date
                               CreatedAt = b.CreatedAt,   // Ensure the correct field for creation date
                               SpecialRequest = b.SpecialRequest, // Ensure correct field name
                               Status = b.Status          // Ensure correct field name for status
                           }).FirstOrDefault();

            // Check if the booking exists, otherwise return null GuestInfo
            if (booking != null)
            {
                GuestInfo = booking;
            }
            else
            {
                GuestInfo = null; // Booking not found
            }
        }

        // ViewModel for displaying the guest info
        public class AdminGuestInfoViewModel
        {
            public string GuestName { get; set; }
            public string Email { get; set; }
            public string Contact { get; set; }
            public string RoomName { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public DateTime CreatedAt { get; set; }
            public string SpecialRequest { get; set; }
            public string Status { get; set; }
        }
    }
}
