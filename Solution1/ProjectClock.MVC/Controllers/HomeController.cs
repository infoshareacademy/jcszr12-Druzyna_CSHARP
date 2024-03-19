using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.MVC.Models;
using System.Diagnostics;


namespace ProjectClock.MVC.Controllers
{
    public class HomeController : Controller
    {       
        private readonly IProjectServices _serviceProject;
        

        public HomeController(IProjectServices serviceProject)
        {           
            _serviceProject = serviceProject;
            
        }

        [Authorize(Roles = "User")]
        public async Task <IActionResult> Index()
        {
            var dto = new StartWorkingTimeDto();
            dto.projects = await _serviceProject.GetAll();
            return View(dto);
        }


       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
