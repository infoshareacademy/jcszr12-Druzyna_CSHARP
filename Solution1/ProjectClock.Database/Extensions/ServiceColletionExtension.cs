using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectClock.Database.Seeders;


namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectClock.Database.ProjectClockDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ProjectClock")));

            services.AddScoped<ProjectClockSeeder>();

            //services.AddScoped<IProjectServices, ProjectServices>();
            //services.AddScoped<IUserServices, UserServices>();
        }
    }
}
