﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectClock.BusinessLogic.Services;




namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<IUserServices, UserServices>();
        }
    }
}
