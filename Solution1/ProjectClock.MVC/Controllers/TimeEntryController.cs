using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;

namespace ProjectClock.MVC.Controllers
{
    public class TimeEntryController : Controller
    {
        public IActionResult Update()
        {
            return View();
        }


        [HttpPatch]
        public IActionResult Update(UpdateWorkingTimeDto dto)
        {

            return View();
        }
    }
}
