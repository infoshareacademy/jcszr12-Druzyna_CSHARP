using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;
using ProjectClock.MVC.Extensions;
namespace ProjectClock.MVC.Services.Components;

public class StopWorkTime : ViewComponent
{
    private readonly IWorkingTimeServices _workingTimeServices;
    public StopWorkTime(IWorkingTimeServices workingTimeServices)
    {
        _workingTimeServices = workingTimeServices;
      
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId);

        var dto = await _workingTimeServices.GetNotFinisedWorkingTimes();

        return View(dto);
    }
}
