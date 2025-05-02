using home_owners.Data;
using home_owners.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace home_owners.Pages
{
    public class Index2Model : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Index2Model(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (IsValidUser(Username, Password))
            {
                HttpContext.Session.SetString("Username", Username);
                return RedirectToPage("/Index1");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        private bool IsValidUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return false;

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
            
            return result == PasswordVerificationResult.Success;
        }
    }
}
