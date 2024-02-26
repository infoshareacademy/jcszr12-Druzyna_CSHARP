using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<ProjectClock.Database.ProjectClockDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ProjectClock")));
        }
    }
}
