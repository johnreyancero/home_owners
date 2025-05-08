using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages.Users
{
    public class UserHomepageModel : PageModel
    {
        public string Username { get; set; }

        [BindProperty]
        public string HomeType { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");

            // Redirect to login if not authenticated
            if (string.IsNullOrEmpty(Username))
            {
                Response.Redirect("/login");
            }
        }

        public IActionResult OnPost(string action)
        {
            Username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(Username))
            {
                return RedirectToPage("/Login");
            }

            if (action == "book" && !string.IsNullOrEmpty(HomeType))
            {
                // Save selected home type to session
                HttpContext.Session.SetString("SelectedHomeType", HomeType);

                // Redirect to booking page (you must create this)
                return RedirectToPage("/Users/BookingPage");
            }

            return Page();
        }
    }
}
