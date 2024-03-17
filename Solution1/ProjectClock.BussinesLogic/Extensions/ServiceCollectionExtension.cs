﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectClock.BusinessLogic.Dtos.Validators;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Dtos.Validators;
using ProjectClock.BusinessLogic.Dtos.Validators;
using ProjectClock.BusinessLogic.Dtos.AccountsValidatorsDto;



namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrganizationServices, OrganizationServices>();

            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<EditPasswordDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<EditEmailDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<DeleteAccountDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

        }
    }
}
