using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using home_owners.Data;
using System.Threading.Tasks;
using System.Linq;

namespace home_owners.Pages.Admin
{
    public class AdminProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AdminProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public home_owners.Models.Admin Admin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");

            if (adminId == null)
            {
                return RedirectToPage("/Login"); // Or some fallback
            }

            Admin = await _context.Admins.FindAsync(adminId.Value);

            if (Admin == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var adminToUpdate = await _context.Admins.FindAsync(Admin.Id);
            if (adminToUpdate == null)
            {
                return NotFound();
            }

            adminToUpdate.Firstname = Admin.Firstname;
            adminToUpdate.Firstname = Admin.Lastname;
            adminToUpdate.Email = Admin.Email;
            adminToUpdate.ContactNumber = Admin.ContactNumber;

            await _context.SaveChangesAsync();
            return RedirectToPage(); // Refresh page after save
        }
    }
}
