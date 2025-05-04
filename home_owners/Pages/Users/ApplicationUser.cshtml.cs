using Microsoft.AspNetCore.Identity;

namespace home_owners.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties here that you need for your application
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
    }
}
