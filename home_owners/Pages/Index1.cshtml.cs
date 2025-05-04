using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace home_owners.Pages
{
    public class Index1Model : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
