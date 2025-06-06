using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using home_owners.Data; // Adjust namespace
using home_owners.Models; // Your custom User model's namespace
using System.Threading.Tasks;
using System.Linq;

namespace home_owners.Pages.Users
{
    public class UserProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login"); // Or some fallback
            }

            User = await _context.Users.FindAsync(userId.Value);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userToUpdate = await _context.Users.FindAsync(User.Id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = User.FirstName;
            userToUpdate.LastName = User.LastName;
            userToUpdate.Email = User.Email;
            userToUpdate.ContactNumber = User.ContactNumber;

            await _context.SaveChangesAsync();
            return RedirectToPage(); // Refresh page after save
        }
    }
}
