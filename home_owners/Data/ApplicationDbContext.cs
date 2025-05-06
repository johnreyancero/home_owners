using Microsoft.EntityFrameworkCore;
using home_owners.Models;

namespace home_owners.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }  // Use your custom User model here
        public DbSet<Admin> Admins { get; set; }

    }
}
