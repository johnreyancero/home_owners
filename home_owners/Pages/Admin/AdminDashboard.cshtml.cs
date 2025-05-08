using home_owners.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace home_owners.Pages.Admin
{
    public class AdminDashboardModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public int TotalBookings { get; set; }
        public List<Booking> RecentBookings { get; set; }

        public AdminDashboardModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            TotalBookings = _dbContext.Bookings.Count();
            RecentBookings = _dbContext.Bookings.OrderByDescending(b => b.CreatedAt).Take(5).ToList();
        }
    }
}
