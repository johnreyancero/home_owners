using home_owners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace home_owners.Pages.Users
{
    public class UserProfileModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public User User { get; set; }

        public UserProfileModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        // OnGetAsync to load the user profile by UserId
        public async Task<IActionResult> OnGetAsync(int userId)
        {
            // Get the user from the database
            User = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        // OnPostAsync to save updated user profile
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Users/UserProfile");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
