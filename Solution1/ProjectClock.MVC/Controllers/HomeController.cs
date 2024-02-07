using Microsoft.AspNetCore.Mvc;
using ProjectClock.MVC.Models;
using System.Diagnostics;
using ProjectClock.BusinessLogic.Services.UserServices;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using ProjectClock.BusinessLogic.Models;
using System.Xml.Linq;


namespace ProjectClock.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var projects = ProjectGetter.GetProjectList();
            return View(projects);
        }

       
        public IActionResult Users()
        {
            
            var users = BusinessLogic.Services.UserServices.General.GetUserList();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
