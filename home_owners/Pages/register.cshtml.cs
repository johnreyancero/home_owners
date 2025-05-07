using System.ComponentModel.DataAnnotations;
using home_owners.Models;
using home_owners.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity; // For PasswordHasher

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
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string ContactNumber { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            // Handle GET request
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = Username,
                    LastName = LastName,
                    FirstName = FirstName,
                    Email = Email,
                    ContactNumber = ContactNumber
                };

                // Use ASP.NET Core's password hasher
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, Password);

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
