using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages
{
    public class Index1Model : PageModel
    {
        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                // Not logged in, redirect to login page
                return RedirectToPage("/login"); // Adjust if your login page has a different name
            }

            // Otherwise, show the page
            return Page();
        }
    }
}
