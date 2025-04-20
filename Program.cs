using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Helpers;
using Portfolio.Settings;

namespace Portfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

            // Configure SmtpSettings
            builder.Services.Configure<SmtpSettings>(
                 builder.Configuration.GetSection("SmtpSettings"));
            // Add EmailService
            builder.Services.AddTransient<EmailService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
