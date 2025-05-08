using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace home_owners.Pages.Users
{
    public class BookingPageModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Data.ApplicationDbContext _dbContext; // Your EF Core context

        public BookingPageModel(UserManager<IdentityUser> userManager, Data.ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [BindProperty(SupportsGet = true)]
        public string HomeType { get; set; }

        public string SelectedHomeType { get; set; }

        [BindProperty]
        public DateTime CheckIn { get; set; }

        [BindProperty]
        public DateTime CheckOut { get; set; }

        [BindProperty]
        public string SpecialRequest { get; set; }

        [BindProperty] // Add this for the RoomName
        public string RoomName { get; set; } // New RoomName property

        public void OnGet()
        {
            SelectedHomeType = HomeType;
            HttpContext.Session.SetString("SelectedHomeType", SelectedHomeType ?? "");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SelectedHomeType = HttpContext.Session.GetString("SelectedHomeType") ?? "";
            
            // Lookup user's full name in your custom user table
            var appUser = _dbContext.Users.FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("UserId"));
            string guestName = $"{appUser.FirstName} {appUser.LastName}";

            // DB connection
            string connectionString = "server=localhost;database=home_owners;user=root;password=;";
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO bookings (GuestName, CheckIn, CheckOut, SpecialRequest, RoomName)
                                VALUES (@GuestName, @CheckIn, @CheckOut, @SpecialRequest, @RoomName)";
            cmd.Parameters.AddWithValue("@GuestName", guestName);
            cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
            cmd.Parameters.AddWithValue("@CheckOut", CheckOut);
            cmd.Parameters.AddWithValue("@SpecialRequest", SpecialRequest ?? "");
            cmd.Parameters.AddWithValue("@RoomName", SelectedHomeType ?? "");

            cmd.ExecuteNonQuery();

            return RedirectToPage("UserHomepage");
        }
    }
}
