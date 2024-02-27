using Microsoft.EntityFrameworkCore;
using ProjectClock.Database.Extensions;
using System.Configuration;

namespace ProjectClock.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddServices(builder.Configuration);

            builder.Services.AddDbContext<ProjectClock.Database.ProjectClockDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectClock"));
                //options.EnableThreadSafetyChecks();
            });



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
