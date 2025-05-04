using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages.Users
{
    public class UserHomepageModel : PageModel
    {
        public string Username { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");

            // Optional: Redirect to login if session has expired
            if (string.IsNullOrEmpty(Username))
            {
                Response.Redirect("/login"); // or wherever your login page is
            }
        }
    }
}
