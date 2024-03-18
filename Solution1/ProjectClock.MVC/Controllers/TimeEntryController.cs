using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services;

namespace ProjectClock.MVC.Controllers
{
    public class TimeEntryController : Controller
    {
        private readonly IWorkingTimeServices _workingTimeServices;
        public TimeEntryController(IWorkingTimeServices workingTimeServices)
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

        [HttpPatch]
        public IActionResult Stop(StartStopWorkingTimeDto dto)
        {
            
            return View();
        }
    }
}
