using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services;

namespace ProjectClock.MVC.Controllers
{
    public class NewProjectController : Controller
    {
        private readonly IProjectServices _serviceProject;


        public NewProjectController(IProjectServices serviceProject)
        {
            _serviceProject = serviceProject;

        }
        public async Task<IActionResult> Index()
        {
            var list = await _serviceProject.GetAll();

            return View(list);
        }
    }
}
