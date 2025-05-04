using home_owners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace home_owners.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public LoginModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new(); // <-- safer initialization

        public string ErrorMessage { get; set; }

        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public void OnGet()
        {
            // This line ensures Input is never null during GET
            Input ??= new LoginInputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Input?.Username) || string.IsNullOrWhiteSpace(Input?.Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return Page();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Input.Username);

            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, Input.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToPage("/Users/UserHomePage", new { userId = user.Id });
        }
    }

}
