using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectClock.BusinessLogic.Dtos.AccountsValidatorsDto;
using ProjectClock.BusinessLogic.Dtos.Validators;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.ValidatorsDto;
using ProjectClock.BusinessLogic.Mapping;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.EmailHostedServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.BusinessLogic.Services.UserServices;
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
            services.AddTransient<IAccountServices, AccountService>();
            services.AddTransient<IOrganizationServices, OrganizationServices>();
            services.AddTransient<IWorkingTimeServices, WorkingTimeServices>();
            services.AddSingleton<EmailHostedServices>();
            services.AddHostedService(provider => provider.GetService<EmailHostedServices>());

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
            services.AddValidatorsFromAssemblyContaining<UpdateWorkingTimeDtoValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
