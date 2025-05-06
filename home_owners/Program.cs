using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using home_owners.Data;

namespace home_owners;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Configure the database context with MySQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, 
                new MySqlServerVersion(new Version(10, 4, 32))));

        // Add exception filter for database development
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Configure Identity with custom password policies
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false; // Temporarily set to false while developing
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add Razor Pages for the app
        builder.Services.AddRazorPages();

        // Add session services
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();

        // Enable error handling for development and production
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // HTTPS redirection and static files
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Routing configuration
        app.UseRouting();

        // Enable session before authentication middleware
        app.UseSession();

        // Enable authentication and authorization middleware
        app.UseAuthentication(); // Make sure authentication comes before authorization
        app.UseAuthorization();

        // Map Razor Pages
        app.MapRazorPages();

        // Run the app
        app.Run();
    }
}
