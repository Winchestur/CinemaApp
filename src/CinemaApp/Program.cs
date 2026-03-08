using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
namespace CinemaApp.Web
{
    using CinemaApp.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using CinemaApp.Services.Core;
    using CinemaApp.Services.Core.Contracts;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                 ConfigureIdentityOptions(builder.Configuration, options);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();

            
        }

        private static void ConfigureIdentityOptions(ConfigurationManager configuration, IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = 
                configuration.GetValue<bool>("Identity:RequireConfirmedAccount");

            options.Password.RequireDigit = configuration.GetValue<bool>("Identity:RequireDigit");

            options.Password.RequiredLength = configuration.GetValue<int>("Identity:RequireLength");

            options.Password.RequireUppercase = configuration.GetValue<bool>("Identity:RequireUppercase");

            options.Password.RequireNonAlphanumeric = 
                configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");

            options.Password.RequireLowercase = configuration.GetValue<bool>("Identity:RequireLowercase");
        }
    }
}
