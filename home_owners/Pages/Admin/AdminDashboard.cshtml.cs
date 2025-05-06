using Microsoft.AspNetCore.Mvc.RazorPages;
using home_owners.Data; // Adjust namespace
using home_owners.Models; // Your custom User model's namespace
using System;
using System.Collections.Generic;

namespace home_owners.Pages.Admin
{
    public class AdminDashboardModel : PageModel
    {
        // Dashboard metrics
        public int CheckIns { get; set; }
        public int Cancellations { get; set; }
        public int TotalBookings { get; set; }
        public decimal Revenue { get; set; }

        // List of recent bookings
        public List<Booking> RecentBookings { get; set; }

        public void OnGet()
        {
            // Example hardcoded data — replace with database/service call
            CheckIns = 75;
            Cancellations = 12;
            TotalBookings = 105;
            Revenue = 1205.00m;

            RecentBookings = new List<Booking>
            {
                new Booking { Guest = "Emily Smith", CheckIn = "April 15", CheckOut = "Lagkaw Elite", RoomStatus = "Checked In" },
                new Booking { Guest = "Jane Johnson", CheckIn = "April 20", CheckOut = "Lagkaw Haven", RoomStatus = "Cancelled" },
                new Booking { Guest = "Sarah Doe", CheckIn = "May 1", CheckOut = "Lagkaw Prime", RoomStatus = "Checked In" },
                new Booking { Guest = "Michael Brown", CheckIn = "May 2", CheckOut = "Lagkaw Elite", RoomStatus = "Confirmed" }
            };
        }

        // Booking model for recent bookings list
        public class Booking
        {
            public string Guest { get; set; }
            public string CheckIn { get; set; }
            public string CheckOut { get; set; }
            public string RoomStatus { get; set; }
        }
    }
}
