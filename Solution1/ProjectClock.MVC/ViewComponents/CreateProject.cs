using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.EmailHostedServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.MVC.Extensions;
namespace ProjectClock.MVC.Services.Components;

public class CreateProject : ViewComponent
{
    private readonly IOrganizationServices _organizationServices;
    private readonly IAccountServices _accountService;
    private readonly IEmailHostedServices _emailHostedServices;

    public CreateProject(IOrganizationServices organizationServices
        , IAccountServices accountService,
        EmailHostedServices emailHostedServices)
    {
        _organizationServices = organizationServices;
        _accountService = accountService;
        _emailHostedServices = emailHostedServices;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);

        var userId = await _accountService.GetUserIdFromAccountId(accountId);

        var list = await _organizationServices.GetAllUserOrganization(userId);

        //await _emailHostedServices.SendMailAsync(new BusinessLogic.Email.Models.Email.EmailModel()
        //{
        //    EmailAdress = "tomaszzukowskibp@gmail.com",
        //    Subject = "Hello ProjectClock here",
        //    Body = "<strong>Hi</strong>",
        //});

        return View(list);
    }
}
