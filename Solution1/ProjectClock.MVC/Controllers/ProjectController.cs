using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectClock.BusinessLogic.Services;
using ProjectClock.Database.Entities;

namespace ProjectClock.MVC.Controllers
{
    public class ProjectController : Controller
    {
        
        private readonly IProjectServices _serviceProject;
        

        public ProjectController(IProjectServices serviceProject)
        {           
            _serviceProject = serviceProject;
            
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var list = await _serviceProject.GetAll();
            return View(list);
        }

        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            await _serviceProject.Create(project);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "User")]
        [Route("Project/{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var list = await _serviceProject.GetAll();
            int id = 0;
            foreach (var project in list)
            {
                if (project.Name == name)
                {
                    id = project.Id;
                }
            }
            await _serviceProject.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
