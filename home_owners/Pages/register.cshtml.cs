using home_owners.Data;
using home_owners.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            // This method is called when the page is accessed via a GET request.
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName))
            {
                var user = new User
                {
                    Username = Username,
                    LastName = LastName,
                    FirstName = FirstName
                };

                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, Password);

                _context.Users.Add(user);
                _context.SaveChanges();

                Message = $"User {FirstName} {LastName} with username '{Username}' has been registered successfully.";

                return RedirectToPage("/login");
            }
            else
            {
                Message = "All fields are required.";
                return Page();
            }
        }
    }
}
