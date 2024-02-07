using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services.ProjectServices;
using System.Numerics;

namespace ProjectClock.MVC.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            ProjectCreator.CreateProject(name);
            return RedirectToAction(nameof(Create));
        }    
    }
}
