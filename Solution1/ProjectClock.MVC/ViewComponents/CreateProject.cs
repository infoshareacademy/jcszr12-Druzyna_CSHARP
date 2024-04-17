using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services.AccountServices;
using ProjectClock.BusinessLogic.Services.OrganizationServices;
using ProjectClock.MVC.Extensions;
namespace ProjectClock.MVC.Services.Components;

public class CreateProject : ViewComponent
{
    private readonly IOrganizationServices _organizationServices;
    private readonly IAccountServices _accountService;

    public CreateProject(IOrganizationServices organizationServices
        , IAccountServices accountService)
    {
        _organizationServices = organizationServices;
        _accountService = accountService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId);

        var userId = await _accountService.GetUserIdFromAccountId(accountId);

        var list = await _organizationServices.GetAllUserOrganization(userId);

        return View(list);
    }
}
