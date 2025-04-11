using home_owners.Data;
using home_owners.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages
{
    public class registerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public registerModel(ApplicationDbContext context)
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

        public void OnPost()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName))
            {
                // Create a new User object
                var user = new User
                {
                    Username = Username,
                    Password = Password,
                    LastName = LastName,
                    FirstName = FirstName
                };

                // Save the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                Message = $"User {FirstName} {LastName} with username '{Username}' has been registered successfully.";
            }
            else
            {
                Message = "All fields are required.";
            }
        }
    }
}