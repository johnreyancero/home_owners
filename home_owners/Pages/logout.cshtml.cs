using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace home_owners.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Sign the user out of the application using SignInManager
            await _signInManager.SignOutAsync();

            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the login page after successful logout
            return RedirectToPage("/login");
        }
    }
}
