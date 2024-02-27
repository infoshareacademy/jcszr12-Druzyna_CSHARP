using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.MVC.Models;
using System.Diagnostics;


namespace ProjectClock.MVC.Controllers
{
    public class HomeController : Controller
    {       
        private readonly IProjectServices _serviceProject;
        private readonly IUserServices _serviceUser;

        public HomeController(IProjectServices serviceProject, IUserServices serviceUser)
        {           
            _serviceProject = serviceProject;
            _serviceUser = serviceUser;
        }

        public async Task <IActionResult> Index()
        {
            var projects = await _serviceProject.GetAll();
            return View(projects);
        }


        public async Task <IActionResult> Users()
        {

            var users = await _serviceUser.GetAll();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
