using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services.WorkingTimeServices;

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

       
    }
}
