using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages
{
    public class Index2Model : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (IsValidUser(Username, Password)) // Replace with actual authentication logic
            {
                HttpContext.Session.SetString("Username", Username);
                return RedirectToPage("/Dashboard");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        private bool IsValidUser(string username, string password)
        {
            // Replace this with actual authentication logic (e.g., database validation)
            return username == "admin" && password == "password";
        }
    }
}
