using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectClock.BusinessLogic.Services;
using FluentValidation;



namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<IWorkingTimeServices, WorkingTimeServices>();
            services.AddScoped<IOrganizationServices, OrganizationServices>();
        }
    }
}
