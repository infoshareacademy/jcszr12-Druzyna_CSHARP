using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectClock.BusinessLogic.Dtos.AccountsValidatorsDto;
using ProjectClock.BusinessLogic.Dtos.Validators;
using ProjectClock.BusinessLogic.Mapping;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;



namespace ProjectClock.Database.Extensions
{
    public static class ServiceColletionExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(WorkingTimeMappingProfile));


            services.AddTransient<IProjectServices, ProjectServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IOrganizationServices, OrganizationServices>();
            services.AddTransient<IWorkingTimeServices, WorkingTimeServices>();

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
