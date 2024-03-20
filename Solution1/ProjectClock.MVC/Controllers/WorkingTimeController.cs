using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;
using ProjectClock.MVC.Extensions;

namespace ProjectClock.MVC.Controllers
{
    public class WorkingTimeController : Controller
    {
        private readonly IWorkingTimeServices _workingTimeServices;
        public WorkingTimeController(IWorkingTimeServices workingTimeServices)
        {
            _workingTimeServices = workingTimeServices;
        }



        public IActionResult Update()
        {
            return View();
        }


        [HttpPatch]
        public IActionResult Update(UpdateWorkingTimeDto dto)
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Stop(StartStopWorkingTimeDto dto)
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var accountId))
            {
                return RedirectToAction("Index", "Home");
            }

            await _workingTimeServices.StopWork(dto);

            return RedirectToAction("Index", "Home");
        }
    }
}
