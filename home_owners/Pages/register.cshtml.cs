using System.Security.Cryptography;
using System.Text;
using home_owners.Models;
using home_owners.Data;
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

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string ContactNumber { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            // This method is called when the page is accessed via a GET request.
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Username) &&
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(LastName) &&
                !string.IsNullOrEmpty(FirstName) &&
                !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(ContactNumber))
            {
                var user = new User
                {
                    Username = Username,
                    LastName = LastName,
                    FirstName = FirstName,
                    Email = Email,
                    ContactNumber = ContactNumber
                };

                // Manually hash the password using SHA256
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);
                    byte[] hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
                    string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
                    
                    user.Password = hashedPassword; // Store hashed password
                }

                // Save the user to the database
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
