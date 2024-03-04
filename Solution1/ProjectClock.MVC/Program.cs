using Microsoft.EntityFrameworkCore;
using ProjectClock.Database.Extensions;
using System.Configuration;
using ProjectClock.Database.Seeders;
using ProjectClock.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectClock.BusinessLogic.Dtos.Validators;

namespace ProjectClock.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddServices(builder.Configuration);

            

            builder.Services.AddDbContext<ProjectClock.Database.ProjectClockDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProjectClock"));
                
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";

    });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<ProjectClockSeeder>();
            await seeder.Seed();

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
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
