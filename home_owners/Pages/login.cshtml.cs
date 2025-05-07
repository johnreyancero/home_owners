using home_owners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AdminModel = home_owners.Models.Admin;


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
        public LoginInputModel Input { get; set; } = new();

        public string ErrorMessage { get; set; }

        public class LoginInputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        public void OnGet()
        {
            Input ??= new LoginInputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Input?.Username) || string.IsNullOrWhiteSpace(Input?.Password))
            {
                ErrorMessage = "Please enter both username and password.";
                return Page();
            }

            // Check if user is an admin
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == Input.Username);
            if (admin != null)
            {
                var hasher = new PasswordHasher<AdminModel>();
                var result = hasher.VerifyHashedPassword(admin, admin.Password, Input.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("AdminId", admin.Id);
                    HttpContext.Session.SetString("Username", admin.Username);
                    HttpContext.Session.SetString("Role", "admin");
                    return RedirectToPage("/Admin/AdminDashboard", new { adminId = admin.Id });
                }
            }

            // If not an admin, check regular user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Input.Username);
            if (user != null)
            {
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, Input.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", "user");
                    return RedirectToPage("/Users/UserHomePage", new { userId = user.Id });
                }
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
